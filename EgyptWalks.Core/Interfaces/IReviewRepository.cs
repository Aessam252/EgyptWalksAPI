using EgyptWalks.Core.Models;

namespace EgyptWalks.Core.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> Add(Review review, Guid applicationUserId);
    }
}
