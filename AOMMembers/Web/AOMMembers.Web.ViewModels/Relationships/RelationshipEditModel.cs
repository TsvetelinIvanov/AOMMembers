using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.Relationships
{
    public class RelationshipEditModel
    {
        [Display(Name = RelationshipKindDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(RelationshipKindMaxLength, MinimumLength = RelationshipKindMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Kind { get; set; }

        [Display(Name = RelationshipDescriptionDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(RelationshipDescriptionMaxLength, MinimumLength = RelationshipDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; }

        //public string MemberId { get; set; }

        //public string CitizenId { get; set; }
    }
}