using System.ComponentModel.DataAnnotations;
using AOMMembers.Web.ViewModels.WorkPositions;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Careers
{
    public class CareerDetailsViewModel
    {
        [Display(Name = CareerDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = CareerCVLinkDisplayName)]
        public string? CVLink { get; set; }

        //public string CitizenId { get; set; }

        [Display(Name = CareerCurrentWorkPositionDisplayName)]
        public string? CurrentWorkPosition { get; set; }

        [Display(Name = CareerWorkPositionsCountDisplayName)]
        public int WorkPositionsCount { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }

        public WorkPositionListViewModel WorkPositions { get; set; }
    }
}