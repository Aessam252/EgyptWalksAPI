using Microsoft.AspNetCore.Mvc;
using EgyptWalks.Core.Models;
using EgyptWalks.Core.DTOs;
using EgyptWalks.Core;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        
        public ImagesController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDTO request) 
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid) 
            {
                Image image = new Image();

                image.File = request.File;
                image.FileName = request.FileName;
                image.FileExtension = Path.GetExtension(request.File.FileName);
                image.FileSizeInBytes = request.File.Length;
                image.FileDescription = request.FileDescription;

                await unitOfWork.Images.UploadAsync(image);

                return Ok(image);
            }
            return BadRequest(ModelState);
        }


        private void ValidateFileUpload(ImageUploadRequestDTO request) 
        {
            string[] allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName))) 
            {
                ModelState.AddModelError("File", "Unsupported File Extension");
            }

            if (request.File.Length > 10485760) 
            {
                ModelState.AddModelError("file", "File size is more than 10 mb ");
            }
        }
    }
}
