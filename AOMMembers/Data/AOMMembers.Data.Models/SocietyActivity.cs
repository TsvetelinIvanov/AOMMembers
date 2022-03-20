using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class SocietyActivity : BaseDeletableModel
    {
        [Required]
        [StringLength(SocietyActivityNameMaxLength, MinimumLength = SocietyActivityNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(SocietyActivityDescriptionMaxLength, MinimumLength = SocietyActivityDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [StringLength(SocietyActivityResultMaxLength, MinimumLength = SocietyActivityResultMinLength)]
        public string Result { get; set; }

        public string? EventLink { get; set; }

        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
    }
}