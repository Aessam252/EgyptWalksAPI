using EgyptWalks.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using EgyptWalks.Core.Models;

namespace EgyptWalks.DAL.Repositories
{
    public class FavouriteWalkRepository : IFavouriteWalkRepository
    {
        private readonly EgyptWalksDbContext context;

        public FavouriteWalkRepository(EgyptWalksDbContext _context) 
        {
            context = _context;
        }


        public async Task<List<FavouriteWalk>> GetFavouriteWalksAsync()
        {
            return await context.FavouriteWalks.ToListAsync();
        }

        public async Task<FavouriteWalk> AddFavouriteWalkAsync(FavouriteWalk favWalk)
        {
            await context.FavouriteWalks.AddAsync(favWalk);
            await context.SaveChangesAsync();

            return favWalk;
        }

        public async Task<FavouriteWalk> RemoveFavouriteWalkAsync(Guid id)
        {
            FavouriteWalk favWalk = await context.FavouriteWalks.FirstOrDefaultAsync(f => f.Id == id);

            if (favWalk != null)
            {
                context.FavouriteWalks.Remove(favWalk);
            }
            return null;
        }
    }
}
