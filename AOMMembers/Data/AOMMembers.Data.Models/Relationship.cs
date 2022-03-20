using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class Relationship : BaseDeletableModel
    {
        [Required]
        [StringLength(RelationshipKindMaxLength, MinimumLength = RelationshipKindMinLength)]
        public string Kind { get; set; }

        [Required]
        [StringLength(RelationshipDescriptionMaxLength, MinimumLength = RelationshipDescriptionMinLength)]
        public string Description { get; set; }        

        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
    }
}