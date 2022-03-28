using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.SocietyActivities
{
    public class SocietyActivityEditModel
    {
        public string Id { get; set; }

        [Display(Name = SocietyActivityNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SocietyActivityNameMaxLength, MinimumLength = SocietyActivityNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = SocietyActivityDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SocietyActivityDescriptionMaxLength, MinimumLength = SocietyActivityDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = SocietyActivityResultDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SocietyActivityResultMaxLength, MinimumLength = SocietyActivityResultMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Result { get; set; }

        [Display(Name = SocietyActivityEventLinkDisplayName)]
        public string? EventLink { get; set; }

        //public string CitizenId { get; set; }
    }
}