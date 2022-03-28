using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.LawStates
{
    public class LawStateInputModel
    {
        [Display(Name = LawStateConditionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(LawStateConditionMaxLength, MinimumLength = LawStateConditionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Condition { get; set; }

        //public string CitizenId { get; set; }
    }
}