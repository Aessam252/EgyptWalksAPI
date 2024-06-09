using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EgyptWalks.API.CustomActionFilters;
using EgyptWalks.Core.Models;
using EgyptWalks.Core.DTOs;
using EgyptWalks.Core;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy ="Admin")]
    public class RegionsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
       
        public RegionsController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Region> regions = await unitOfWork.Region.GetAllAsync();

            if (regions != null)
            {
                List<RegionDTO> regionsDTO = new List<RegionDTO>();
                foreach (var r in regions)
                {
                    regionsDTO.Add(new RegionDTO()
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Code = r.Code,
                        RegionImageUrl = r.RegionImageUrl
                    });
                }
                return Ok(regionsDTO);
            }
            return NotFound();
        }


        [HttpGet]
        [Route("{id:guid}", Name = "(GetById")]
        public async Task<IActionResult> GetById(Guid id) 
        {
            Region? region = await unitOfWork.Region.GetByIdAsync(id);

            if (region != null) 
            {
                RegionDTO regionDTO = new RegionDTO();

                regionDTO.Id = id;
                regionDTO.Name = region.Name;
                regionDTO.Code = region.Code;
                regionDTO.RegionImageUrl = region.RegionImageUrl;
                return Ok(regionDTO);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("Create")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionDTO addRegionDTO)
        {
            Region region = new Region();

            region.Id = addRegionDTO.Id;
            region.Name = addRegionDTO.Name;
            region.Code = addRegionDTO.Code;
            region.RegionImageUrl = addRegionDTO.RegionImageUrl;

            await unitOfWork.Region.CreateAsync(region);

            AddRegionDTO resultRegionDTO = new AddRegionDTO()
            {
                Id = addRegionDTO.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };
            return StatusCode(201, resultRegionDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updatedRegionDTO)
        {
            Region region = new Region();

            region.Name = updatedRegionDTO.Name;
            region.Code = updatedRegionDTO.Code;
            region.RegionImageUrl = updatedRegionDTO.RegionImageUrl;

            Region? updatedRegion = await unitOfWork.Region.UpdateAsync(id, region);

            if (updatedRegion == null)
            {
                return NotFound();
            }

            UpdateRegionDTO resultRegionDTO = new UpdateRegionDTO()
            {
                Name = updatedRegion.Name,
                Code = updatedRegion.Code,
                RegionImageUrl = updatedRegion.RegionImageUrl
            };

            return Ok(resultRegionDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Region region = await unitOfWork.Region.DeleteAsync(id);

            if (region != null) 
            {
                RegionDTO regionDTO = new RegionDTO() 
                {
                    Id = region.Id,
                    Name= region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                };
                return Ok(regionDTO);
            }
            return NotFound();
        }
    }
}
