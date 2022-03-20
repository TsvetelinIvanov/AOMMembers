using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class Education : BaseDeletableModel
    {
        [Required]
        [StringLength(EducationDescriptionMaxLength, MinimumLength = EducationDescriptionMinLength)]
        public string Description { get; set; }

        public string? CVLink { get; set; }

        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }

        public virtual ICollection<Qualification> Qualifications { get; set; } = new HashSet<Qualification>();
    }
}