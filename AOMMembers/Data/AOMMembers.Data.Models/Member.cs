using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class Member : BaseDeletableModel
    {
        [Required]
        [StringLength(MemberFullNameMaxLength, MinimumLength = MemberFullNameMinLength)]
        public string FullName { get; set; }

        [Required]
        [StringLength(MemberEmailMaxLength, MinimumLength = MemberEmailMinLength)]
        public string Email { get; set; }

        [Required]
        [StringLength(MemberPhoneNumberMaxLength, MinimumLength = MemberPhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string? CitizenId { get; set; }
        public virtual Citizen? Citizen { get; set; }

        public string? PublicImageId { get; set; }
        public virtual PublicImage? PublicImage { get; set; }

        public virtual ICollection<Relationship> Relationships { get; set; } = new HashSet<Relationship>();

        public virtual ICollection<PartyPosition> PartyPositions { get; set; } = new HashSet<PartyPosition>();
    }
}