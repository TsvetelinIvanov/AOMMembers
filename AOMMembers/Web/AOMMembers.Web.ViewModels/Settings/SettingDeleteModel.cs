using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Settings
{
    public class SettingDeleteModel
    {
        [Display(Name = SettingNameDisplayName)]
        public string Name { get; set; }

        [Display(Name = SettingValueDisplayName)]
        public string Value { get; set; }

        //public string CitizenId { get; set; }
    }
}