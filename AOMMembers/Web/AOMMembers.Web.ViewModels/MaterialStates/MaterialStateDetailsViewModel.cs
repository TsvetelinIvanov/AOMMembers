using System.ComponentModel.DataAnnotations;
using AOMMembers.Web.ViewModels.Assets;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.MaterialStates
{
    public class MaterialStateDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = MaterialStateRichesDisplayName)]
        public decimal Riches { get; set; }

        [Display(Name = MaterialStateMoneyDisplayName)]
        public decimal Money { get; set; }

        [Display(Name = MaterialStateMonthIncomeDisplayName)]
        public decimal MonthIncome { get; set; }

        [Display(Name = MaterialStateDescriptionDisplayName)]        
        public string Description { get; set; }

        [Display(Name = MaterialStateTaxDeclarationLinkDisplayName)]
        public string? TaxDeclarationLink { get; set; }

        public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = MaterialStateAssetsCountDisplayName)]
        public int AssetsCount { get; set; }

        public HashSet<AssetViewModel> Assets { get; set; }
    }
}