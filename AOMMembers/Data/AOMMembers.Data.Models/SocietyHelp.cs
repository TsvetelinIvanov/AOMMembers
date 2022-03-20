using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class SocietyHelp : BaseDeletableModel
    {
        [Required]
        [StringLength(SocietyHelpNameMaxLength, MinimumLength = SocietyHelpNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(SocietyHelpDescriptionMaxLength, MinimumLength = SocietyHelpDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [StringLength(SocietyHelpResultMaxLength, MinimumLength = SocietyHelpResultMinLength)]
        public string Result { get; set; }

        public string? EventLink { get; set; }

        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
    }
}