using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class PartyMembership : BaseDeletableModel
    {
        [Required]
        [StringLength(PartyMembershipPartyNameMaxLength, MinimumLength = PartyMembershipPartyNameMinLength)]
        public string PartyName { get; set; }

        [Required]
        [StringLength(PartyMembershipDescriptionMaxLength, MinimumLength = PartyMembershipDescriptionMinLength)]
        public string Description { get; set; }

        public bool IsCurrent { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
    }
}