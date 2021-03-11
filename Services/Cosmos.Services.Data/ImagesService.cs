namespace Cosmos.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Cosmos.Data;
    using Cosmos.Data.Common.Repositories;
    using Cosmos.Data.Models;
    using Cosmos.Web.ViewModels.ImageFiles;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;

    public class ImagesService : IImagesService
    {
        private const int RenderingSize = 1000;

        private readonly IRepository<Alliance> allianceRepository;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IRepository<Player> playersRepository;
        private readonly ApplicationDbContext dbContext;
        private readonly IRepository<CosmosOriginalImage> originalImagesRepository;
        private readonly IRepository<CosmosImage> imagesRepository;

        public ImagesService(IRepository<Alliance> allianceRepository, IServiceScopeFactory serviceScopeFactory, IRepository<Player> playersRepository, ApplicationDbContext dbContext, IRepository<CosmosOriginalImage> originalImagesRepository, IRepository<CosmosImage> imagesRepository)
        {
            this.allianceRepository = allianceRepository;
            this.serviceScopeFactory = serviceScopeFactory;
            this.playersRepository = playersRepository;
            this.dbContext = dbContext;
            this.originalImagesRepository = originalImagesRepository;
            this.imagesRepository = imagesRepository;
        }

        public async Task AddImage(Stream content, string id)
        {
            try
            {
                using var imageResult = await Image.LoadAsync(content);

                var original = await this.SaveImage(imageResult, imageResult.Width);
                var renderImage = await this.SaveImage(imageResult, RenderingSize);

                // var database = this.serviceScopeFactory
                //    .CreateScope()
                //    .ServiceProvider
                //    .GetRequiredService<ApplicationDbContext>();
                var player = this.playersRepository.All().FirstOrDefault(x => x.Id == id);
                if (player != null)
                {
                    var originalPlayerImage = new CosmosOriginalImage { Content = original };
                    await this.originalImagesRepository.AddAsync(originalPlayerImage);
                    player.OriginalImageId = originalPlayerImage.Id;
                    await this.originalImagesRepository.SaveChangesAsync();

                    var image = new CosmosImage { Content = renderImage };
                    await this.imagesRepository.AddAsync(image);
                    player.ImageId = image.Id;
                    this.playersRepository.Update(player);
                    await this.playersRepository.SaveChangesAsync();
                    await this.imagesRepository.SaveChangesAsync();
                    return;
                }

                var alliance = await this.allianceRepository.All().FirstOrDefaultAsync(x => x.Id == id);
                var originalImage = new CosmosOriginalImage { Content = original };
                await this.originalImagesRepository.AddAsync(originalImage);
                await this.originalImagesRepository.SaveChangesAsync();
                alliance.OriginalImage = originalImage;

                var allianceRenderImage = new CosmosImage { Content = renderImage };
                await this.imagesRepository.AddAsync(allianceRenderImage);
                await this.imagesRepository.SaveChangesAsync();
                alliance.Image = allianceRenderImage;
                this.allianceRepository.Update(alliance);
                await this.allianceRepository.SaveChangesAsync();

                // await database.SaveChangesAsync();
            }
            catch
            {
                // Log information.
            }
        }

        //public Task<FileStreamResult> GetRenderImage(string allianceId)
        //{
        //    return this.GetImageData(allianceId);
        //}

        public async Task<string> GetImageData(string id)
        {
            if (id == null)
            {
                return null;
            }

            var database = this.dbContext.Database;

            var dbConnection = (SqlConnection)database.GetDbConnection();

            // var command = new SqlCommand($"SELECT AllianceImage FROM Alliances WHERE Id = @id;", dbConnection);
            var command = new SqlCommand($"SELECT Content FROM Images WHERE Id = @id;", dbConnection);

            command.Parameters.Add(new SqlParameter("@id", id));

            dbConnection.Open();

            var dtoImage = await command.ExecuteScalarAsync();

            if (dtoImage == DBNull.Value)
            {
                await dbConnection.CloseAsync();
                return null;
            }

            if (dtoImage == null)
            {
                await dbConnection.CloseAsync();
                return null;
            }

            var imageAsBytes = (byte[])dtoImage;
            var result = Convert.ToBase64String(imageAsBytes);

            //Stream result = null;

            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        result = reader.GetStream(0);
            //    }
            //}

            //reader.Close();

            await dbConnection.CloseAsync();
            return result;
        }

        private async Task<byte[]> SaveImage(Image image, int resizeWidth)
        {
            var width = image.Width;
            var height = image.Height;

            if (width > resizeWidth)
            {
                height = (int)((double)resizeWidth / width * height);
                width = resizeWidth;
            }

            image
                .Mutate(i => i
                    .Resize(new Size(width, height)));

            image.Metadata.ExifProfile = null;

            var memoryStream = new MemoryStream();

            await image.SaveAsJpegAsync(memoryStream, new JpegEncoder
            {
                Quality = 75,
            });

            return memoryStream.ToArray();
        }
    }
}
