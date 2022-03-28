using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.Qualifications
{
    public class QualificationInputModel
    {
        [Display(Name = QualificationNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(QualificationNameMaxLength, MinimumLength = QualificationNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = QualificationDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(QualificationDescriptionMaxLength, MinimumLength = QualificationDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = QualificationStartDateDisplayName)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = QualificationEndDateDisplayName)]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        //public string EducationId { get; set; }
    }
}