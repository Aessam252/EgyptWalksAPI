using EgyptWalks.Core.Models;

namespace EgyptWalks.Core.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> UploadAsync(Image image);
    }
}
