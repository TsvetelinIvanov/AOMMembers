using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.PartyMemberships
{
    public class PartyMembershipDeleteModel
    {
        public string Id { get; set; }

        [Display(Name = PartyMembershipPartyNameDisplayName)]
        public string PartyName { get; set; }

        [Display(Name = PartyMembershipDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = PartyMembershipIsCurrentDisplayName)]
        public bool IsCurrent { get; set; }

        [Display(Name = PartyMembershipStartDateDisplayName)]
        public DateTime StartDate { get; set; }

        [Display(Name = PartyMembershipEndDateDisplayName)]
        public DateTime? EndDate { get; set; }

        //public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }
    }
}