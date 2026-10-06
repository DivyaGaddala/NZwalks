using NZwalks.API.Models.Domain;
using NZwalks.API.Data;


namespace NZwalks.API.Repository
{
    public class LocalImageRepository : IImageRepository

    {
        private readonly IWebHostEnvironment webhostEnvironment;
        private readonly IHttpContextAccessor httpcontextaccessor;
        private readonly NZwalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment  webhostEnvironment,IHttpContextAccessor  httpcontextaccessor,
          NZwalksDbContext dbContext  )
        {
            this.webhostEnvironment = webhostEnvironment;
            this.httpcontextaccessor = httpcontextaccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            // 1. Ensure the 'Images' directory exists on disk
            var imagesDirectory = Path.Combine(webhostEnvironment.ContentRootPath, "Images");
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }

            // 2. Extract ONLY the file name without its extension to avoid double extensions
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(image.Filename);

            // 3. Combine folder, filename, and extension properly
            var localFilePath = Path.Combine(imagesDirectory, $"{fileNameWithoutExtension}{image.Fileextension}");

            // 4. Create stream and write file
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // 5. Construct HTTP URL for client access
            var urlFilePath = $"{httpcontextaccessor.HttpContext.Request.Scheme}://{httpcontextaccessor.HttpContext.Request.Host}{httpcontextaccessor.HttpContext.Request.PathBase}/Images/{fileNameWithoutExtension}{image.Fileextension}";

            image.filepath = urlFilePath;

            // 6. Save metadata to database
            await dbContext.images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
    }

