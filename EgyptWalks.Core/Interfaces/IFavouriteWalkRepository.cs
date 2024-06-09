using EgyptWalks.Core.Models;

namespace EgyptWalks.Core.Interfaces
{
    public interface IFavouriteWalkRepository
    {
        Task<List<FavouriteWalk>> GetFavouriteWalksAsync();
        Task<FavouriteWalk> AddFavouriteWalkAsync(FavouriteWalk favWalk);
        Task<FavouriteWalk> RemoveFavouriteWalkAsync(Guid id);
    }
}
