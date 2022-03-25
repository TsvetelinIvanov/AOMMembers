using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Interests
{
    public class InterestDeleteModel
    {
        [Display(Name = InterestDescriptionDisplayName)]
        public string Description { get; set; }

        //public string WorldviewId { get; set; }
    }
}