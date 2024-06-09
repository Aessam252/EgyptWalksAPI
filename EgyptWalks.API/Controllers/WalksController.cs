using Microsoft.AspNetCore.Mvc;
using EgyptWalks.API.CustomActionFilters;
using EgyptWalks.Core.Models;
using EgyptWalks.Core.DTOs;
using EgyptWalks.Core;
using Microsoft.AspNetCore.Authorization;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalksController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public WalksController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000) 
        {
            List<Walk>? walks = await unitOfWork.Walks.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            if (walks != null)
            {
                List<WalkDTO> walkDTOs = new List<WalkDTO>();

                foreach (var w in walks)
                {
                    walkDTOs.Add(new WalkDTO()
                    {
                        Id = w.Id,
                        Name = w.Name,
                        Description = w.Description,
                        LengthInKm = w.LengthInKm,
                        WalkImageUrl = w.WalkImageUrl,
                        DifficultyId = w.DifficultyId,
                        RegionId = w.RegionId,
                    });
                }
                return Ok(walkDTOs);
            }
            return NotFound();
        }


        [HttpGet]
        [Route("{id:guid}", Name ="GetById")]
        public async Task<IActionResult> GetById(Guid id) 
        {
            Walk? walk = await unitOfWork.Walks.GetByIdAsync(id);

            if (walk != null) 
            {
                WalkDTO walkDTO = new WalkDTO();

                walkDTO.Id = walk.Id;
                walkDTO.Name = walk.Name;
                walkDTO.Description = walk.Description;
                walkDTO.LengthInKm = walk.LengthInKm;
                walkDTO.WalkImageUrl = walk.WalkImageUrl;
                walkDTO.DifficultyId = walk.DifficultyId;
                walkDTO.RegionId = walk.RegionId;

                return Ok(walkDTO);
            }
            else
                return NotFound();
        }


        [HttpPost]
        [Route("Create")]
        [ValidateModel]
        public async Task<IActionResult> Create(AddWalkDTO addWalkDTO)
        {
            Walk walk = new Walk();

            walk.Id = addWalkDTO.Id;
            walk.Name = addWalkDTO.Name;
            walk.Description = addWalkDTO.Description;
            walk.LengthInKm = addWalkDTO.LengthInKm;
            walk.WalkImageUrl = addWalkDTO.WalkImageUrl;
            walk.RegionId = addWalkDTO.RegionId;
            walk.DifficultyId = addWalkDTO.DifficultyId;

            await unitOfWork.Walks.CreateAsync(walk);

            AddWalkDTO resultWalkDTO = new AddWalkDTO()
            {
                Id = addWalkDTO.Id,
                Name = walk.Name,
                Description = walk.Description,
                LengthInKm = walk.LengthInKm,
                WalkImageUrl = walk.WalkImageUrl,
                RegionId = walk.RegionId,
                DifficultyId = walk.DifficultyId
            };
            return StatusCode(201, resultWalkDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, UpdateWalkDTO updatedWalkDTO)
        {
            Walk walk = new Walk();

            walk.Name = updatedWalkDTO.Name;
            walk.Description = updatedWalkDTO.Description;
            walk.LengthInKm = updatedWalkDTO.LengthInKm;
            walk.WalkImageUrl = updatedWalkDTO.WalkImageUrl;
            walk.RegionId = updatedWalkDTO.RegionId;
            walk.DifficultyId = updatedWalkDTO.DifficultyId;

            Walk? updatedWalk = await unitOfWork.Walks.UpdateAsync(id, walk);

            if (updatedWalk == null)
            {
                return NotFound();
            }

            UpdateWalkDTO resultWalkDTO = new UpdateWalkDTO()
            {
                Name = updatedWalk.Name,
                Description = updatedWalk.Description,
                LengthInKm = updatedWalk.LengthInKm,
                WalkImageUrl = updatedWalk.WalkImageUrl,
                RegionId = updatedWalk.RegionId,
                DifficultyId = updatedWalk.DifficultyId
            };
            return Ok(resultWalkDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            Walk? existingWalk = await unitOfWork.Walks.DeleteAsync(id);

            if (existingWalk != null) 
            {
                WalkDTO walkDTO = new WalkDTO()
                {
                    Id = existingWalk.Id,
                    Name = existingWalk.Name,
                    Description = existingWalk.Description,
                    LengthInKm = existingWalk.LengthInKm,
                    WalkImageUrl = existingWalk.WalkImageUrl,
                    DifficultyId = existingWalk.DifficultyId,
                    RegionId = existingWalk.RegionId
                };
                return Ok(walkDTO);
            }
            return NotFound();  
        }

    }
}
