namespace EgyptWalks.Core.Models
{
    public class FavouriteWalk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid WalkId { get; set; }
        public string ApplicationUserId { get; set; }

        public Walk Walk { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
