using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.Members
{
    public class MemberInputModel
    {
        [Display(Name = MemberFullNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MemberFullNameMaxLength, MinimumLength = MemberFullNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string FullName { get; set; }

        [Display(Name = MemberEmailDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [EmailAddress(ErrorMessage = MemberEmailErrorMessage)]
        [StringLength(MemberEmailMaxLength, MinimumLength = MemberEmailMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Email { get; set; }

        [Display(Name = MemberPhoneNumberDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Phone(ErrorMessage = MemberPhoneNumberErrorMessage)]
        [StringLength(MemberPhoneNumberMaxLength, MinimumLength = MemberPhoneNumberMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string PhoneNumber { get; set; }

        [Display(Name = MemberPictureUrlDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string PictureUrl { get; set; }

        //public string ApplicationUserId { get; set; }
    }
}