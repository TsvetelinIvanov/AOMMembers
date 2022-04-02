using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Citizens
{
    public class CitizenDeleteModel
    {
        public string Id { get; set; }

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
        public DateTime BirthDate { get; set; }

        [Display(Name = CitizenDeathDateDisplayName)]
        public DateTime? DeathDate { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = CitizenCurrentWorkPositionDisplayName)]
        public string? CurrentWorkPosition { get; set; }

        [Display(Name = CitizenMatrialStateDisplayName)]
        public decimal MaterialState { get; set; }

        [Display(Name = CitizenAssetsCountDisplayName)]
        public int AssetsCount { get; set; }

        [Display(Name = CitizenLawStateConditionDisplayName)]
        public string? LawStateCondition { get; set; }

        [Display(Name = CitizenPartyMembershipsCountDisplayName)]
        public int PartyMembershipsCount { get; set; }

        [Display(Name = CitizenSocietyHelpsCountDisplayName)]
        public int SocietyHelpsCount { get; set; }

        [Display(Name = CitizenSocietyActivitiesCountDisplayName)]
        public int SocietyActivitiesCount { get; set; }
    }
}