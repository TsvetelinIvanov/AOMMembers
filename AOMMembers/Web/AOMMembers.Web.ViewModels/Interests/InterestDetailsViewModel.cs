using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Interests
{
    public class InterestDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = InterestDescriptionDisplayName)]        
        public string Description { get; set; }

        public string WorldviewId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }
    }
}