using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class WorkPosition : BaseDeletableModel
    {
        [Required]
        [StringLength(WorkPositionNameMaxLength, MinimumLength = WorkPositionNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(WorkPositionDescriptionMaxLength, MinimumLength = WorkPositionDescriptionMinLength)]
        public string Description { get; set; }

        public bool IsCurrent { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public string CareerId { get; set; }
        public virtual Career Career { get; set; }
    }
}