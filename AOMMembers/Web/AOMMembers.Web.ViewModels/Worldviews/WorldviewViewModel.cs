using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Worldviews
{
    public class WorldviewViewModel : IMapFrom<Worldview>
    {
        public string Id { get; set; }

        [Display(Name = WorldviewDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = WorldviewIdeologyDisplayName)]
        public string Ideology { get; set; }

        public string CitizenId { get; set; }        

        [Display(Name = WorldviewInterestsCountDisplayName)]
        public int InterestsCount { get; set; }
    }
}