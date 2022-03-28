using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Interests
{
    public class InterestViewModel
    {
        public string Id { get; set; }

        [Display(Name = InterestDescriptionDisplayName)]
        public string Description { get; set; }

        //public string WorldviewId { get; set; }
    }
}