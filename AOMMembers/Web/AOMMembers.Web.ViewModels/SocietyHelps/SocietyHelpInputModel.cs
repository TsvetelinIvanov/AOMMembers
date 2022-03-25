using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.SocietyHelps
{
    public class SocietyHelpInputModel
    {
        [Display(Name = SocietyHelpNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SocietyHelpNameMaxLength, MinimumLength = SocietyHelpNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = SocietyHelpDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SocietyHelpDescriptionMaxLength, MinimumLength = SocietyHelpDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = SocietyHelpResultDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SocietyHelpResultMaxLength, MinimumLength = SocietyHelpResultMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Result { get; set; }

        [Display(Name = SocietyHelpEventLinkDisplayName)]
        public string? EventLink { get; set; }

        public string CitizenId { get; set; }
    }
}