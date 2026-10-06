using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required] 
        [Range(0,50)]
        public double LengthInkm { get; set; }
        public string? WalkImageUrl { get; set; }


        [Required]
        public Guid DifficultyId { get; set; }

        [Required]

        public Guid RegionId { get; set; }
    }
}
