using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class Setting : BaseDeletableModel
    {
        [Required]
        [StringLength(SettingNameMaxLength, MinimumLength = SettingNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(SettingValueMaxLength, MinimumLength = SettingValueMinLength)]
        public string Value { get; set; }
        
        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
    }
}