using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Careers
{
    public class CareerDeleteModel
    {
        public string Id { get; set; }

        [Display(Name = CareerDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = CareerCVLinkDisplayName)]
        public string? CVLink { get; set; }

        //public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = CareerCurrentWorkPositionDisplayName)]
        public string? CurrentWorkPosition { get; set; }

        [Display(Name = CareerWorkPositionsCountDisplayName)]
        public int WorkPositionsCount { get; set; }
    }
}