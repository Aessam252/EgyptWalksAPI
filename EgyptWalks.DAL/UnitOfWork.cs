using EgyptWalks.Core;
using EgyptWalks.Core.Interfaces;
using EgyptWalks.Core.Models;
using EgyptWalks.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace EgyptWalks.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EgyptWalksDbContext context;

        public IGenericRepository<Walk> Walks { get; private set; }
        public IGenericRepository<Region> Region { get; private set; }
        public IGenericRepository<Difficulty> Difficulty { get; private set; }
        public IReviewRepository Reviews { get; private set; }
        public IFavouriteWalkRepository FavoriteWalks { get; private set; }
        public IImageRepository Images { get; private set; }

        public UnitOfWork(IHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor, EgyptWalksDbContext _context)
        {
            context = _context;
            Walks = new WalkRepository(context);
            Region = new RegionRepository(context);
            Difficulty = new DifficultyRepository(context);
            Reviews = new ReviewRepository(context);
            FavoriteWalks = new FavouriteWalkRepository(context);
            Images = new ImageRepository(_webHostEnvironment, _httpContextAccessor, context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
