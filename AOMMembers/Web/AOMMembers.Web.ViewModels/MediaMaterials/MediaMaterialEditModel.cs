using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.MediaMaterials
{
    public class MediaMaterialEditModel
    {
        [Display(Name = MediaMaterialKindDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MediaMaterialKindMaxLength, MinimumLength = MediaMaterialKindMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Kind { get; set; }

        [Display(Name = MediaMaterialMediaNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MediaMaterialMediaNameMaxLength, MinimumLength = MediaMaterialMediaNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string MediaName { get; set; }

        [Display(Name = MediaMaterialIssueDateDisplayName)]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Display(Name = MediaMaterialAuthorDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MediaMaterialAuthorMaxLength, MinimumLength = MediaMaterialAuthorMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Author { get; set; }

        [Display(Name = MediaMaterialHeadingDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MediaMaterialHeadingMaxLength, MinimumLength = MediaMaterialHeadingMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Heading { get; set; }

        [Display(Name = MediaMaterialDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MediaMaterialDescriptionMaxLength, MinimumLength = MediaMaterialDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = MediaMaterialMediaMaterialLinkDisplayName)]
        public string? MediaMaterialLink { get; set; }

        //public string PublicImageId { get; set; }
    }
}