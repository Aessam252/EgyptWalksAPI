using EgyptWalks.Core.Models;
using EgyptWalks.Core.Interfaces;

namespace EgyptWalks.DAL.Repositories
{
    public class ReviewRepository: IReviewRepository
    {
        protected EgyptWalksDbContext context;

        public ReviewRepository(EgyptWalksDbContext _context)
        {
            context = _context;
        }

        public async Task<Review> Add(Review review, Guid applicationUserId)
        {
            applicationUserId = context.Entry(review).Property<Guid>("ApplicationUserId").CurrentValue = applicationUserId;

            await context.Reviews.AddAsync(review);
            context.SaveChanges();

            return review;
        }

       
    }
}
