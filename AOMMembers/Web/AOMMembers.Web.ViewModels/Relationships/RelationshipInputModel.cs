using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.Relationships
{
    public class RelationshipInputModel
    {
        [Display(Name = RelationshipKindDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(RelationshipKindMaxLength, MinimumLength = RelationshipKindMinLength)]
        public string Kind { get; set; }

        [Display(Name = RelationshipDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(RelationshipDescriptionMaxLength, MinimumLength = RelationshipDescriptionMinLength)]
        public string Description { get; set; }

        public string MemberId { get; set; }

        public string CitizenId { get; set; }
    }
}