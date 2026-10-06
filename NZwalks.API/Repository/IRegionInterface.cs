using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repository
{
    public interface IRegionInterface
    {
        //we need 5 definitions Actions
        Task<List<Region>> GetAllAsync();

        Task<Region?>GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id, Region reegion);

        Task<Region?> DeleteAsync(Guid id);
    }
}

