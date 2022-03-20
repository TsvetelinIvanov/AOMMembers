using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class LawProblem : BaseDeletableModel
    {
        [Required]
        [StringLength(LawProblemDescriptionMaxLength, MinimumLength = LawProblemDescriptionMinLength)]
        public string Description { get; set; }

        public string? LawProblemLink { get; set; }

        public string LawStateId { get; set; }
        public virtual LawState LawState { get; set; }        
    }
}