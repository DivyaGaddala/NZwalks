using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MaxLength(4,ErrorMessage ="Code requires maimum 4 leytters")]
        [MinLength(2, ErrorMessage = "Code requires maimum 2 leytters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100,ErrorMessage ="Name has to be a max 100 letters ")]
        public string Name { get; set; }
        public string? RegionImageurl { get; set; }
    }
}
