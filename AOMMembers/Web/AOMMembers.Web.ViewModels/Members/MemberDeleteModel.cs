using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Members
{
    public class MemberDeleteModel
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

        //public string ApplicationUserId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }

        [Display(Name = MemberCurrentPartyPositionDisplayName)]
        public string? CurrentPartyPosition { get; set; }

        [Display(Name = MemberRelationshipsCountDisplayName)]
        public int RelationshipsCount { get; set; }
    }
}