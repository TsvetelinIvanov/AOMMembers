using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.Careers
{
    public class CareerInputModel
    {
        [Display(Name = CareerDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(CareerDescriptionMaxLength, MinimumLength = CareerDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = CareerCVLinkDisplayName)]
        public string? CVLink { get; set; }

        public string CitizenId { get; set; }
    }
}