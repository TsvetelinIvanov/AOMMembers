using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.Settings
{
    public class SettingEditModel
    {
        [Display(Name = SettingNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SettingNameMaxLength, MinimumLength = SettingNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = SettingValueDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SettingValueMaxLength, MinimumLength = SettingValueMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Value { get; set; }

        //public string CitizenId { get; set; }
    }
}