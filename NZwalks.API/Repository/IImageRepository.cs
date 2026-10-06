using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
