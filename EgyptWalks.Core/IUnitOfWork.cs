using EgyptWalks.Core.Interfaces;
using EgyptWalks.Core.Models;

namespace EgyptWalks.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Walk> Walks { get; }
        IGenericRepository<Region> Region { get; }
        IGenericRepository<Difficulty> Difficulty { get; }
        IReviewRepository Reviews { get; }
        IFavouriteWalkRepository FavoriteWalks { get; }
        IImageRepository Images { get; }

        int Complete();
    }
}
