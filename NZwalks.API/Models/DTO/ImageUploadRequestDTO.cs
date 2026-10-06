using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string Filename { get; set; }
        public string? Filedexcription{ get; set; }

    }
}
