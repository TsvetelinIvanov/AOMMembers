using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.Educations
{
    public class EducationInputModel
    {
        [Display(Name = EducationDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(EducationDescriptionMaxLength, MinimumLength = EducationDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = EducationCVLinkDisplayName)]
        public string? CVLink { get; set; }

        public string CitizenId { get; set; }
    }
}