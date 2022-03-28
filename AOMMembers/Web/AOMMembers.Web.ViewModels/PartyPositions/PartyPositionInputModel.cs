using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.PartyPositions
{
    public class PartyPositionInputModel
    {
        [Display(Name = PartyPositionNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(PartyPositionNameMaxLength, MinimumLength = PartyPositionNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = PartyPositionDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(PartyPositionDescriptionMaxLength, MinimumLength = PartyPositionDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = PartyPositionIsCurrentDisplayName)]
        public bool IsCurrent { get; set; }

        [Display(Name = PartyPositionStartDateDisplayName)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = PartyPositionEndDateDisplayName)]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        //public string MemberId { get; set; }
    }
}