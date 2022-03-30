using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.SocietyActivities
{
    public class SocietyActivityDetailsViewModel : IMapFrom<SocietyActivity>
    {
        public string Id { get; set; }

        [Display(Name = SocietyActivityNameDisplayName)]        
        public string Name { get; set; }

        [Display(Name = SocietyActivityDescriptionDisplayName)]        
        public string Description { get; set; }

        [Display(Name = SocietyActivityResultDisplayName)]        
        public string Result { get; set; }

        [Display(Name = SocietyActivityEventLinkDisplayName)]
        public string? EventLink { get; set; }

        public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }
    }
}