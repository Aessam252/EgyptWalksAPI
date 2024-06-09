using EgyptWalks.Core.Interfaces;
using EgyptWalks.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EgyptWalks.DAL.Repositories
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly EgyptWalksDbContext context;

        public DifficultyRepository(EgyptWalksDbContext _context)
        {
            context = _context;
        }


        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            await context.Difficulties.AddAsync(difficulty);
            await context.SaveChangesAsync();

            return difficulty;
        }

        public async Task<Difficulty?> DeleteAsync(Guid id)
        {
            Difficulty existingDifficulty = await context.Difficulties.FirstOrDefaultAsync(d=>d.Id == id);

            if (existingDifficulty != null) 
            {
                context.Difficulties.Remove(existingDifficulty);
                await context.SaveChangesAsync();

                return existingDifficulty;
            }
            return null;
        }

        public async Task<List<Difficulty>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = false, int pageNumber = 1, int pageSize = 1000)
        {
            return await context.Difficulties.ToListAsync();
        }

        public async Task<Difficulty?> GetByIdAsync(Guid id)
        {
            return await context.Difficulties.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty)
        {
            Difficulty oldDifficulty = await context.Difficulties.FirstOrDefaultAsync(d => d.Id == id);

            if (oldDifficulty == null) 
            {
                return null;
            }

            oldDifficulty.Name = difficulty.Name;

            await context.SaveChangesAsync();

            return oldDifficulty;
        }
    }
}
