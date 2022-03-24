using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Assets
{
    public class AssetDetailsViewModel
    {
        [Display(Name = AssetNameDisplayName)]
        public string Name { get; set; }

        [Display(Name = AssetDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = AssetWorthinessDisplayName)]
        public decimal Worthiness { get; set; }

        //public string MaterialStateId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }
    }
}