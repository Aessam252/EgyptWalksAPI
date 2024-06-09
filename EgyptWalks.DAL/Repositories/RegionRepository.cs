using EgyptWalks.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using EgyptWalks.Core.Models;

namespace EgyptWalks.DAL.Repositories
{
    public class RegionRepository : IRegionRepository
    {

        private readonly EgyptWalksDbContext context;
       
        public RegionRepository(EgyptWalksDbContext _context)
        {
            context = _context;
           
        }

        public async Task<List<Region>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = false, int pageNumber = 1, int pageSize = 1000)
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> CreateAsync(Region region) 
        {
            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            Region? oldRegion = await context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (oldRegion == null) 
            {
                return null;
            }

             oldRegion.Name = region.Name;
             oldRegion.Code = region.Code;
             oldRegion.RegionImageUrl = region.RegionImageUrl;

            await context.SaveChangesAsync();

            return oldRegion;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            Region? existingRegion = await context.Regions.FirstOrDefaultAsync(r => r.Id == id);
       
            if (existingRegion != null) 
            {
                context.Regions.Remove(existingRegion);
                await context.SaveChangesAsync();
                return existingRegion;
            }
            return null;
        }
    }
}
