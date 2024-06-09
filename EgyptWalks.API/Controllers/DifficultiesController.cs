using EgyptWalks.Core;
using EgyptWalks.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public DifficultiesController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            return Ok(await unitOfWork.Difficulty.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id) 
        {
            Difficulty difficulty = await unitOfWork.Difficulty.GetByIdAsync(id);
            
            return Ok(difficulty);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Difficulty difficulty) 
        {
            return StatusCode(201, await unitOfWork.Difficulty.CreateAsync(difficulty));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Guid id, Difficulty oldDifficulty) 
        {
            return Ok(await unitOfWork.Difficulty.UpdateAsync(id, oldDifficulty));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id) 
        {
            return Ok(unitOfWork.Difficulty.DeleteAsync(id));
        }


    }
}
