using EgyptWalks.Core.Interfaces;
using EgyptWalks.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EgyptWalks.DAL.Repositories
{
    public class WalkRepository : IWalkRepository
    {

        EgyptWalksDbContext context;
        public WalkRepository(EgyptWalksDbContext _context)
        { 
            context = _context;
        }


        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 1000) 
        {
            var walks = context.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering:
            if (string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery) == false) 
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
                {
                    walks = walks.Where(w => w.Name.Contains(filterQuery));
                }
            }

            //Sorting:
            if (string.IsNullOrWhiteSpace(sortBy) == false) 
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (bool)isAscending ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase)) 
                {
                    walks = (bool)isAscending ? walks.OrderBy(w => w.LengthInKm) : walks.OrderByDescending(w => w.LengthInKm);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk> CreateAsync(Walk walk) 
        {
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk updatedWalk) 
        {
            Walk? existingWalk = await context.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (existingWalk != null)
            {
                existingWalk.Name = updatedWalk.Name;
                existingWalk.Description = updatedWalk.Description;
                existingWalk.LengthInKm = updatedWalk.LengthInKm;
                existingWalk.WalkImageUrl = updatedWalk.WalkImageUrl;
                existingWalk.RegionId = updatedWalk.RegionId;
                existingWalk.DifficultyId = updatedWalk.DifficultyId;

                await context.SaveChangesAsync();
                return existingWalk;
            }
            else 
            {
                return null;
            }
        }


        public async Task<Walk?> DeleteAsync(Guid id) 
        {
            Walk? existingWalk = await context.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (existingWalk != null) 
            {
                context.Walks.Remove(existingWalk);
                await context.SaveChangesAsync();
                return existingWalk;
            }
            return null;
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await context.Walks.FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}
