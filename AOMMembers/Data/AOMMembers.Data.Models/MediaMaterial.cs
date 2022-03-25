using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class MediaMaterial : BaseDeletableModel
    {
        [Required]
        [StringLength(MediaMaterialKindMaxLength, MinimumLength = MediaMaterialKindMinLength)]
        public string Kind { get; set; }

        [Required]
        [StringLength(MediaMaterialMediaNameMaxLength, MinimumLength = MediaMaterialMediaNameMinLength)]
        public string MediaName { get; set; }

        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Required]
        [StringLength(MediaMaterialAuthorMaxLength, MinimumLength = MediaMaterialAuthorMinLength)]
        public string Author { get; set; }

        [Required]
        [StringLength(MediaMaterialHeadingMaxLength, MinimumLength = MediaMaterialHeadingMinLength)]
        public string Heading { get; set; }

        [Required]
        [StringLength(MediaMaterialDescriptionMaxLength, MinimumLength = MediaMaterialDescriptionMinLength)]
        public string Description { get; set; }

        public string? MediaMaterialLink { get; set; }

        public string PublicImageId { get; set; }
        public virtual PublicImage PublicImage { get; set; }
    }
}