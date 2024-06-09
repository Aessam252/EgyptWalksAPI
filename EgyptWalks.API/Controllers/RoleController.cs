using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EgyptWalks.Core.DTOs;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> _roleManager) 
        {
            roleManager = _roleManager;
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(AddRoleDTO roleDTO) 
        {
            IdentityRole role = new IdentityRole();
            role.Name = roleDTO.RoleName;

            IdentityResult result = await roleManager.CreateAsync(role);

            if (result.Succeeded) 
            {
                return StatusCode(201, "Role is created successfully");
            }
            return BadRequest();
        }
    }
}
