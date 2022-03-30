using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Settings
{
    public class SettingDetailsViewModel : IMapFrom<Setting>
    {
        public string Id { get; set; }

        [Display(Name = SettingNameDisplayName)]        
        public string Name { get; set; }

        [Display(Name = SettingValueDisplayName)]       
        public string Value { get; set; }

        public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }
    }
}