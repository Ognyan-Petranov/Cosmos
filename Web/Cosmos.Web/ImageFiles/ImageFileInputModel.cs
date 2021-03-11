namespace Cosmos.Web.ViewModels.ImageFiles
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    
    using Microsoft.AspNetCore.Http;

    public class ImageFileInputModel
    {
        public string Name { get; set; }

        [Display(Name = "Upload New Image")]
        public IFormFile AllianceImage { get; set; }

        public Stream Content { get; set; }

        public string PlayerId { get; set; }

        public string AllianceId { get; set; }
    }
}
