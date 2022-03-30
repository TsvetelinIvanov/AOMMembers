using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.PartyMemberships
{
    public class PartyMembershipViewModel : IMapFrom<PartyMembership>
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
    }
}