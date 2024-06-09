using System.ComponentModel.DataAnnotations;

namespace EgyptWalks.Core.DTOs
{
    public class AddRegionDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Code should be at least 3 charachters")]
        [MaxLength(3, ErrorMessage = "Code should be at most 3 charachters")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
