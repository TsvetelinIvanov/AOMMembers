using System.ComponentModel.DataAnnotations;
using AOMMembers.Web.ViewModels.Relationships;
using AOMMembers.Web.ViewModels.PartyMemberships;
using AOMMembers.Web.ViewModels.SocietyHelps;
using AOMMembers.Web.ViewModels.SocietyActivities;
using AOMMembers.Web.ViewModels.Settings;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Citizens
{
    public class CitizenDetailsViewModel
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

        public string? MemberId { get; set; }        

        public string? EducationId { get; set; }        

        public string? CareerId { get; set; }        

        public string? MaterialStateId { get; set; }       

        public string? LawStateId { get; set; }        

        public string? WorldviewId { get; set; }

        public RelationshipListViewModel Relationships { get; set; }

        public PartyMembershipListViewModel PartyMemberships { get; set; }

        public SocietyHelpListViewModel SocietyHelps { get; set; }

        public SocietyActivityListViewModel SocietyActivities { get; set; }

        public SettingsListViewModel Settings { get; set; }
    }
}