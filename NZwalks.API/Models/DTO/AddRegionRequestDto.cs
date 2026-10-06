using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3 ,ErrorMessage ="Code has to be a minimum of 3 letters")]
        [MaxLength(5,ErrorMessage="code has to be max of 3 5 letters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "code has to be max of 3 5 letters")]
        public string Name { get; set; }
        public string? RegionImageurl { get; set; }
    }
}
