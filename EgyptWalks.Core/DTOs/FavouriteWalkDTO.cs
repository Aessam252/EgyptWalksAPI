namespace EgyptWalks.Core.DTOs
{
    public class FavouriteWalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid WalkId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
