using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.MaterialStates
{
    public class MaterialStateInputModel
    {
        [Display(Name = MaterialStateRichesDisplayName)]
        public decimal Riches { get; set; }

        [Display(Name = MaterialStateMoneyDisplayName)]
        public decimal Money { get; set; }

        [Display(Name = MaterialStateMonthIncomeDisplayName)]
        public decimal MonthIncome { get; set; }

        [Display(Name = MaterialStateDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MaterialStateDescriptionMaxLength, MinimumLength = MaterialStateDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = MaterialStateTaxDeclarationLinkDisplayName)]
        public string? TaxDeclarationLink { get; set; }

        public string CitizenId { get; set; }       
    }
}