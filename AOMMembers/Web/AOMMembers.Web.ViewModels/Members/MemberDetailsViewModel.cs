using System.ComponentModel.DataAnnotations;
using AOMMembers.Web.ViewModels.PartyPositions;
using AOMMembers.Web.ViewModels.Relationships;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Members
{
    public class MemberDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = MemberFullNameDisplayName)]        
        public string FullName { get; set; }

        [Display(Name = MemberEmailDisplayName)]       
        public string Email { get; set; }

        [Display(Name = MemberPhoneNumberDisplayName)]        
        public string PhoneNumber { get; set; }

        [Display(Name = MemberPictureUrlDisplayName)]        
        public string PictureUrl { get; set; }

        public string ApplicationUserId { get; set; }       

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = MemberCurrentPartyPositionDisplayName)]
        public string? CurrentPartyPosition { get; set; }

        [Display(Name = MemberRelationshipsCountDisplayName)]
        public int RelationshipsCount { get; set; }

        public string? CitizenId { get; set; }

        public string? PublicImageId { get; set; }

        public HashSet<RelationshipViewModel> Relationships { get; set; }

        public HashSet<PartyPositionViewModel> PartyPositions { get; set; }
    }
}