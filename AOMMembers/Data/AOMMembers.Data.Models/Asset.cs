using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class Asset : BaseDeletableModel
    {
        [Required]
        [StringLength(AssetNameMaxLength, MinimumLength = AssetNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AssetDescriptionMaxLength, MinimumLength = AssetDescriptionMinLength)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Worthiness { get; set; }        

        public string MaterialStateId { get; set; }
        public virtual MaterialState MaterialState { get; set; }        
    }
}