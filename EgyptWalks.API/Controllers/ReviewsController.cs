using Microsoft.AspNetCore.Mvc;
using EgyptWalks.Core.Models;
using EgyptWalks.Core.DTOs;
using EgyptWalks.Core;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ReviewsController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewDTO addReviewDTO) 
        {
            Review review = new Review();

            review.Comment = addReviewDTO.Comment;
            review.Rate = addReviewDTO.Rate;
            review.WalkId = addReviewDTO.WalkId;

            await unitOfWork.Reviews.Add(review, addReviewDTO.ApplicationUserId);

            return Ok(review);
        }
    }
}
