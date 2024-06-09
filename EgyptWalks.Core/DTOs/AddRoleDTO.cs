using System.ComponentModel.DataAnnotations;

namespace EgyptWalks.Core.DTOs
{
    public class AddRoleDTO
    {
        [Required]
        public string RoleName { get; set; }
    }
}
