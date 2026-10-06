using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Data
{
    public class NZwalksDbContext : DbContext
    {
        public NZwalksDbContext(DbContextOptions<NZwalksDbContext> dbcontextoptions)
            : base(dbcontextoptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> images{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seed the Dta for Difficulties
            //Easy,Medium,Difficulties

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                     DifficultyId =Guid.Parse("2b91db42-8de6-4fa7-8dd4-3ca6a33b169d"),
                    Name="Easy"

                },
                  new Difficulty()
                {
                     DifficultyId =Guid.Parse("eed3ae4a-5e16-4d2d-a11b-a98f33d6430c"),
                    Name="Medium"

                },
                    new Difficulty()
                {
                    DifficultyId =Guid.Parse("9b6b8135-fe9f-4e78-9e87-fe4ff654dd5c"),
                    Name="Difficult"

                }


        };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed  data for Regions
            var regions = new List<Region>()
            {
                new Region
                {
                    Id=Guid.Parse("e1648d68-c6fb-4342-a0b1-2d46fe23ee30"),
                    Name="Auckland",
                    Code="AKL",
                    RegionImageurl="https://www.pexels.com/video/stunning-aerial-view-of-lake-wolfgang-austria-39144011/",

                },
                 new Region
                {
                    Id=Guid.Parse("c04b8d78-ccc8-4f78-8f99-13fff48dadda"),
                    Name="Willingston",
                    Code="WLG",
                    RegionImageurl="https://images.pexels.com/photos/17824133/pexels-photo-17824133.jpeg",

                },
                  new Region
                {
                    Id=Guid.Parse("ea1013fe-678d-4eec-86ac-991a77066dc5"),
                    Name="Nelson",
                    Code="NLS",
                    RegionImageurl="https://images.pexels.com/photos/32971907/pexels-photo-32971907.jpeg",

                },

                   new Region
                {
                    Id=Guid.Parse("5557a8df-6337-46df-848b-0064d8198282"),
                    Name="QueensTown",
                    Code="QTW",
                    RegionImageurl="https://images.pexels.com/photos/33981213/pexels-photo-33981213.jpeg",

                }




        };

            modelBuilder.Entity<Region>().HasData(regions);

    }
    }
}