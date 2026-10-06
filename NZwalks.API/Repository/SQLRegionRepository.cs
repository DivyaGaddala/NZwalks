using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZwalks.API.Repository
{
    public class SQLRegionRepository : IRegionInterface
    {
        private readonly NZwalksDbContext dbContext;    
        //contructor injection
        public SQLRegionRepository(NZwalksDbContext dbContext)
        {
            this.dbContext=dbContext; 
        }

      
        public async Task<List<Region>> GetAllAsync()
        {
           return await dbContext.Regions.ToListAsync();
        }

        public async  Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id==id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region reegion)
        {
           var existingRegion= await dbContext.Regions.FirstOrDefaultAsync(x =>x.Id==id);

            if(existingRegion==null)
            {
                return null;
            }
            existingRegion.Code=reegion.Code;
            existingRegion.Name=reegion.Name;
            existingRegion.RegionImageurl=reegion.RegionImageurl;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion= await dbContext.Regions.FirstOrDefaultAsync(x =>x.Id==id);
            if (existingRegion == null)
            {
                return null;
            }
             dbContext.Regions.Remove(existingRegion);//remove is not an Asymnc method so we cant use await keyword
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
