using System.ComponentModel.DataAnnotations;
using AOMMembers.Web.ViewModels.Interests;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Worldviews
{
    public class WorldviewDetailsViewModel
    {
        [Display(Name = WorldviewDescriptionDisplayName)]        
        public string Description { get; set; }

        [Display(Name = WorldviewIdeologyDisplayName)]        
        public string Ideology { get; set; }

        public string CitizenId { get; set; }        

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }

        [Display(Name = WorldviewInterestsCountDisplayName)]
        public int InterestsCount { get; set; }

        public InterestListViewModel Interests { get; set; }
    }
}