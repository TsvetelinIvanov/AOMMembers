using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.Assets
{
    public class AssetInputModel
    {
        [Display(Name = AssetNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(AssetNameMaxLength, MinimumLength = AssetNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = AssetDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(AssetDescriptionMaxLength, MinimumLength = AssetDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = AssetWorthinessDisplayName)]
        public decimal Worthiness { get; set; }

        public string MaterialStateId { get; set; }
    }
}