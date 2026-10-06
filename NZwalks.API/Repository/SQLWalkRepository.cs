using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using NZwalks.API.Repository;

namespace NZwalks.API.Repository
{
    public class SQLWalkRepository : IWalkRepository

    {
        public NZwalksDbContext dbContext;
        public SQLWalkRepository(NZwalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }



        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,string?sortBy =null,bool?isAscending=true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            
            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
                    {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
             
            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (isAscending ?? true) ? walks.OrderBy(x=>x.Name):walks.OrderByDescending(x=>x.Name);
                }
                else if (sortBy.Equals("Lenth", StringComparison.OrdinalIgnoreCase)|| sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (isAscending??true) ?walks.OrderBy(x=>x.LengthInkm ) : walks.OrderByDescending(x=>x.LengthInkm);
                }
            }

            //pagination
            var skipresults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipresults).Take(pageSize).ToListAsync();
        }


        public async Task<Walk?> GetByIdAsync(Guid id)
        {

            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingwalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingwalk == null)
            {
                return null;
            }
            existingwalk.Name = walk.Name;
            existingwalk.LengthInkm = walk.LengthInkm;
            existingwalk.DifficultyId = walk.DifficultyId;
            existingwalk.RegionId = walk.RegionId;

            dbContext.SaveChangesAsync();
            return existingwalk;


        }



        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var deletedomainmodel = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (deletedomainmodel == null)
            {
                return null;
            }
            dbContext.Walks.Remove(deletedomainmodel);
            await dbContext.SaveChangesAsync();
            return deletedomainmodel;
        }

        
    }
}