using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.WorkPositions
{
    public class WorkPositionInputModel
    {
        [Display(Name = WorkPositionNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(WorkPositionNameMaxLength, MinimumLength = WorkPositionNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = WorkPositionDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(WorkPositionDescriptionMaxLength, MinimumLength = WorkPositionDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = WorkPositionIsCurrentDisplayName)]
        public bool IsCurrent { get; set; }

        [Display(Name = WorkPositionStartDateDisplayName)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = WorkPositionEndDateDisplayName)]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public string CareerId { get; set; }        
    }
}