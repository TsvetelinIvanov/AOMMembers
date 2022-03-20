using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class PublicImage : BaseDeletableModel
    {
        public int? Rating { get; set; }

        public string MemberId { get; set; }
        public virtual Member Member { get; set; }

        public virtual ICollection<MediaMaterial> MediaMaterials { get; set; } = new HashSet<MediaMaterial>();
    }
}