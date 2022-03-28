using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.SocietyHelps
{
    public class SocietyHelpDeleteModel
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

        //public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }
    }
}