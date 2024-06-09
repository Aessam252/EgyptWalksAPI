using Microsoft.AspNetCore.Mvc;
using EgyptWalks.Core.Models;
using EgyptWalks.Core.DTOs;
using EgyptWalks.Core;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteWalksController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public FavouriteWalksController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetFavouriteWalks() 
        {
            List<FavouriteWalkDTO> favWalksDTO = new List<FavouriteWalkDTO>();

            List<FavouriteWalk> favWalks = await unitOfWork.FavoriteWalks.GetFavouriteWalksAsync();

            foreach (var f in favWalks) 
            {
                favWalksDTO.Add(
                    new FavouriteWalkDTO() 
                    {
                        Id = f.Id,
                        Name = f.Name,
                        WalkId = f.WalkId,
                        ApplicationUserId = f.ApplicationUserId
                    }
                    );
            }
            return Ok( favWalksDTO );
        }

        [HttpPost]
        public async Task<IActionResult> AddFavouriteWalkAsync(AddFavouriteWalkDTO favWalkDTO) 
        {
            FavouriteWalk favWalk = new FavouriteWalk();

            favWalk.Id = favWalkDTO.Id;
            favWalk.Name = favWalkDTO.Name;
            favWalk.WalkId = favWalkDTO.WalkId;
            favWalk.ApplicationUserId = favWalkDTO.ApplicationUserId;

            await unitOfWork.FavoriteWalks.AddFavouriteWalkAsync(favWalk);

            return Ok(favWalkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> RemoveFavouriteWalkAsync(Guid id) 
        {
            FavouriteWalk favWalk = await unitOfWork.FavoriteWalks.RemoveFavouriteWalkAsync(id);

            if (favWalk != null) 
            {
                FavouriteWalkDTO favWalkDTO = new FavouriteWalkDTO();

                favWalkDTO.WalkId = favWalk.Id;
                favWalkDTO.Name = favWalk.Name;
                favWalkDTO.WalkId = favWalk.WalkId;
                favWalkDTO.ApplicationUserId = favWalk.ApplicationUserId;

                return Ok(favWalkDTO);
            }
            return BadRequest();
        } 
    }
}
