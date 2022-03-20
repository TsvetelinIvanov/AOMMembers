using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class LawState : BaseDeletableModel
    {
        [Required]
        [StringLength(LawStateConditionMaxLength, MinimumLength = LawStateConditionMinLength)]
        public string Condition { get; set; }        

        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }

        public virtual ICollection<LawProblem> LawProblems { get; set; } = new HashSet<LawProblem>();        
    }
}