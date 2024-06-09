using EgyptWalks.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EgyptWalks.DAL
{
    public class EgyptWalksDbContext : IdentityDbContext<ApplicationUser>
    {
        public EgyptWalksDbContext() { }

        public EgyptWalksDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<FavouriteWalk> FavouriteWalks { get; set; }
    }
}
