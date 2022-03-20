using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class PartyPosition : BaseDeletableModel
    {
        [Required]
        [StringLength(PartyPositionNameMaxLength, MinimumLength = PartyPositionNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PartyPositionDescriptionMaxLength, MinimumLength = PartyPositionDescriptionMinLength)]
        public string Description { get; set; }

        public bool IsCurrent { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public string MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}