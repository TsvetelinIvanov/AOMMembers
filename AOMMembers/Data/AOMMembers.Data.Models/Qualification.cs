using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class Qualification : BaseDeletableModel
    {
        [Required]
        [StringLength(QualificationNameMaxLength, MinimumLength = QualificationNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(QualificationDescriptionMaxLength, MinimumLength = QualificationDescriptionMinLength)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public string EducationId { get; set; }
        public virtual Education Education { get; set; }
    }
}