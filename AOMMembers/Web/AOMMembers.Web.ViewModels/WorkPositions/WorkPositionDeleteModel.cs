using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.WorkPositions
{
    public class WorkPositionDeleteModel
    {
        public string Id { get; set; }

        [Display(Name = WorkPositionNameDisplayName)]
        public string Name { get; set; }

        [Display(Name = WorkPositionDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = WorkPositionIsCurrentDisplayName)]
        public bool IsCurrent { get; set; }

        [Display(Name = WorkPositionStartDateDisplayName)]
        public DateTime StartDate { get; set; }

        [Display(Name = WorkPositionEndDateDisplayName)]
        public DateTime? EndDate { get; set; }

        //public string CareerId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }
    }
}