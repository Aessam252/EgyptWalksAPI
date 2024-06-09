using Microsoft.AspNetCore.Identity;

namespace EgyptWalks.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Age { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
