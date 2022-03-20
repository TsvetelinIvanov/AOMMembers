using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Common.Models
{
    public abstract class BaseModel : IAuditInfo
    {
        [Key]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}