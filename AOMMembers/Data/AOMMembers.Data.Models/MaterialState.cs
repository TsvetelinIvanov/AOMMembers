using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class MaterialState : BaseDeletableModel
    {
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Riches { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Money { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MonthIncome { get; set; }

        [Required]
        [StringLength(MaterialStateDescriptionMaxLength, MinimumLength = MaterialStateDescriptionMinLength)]        
        public string Description { get; set; }

        public string? TaxDeclarationLink { get; set; }

        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }

        public virtual ICollection<Asset> Assets { get; set; } = new HashSet<Asset>();        
    }
}