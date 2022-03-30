using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.SocietyHelps
{
    public class SocietyHelpDetailsViewModel : IMapFrom<SocietyHelp>
    {
        public string Id { get; set; }

        [Display(Name = SocietyHelpNameDisplayName)]        
        public string Name { get; set; }

        [Display(Name = SocietyHelpDescriptionDisplayName)]        
        public string Description { get; set; }

        [Display(Name = SocietyHelpResultDisplayName)]        
        public string Result { get; set; }

        [Display(Name = SocietyHelpEventLinkDisplayName)]
        public string? EventLink { get; set; }

        public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }
    }
}