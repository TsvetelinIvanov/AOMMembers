using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.PartyMemberships
{
    public class PartyMembershipEditModel
    {
        public string Id { get; set; }

        [Display(Name = PartyMembershipPartyNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(PartyMembershipPartyNameMaxLength, MinimumLength = PartyMembershipPartyNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string PartyName { get; set; }

        [Display(Name = PartyMembershipDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(PartyMembershipDescriptionMaxLength, MinimumLength = PartyMembershipDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = PartyMembershipIsCurrentDisplayName)]
        public bool IsCurrent { get; set; }

        [Display(Name = PartyMembershipStartDateDisplayName)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = PartyMembershipEndDateDisplayName)]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        //public string CitizenId { get; set; }
    }
}