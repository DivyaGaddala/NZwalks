using System.ComponentModel.DataAnnotations.Schema;

namespace NZwalks.API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string Filename { get; set; }
        public string? Filedescription { get; set; }

        public string Fileextension { get; set; }
        public long FilesizeinBytes { get; set; }

        public string filepath { get; set; }
    }
}
