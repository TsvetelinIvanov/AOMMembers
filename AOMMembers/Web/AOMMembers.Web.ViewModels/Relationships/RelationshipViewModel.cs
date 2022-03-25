using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Relationships
{
    public class RelationshipViewModel
    {
        [Display(Name = RelationshipKindDisplayName)]
        public string Kind { get; set; }

        [Display(Name = RelationshipDescriptionDisplayName)]
        public string Description { get; set; }

        //public string MemberId { get; set; }

        //public string CitizenId { get; set; }
    }
}