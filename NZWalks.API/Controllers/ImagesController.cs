using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        // API: api/images/upload
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Image imageDomainModel = new Image()
            {
                File = request.File,
                FileExtension = Path.GetExtension(request.File.FileName),
                FileSizeInBytes = request.File.Length,
                FileName = request.FileName,
                FileDescription = request.FileDescription
            };

            Image image = await imageRepository.Upload(imageDomainModel);
            return Ok(image);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            string[] allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("File", "Invalid file extension. Only .jpg, .jpeg, .png are allowed.");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size must be less than 10MB.");
            }

            if (string.IsNullOrWhiteSpace(request.FileName))
            {
                ModelState.AddModelError("FileName", "File name is required.");
            }

        }
    }
}
