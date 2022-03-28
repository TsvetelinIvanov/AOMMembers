using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.LawProblems
{
    public class LawProblemEditModel
    {
        public string Id { get; set; }

        [Display(Name = LawProblemDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(LawProblemDescriptionMaxLength, MinimumLength = LawProblemDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = LawProblemLawProblemLinkDisplayName)]
        public string? LawProblemLink { get; set; }

        //public string LawStateId { get; set; }
    }
}