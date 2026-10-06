using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Models.Domain;
using NZwalks.API.Repository;


namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imagerepository;

        public ImagesController(IImageRepository imagerepository)
        {
            this.imagerepository = imagerepository;
        }
        //POS/api/images/upload
        [HttpPost]
        [Route("Upload")]
       public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO imageuploadrequestdto)
        {
             ValidateFileUpload(imageuploadrequestdto);
            
            if(ModelState.IsValid)
            {
                //DTO - Domain
                var imagedomainmodel = new Image
                {
                    File = imageuploadrequestdto.File,
                    Fileextension = Path.GetExtension(imageuploadrequestdto.File.FileName),
                    FilesizeinBytes = imageuploadrequestdto.File.Length,
                    Filename = Path.GetFileNameWithoutExtension(imageuploadrequestdto.Filename?? imageuploadrequestdto.File.FileName),
                    Filedescription = imageuploadrequestdto.Filedexcription,
                    filepath=string.Empty


                };

                //Use repository to uploda image
                await imagerepository.Upload(imagedomainmodel);
                return Ok(imagedomainmodel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO imageuploadrequestdto)
        {
            //image extension validation
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtensions.Contains(Path.GetExtension(imageuploadrequestdto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
           
            //Checking the size of the file
            if(imageuploadrequestdto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size is more than 10MB,please upload a smaller size file");
            }

        }
    }
}
