using System.ComponentModel.DataAnnotations;

namespace EgyptWalks.Core.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm don't match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
