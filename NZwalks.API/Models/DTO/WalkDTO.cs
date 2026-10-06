using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
      
        public double LengthInkm { get; set; }



        public RegionDTO Region { get; set; }
        public DifficultyDTO Difficulty { get; set; }
    }
}
