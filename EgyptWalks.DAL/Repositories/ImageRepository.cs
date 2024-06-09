using EgyptWalks.Core.Interfaces;
using EgyptWalks.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace EgyptWalks.DAL.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly EgyptWalksDbContext context;

        public ImageRepository(IHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor, EgyptWalksDbContext _context)
        {
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
            context = _context;
        }

        public async Task<Image> UploadAsync(Image image)  
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var URLFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = URLFilePath;  

            await context.Images.AddAsync(image);
            await context.SaveChangesAsync();

            return image;
        }

    }
}
