using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.WorkPositions
{
    public class WorkPositionViewModel : IMapFrom<WorkPosition>
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
    }
}