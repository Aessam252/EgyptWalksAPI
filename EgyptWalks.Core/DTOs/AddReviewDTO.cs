namespace EgyptWalks.Core.DTOs
{
    public class AddReviewDTO
    {
        public string? Comment { get; set; }

        public int Rate { get; set; }

        public Guid WalkId { get; set; }
        public Guid ApplicationUserId { get; set; }
    }
}
