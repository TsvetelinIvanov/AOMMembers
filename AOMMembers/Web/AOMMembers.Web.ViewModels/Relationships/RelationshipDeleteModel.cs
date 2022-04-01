using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Relationships
{
    public class RelationshipDeleteModel
    {
        public string Id { get; set; }

        [Display(Name = RelationshipKindDisplayName)]
        public string Kind { get; set; }

        [Display(Name = RelationshipDescriptionDisplayName)]
        public string Description { get; set; }

        //public string MemberId { get; set; }

        //public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = RelationshipCitizenFullNameDisplayName)]
        public string RelationshipCitizenFullName { get; set; }
    }
}