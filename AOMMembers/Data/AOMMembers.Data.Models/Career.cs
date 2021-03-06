using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class Career : BaseDeletableModel
    {
        [Required]
        [StringLength(CareerDescriptionMaxLength, MinimumLength = CareerDescriptionMinLength)]
        public string Description { get; set; }

        public string? CVLink { get; set; }

        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }

        public virtual ICollection<WorkPosition> WorkPositions { get; set; } = new HashSet<WorkPosition>();
    }
}