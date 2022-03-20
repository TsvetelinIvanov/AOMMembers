using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class Interest : BaseDeletableModel
    {
        [Required]
        [StringLength(InterestDescriptionMaxLength, MinimumLength = InterestDescriptionMinLength)]
        public string Description { get; set; }
        
        public string WorldviewId { get; set; }
        public virtual Worldview Worldview { get; set; }        
    }
}