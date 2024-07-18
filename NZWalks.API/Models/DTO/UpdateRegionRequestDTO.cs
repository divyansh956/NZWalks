using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must be of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code must be of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
