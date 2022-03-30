using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Assets
{
    public class AssetDetailsViewModel : IMapFrom<Asset>
    {
        public string Id { get; set; }

        [Display(Name = AssetNameDisplayName)]
        public string Name { get; set; }

        [Display(Name = AssetDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = AssetWorthinessDisplayName)]
        public decimal Worthiness { get; set; }

        public string MaterialStateId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }
    }
}