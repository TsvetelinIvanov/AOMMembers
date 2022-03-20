using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class Worldview : BaseDeletableModel
    {
        [Required]
        [StringLength(WorldviewDescriptionMaxLength, MinimumLength = WorldviewDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [StringLength(WorldviewIdeologyMaxLength, MinimumLength = WorldviewIdeologyMinLength)]
        public string Ideology { get; set; }

        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }

        public virtual ICollection<Interest> Interests { get; set; } = new HashSet<Interest>();
    }
}