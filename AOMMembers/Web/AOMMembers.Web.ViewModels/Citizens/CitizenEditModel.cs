using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;
using static AOMMembers.Common.DataConstants;
using static AOMMembers.Common.DataErrorMessages;

namespace AOMMembers.Web.ViewModels.Citizens
{
    public class CitizenEditModel
    {
        public string Id { get; set; }

        [Display(Name = CitizenFirstNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(CitizenFirstNameMaxLength, MinimumLength = CitizenFirstNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string FirstName { get; set; }

        [Display(Name = CitizenSecondNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(CitizenSecondNameMaxLength, MinimumLength = CitizenSecondNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string SecondName { get; set; }

        [Display(Name = CitizenLastNameDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(CitizenLastNameMaxLength, MinimumLength = CitizenLastNameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string LastName { get; set; }

        [Display(Name = CitizenGenderDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(CitizenGenderMaxLength, MinimumLength = CitizenGenderMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Gender { get; set; }

        [Display(Name = CitizenAgeDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(CitizenAgeMinValue, CitizenAgeMaxValue, ErrorMessage = CitizenAgeErrorMessage)]
        public int Age { get; set; }

        [Display(Name = CitizenBirthDateDisplayName)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = CitizenDeathDateDisplayName)]
        [DataType(DataType.Date)]
        public DateTime? DeathDate { get; set; }
    }
}