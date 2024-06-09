namespace EgyptWalks.Core.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string? Comment { get; set; }
        public int Rate { get; set; }

        public Guid WalkId { get; set; }
        public string ApplicationUserId { get; set; }
        public Walk Walk { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
