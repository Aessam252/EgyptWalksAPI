using EgyptWalks.Core.DTOs;
using EgyptWalks.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager, IConfiguration _configuration)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            configuration = _configuration;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDTO userDTO)
        {
            ApplicationUser user = new ApplicationUser();

            user.UserName = userDTO.Username;
            user.Email = userDTO.Email;
            user.Age = userDTO.Age;

            IdentityResult result = await userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                return StatusCode(201, "Your Account has been created successfully");
            }
            return BadRequest(result.Errors);
        }


        [HttpPost("AddAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterUserDTO userDTO)
        {
            ApplicationUser user = new ApplicationUser();

            user.UserName = userDTO.Username;
            user.Email = userDTO.Email;
            user.Age = userDTO.Age;

            IdentityResult result = await userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
                return StatusCode(201, "Your Account has been created successfully");
            }
            return BadRequest(result.Errors);
        }


        [HttpPost("AssignUserToRole")]
        public async Task<IActionResult> AssignUserToRole(string userName, string roleName)
        {
            ApplicationUser? user = await userManager.FindByNameAsync(userName);
            IdentityRole? role = await roleManager.FindByNameAsync(roleName);

            if(role == null) 
            {
                IdentityRole newRole = new();
                newRole.Name = roleName;
                await roleManager.CreateAsync(newRole);
            }

            if (user == null) 
            {
                return NotFound("The user name doesn't exist");
            }

            await userManager.AddToRoleAsync(user, role.Name);

            return StatusCode(201, $"The user:'{user.UserName}' has been assigned to the :'{role.Name}' role successfully");
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            ApplicationUser? existingUser = await userManager.FindByNameAsync(loginUserDTO.UserName);
            if (existingUser != null)
            {
                bool found = await userManager.CheckPasswordAsync(existingUser, loginUserDTO.Password);

                if (found)
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Name, existingUser.UserName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, existingUser.Id));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    IList<string> roles = await userManager.GetRolesAsync(existingUser);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"]));
                    SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken myToken = new JwtSecurityToken( 
                            issuer: configuration["JWT:ValidIssuer"],
                            audience: configuration["JWT:ValidAudience"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signingCredentials
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(myToken), 
                        expiration = myToken.ValidTo
                    });
                }
                return Unauthorized();
            }
            return Unauthorized();
        }

    }
}
