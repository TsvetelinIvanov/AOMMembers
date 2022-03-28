using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Settings
{
    public class SettingDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = SettingNameDisplayName)]        
        public string Name { get; set; }

        [Display(Name = SettingValueDisplayName)]       
        public string Value { get; set; }

        public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }
    }
}