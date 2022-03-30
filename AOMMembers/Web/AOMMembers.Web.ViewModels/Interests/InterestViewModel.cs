using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Interests
{
    public class InterestViewModel : IMapFrom<Interest>
    {
        public string Id { get; set; }

        [Display(Name = InterestDescriptionDisplayName)]
        public string Description { get; set; }

        //public string WorldviewId { get; set; }
    }
}