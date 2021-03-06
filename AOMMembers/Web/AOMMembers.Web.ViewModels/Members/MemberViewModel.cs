using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Members
{
    public class MemberViewModel : IMapFrom<Member>
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

        [Display(Name = MemberCurrentPartyPositionDisplayName)]
        public string? CurrentPartyPosition { get; set; }

        [Display(Name = MemberRelationshipsCountDisplayName)]
        public int RelationshipsCount { get; set; }
    }
}