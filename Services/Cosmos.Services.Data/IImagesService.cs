namespace Cosmos.Services.Data
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public interface IImagesService
    {
        Task AddImage(Stream content, string allianceId);

        Task<string> GetImageData(string id);

        // Task<FileStreamResult> GetRenderImage(string allianceId);
    }
}
