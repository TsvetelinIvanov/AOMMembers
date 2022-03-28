using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.Worldviews
{
    public class WorldviewInputModel
    {
        [Display(Name = WorldviewDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(WorldviewDescriptionMaxLength, MinimumLength = WorldviewDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = WorldviewIdeologyDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(WorldviewIdeologyMaxLength, MinimumLength = WorldviewIdeologyMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Ideology { get; set; }

        //public string CitizenId { get; set; }
    }
}