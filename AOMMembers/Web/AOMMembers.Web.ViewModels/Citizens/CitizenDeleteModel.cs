using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Citizens
{
    public class CitizenDeleteModel
    {
        [Display(Name = CitizenFirstNameDisplayName)]
        public string FirstName { get; set; }

        [Display(Name = CitizenSecondNameDisplayName)]
        public string SecondName { get; set; }

        [Display(Name = CitizenLastNameDisplayName)]
        public string LastName { get; set; }

        [Display(Name = CitizenGenderDisplayName)]
        public string Gender { get; set; }

        [Display(Name = CitizenAgeDisplayName)]
        public int Age { get; set; }

        [Display(Name = CitizenBirthDateDisplayName)]
        public string BirthDate { get; set; }

        [Display(Name = CitizenDeathDateDisplayName)]
        public string? DeathDate { get; set; }
    }
}