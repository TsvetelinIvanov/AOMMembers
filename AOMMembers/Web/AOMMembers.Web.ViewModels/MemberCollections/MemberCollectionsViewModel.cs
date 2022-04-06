using System.ComponentModel.DataAnnotations;
using AOMMembers.Web.ViewModels.PartyPositions;
using AOMMembers.Web.ViewModels.Relationships;
using AOMMembers.Web.ViewModels.MediaMaterials;
using AOMMembers.Web.ViewModels.PartyMemberships;
using AOMMembers.Web.ViewModels.SocietyHelps;
using AOMMembers.Web.ViewModels.SocietyActivities;
using AOMMembers.Web.ViewModels.Settings;
using AOMMembers.Web.ViewModels.Qualifications;
using AOMMembers.Web.ViewModels.WorkPositions;
using AOMMembers.Web.ViewModels.Assets;
using AOMMembers.Web.ViewModels.LawProblems;
using AOMMembers.Web.ViewModels.Interests;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.MemberCollections
{
    public class MemberCollectionsViewModel
    {
        [Display(Name = MemberRelationshipsCountDisplayName)]
        public int RelationshipsCount { get; set; }

        [Display(Name = MemberPartyPositionsCountDisplayName)]
        public int PartyPositionsCount { get; set; }

        [Display(Name = PublicImageMediaMaterialsCountDisplayName)]
        public int MediaMaterialsCount { get; set; }        

        [Display(Name = CitizenPartyMembershipsCountDisplayName)]
        public int PartyMembershipsCount { get; set; }

        [Display(Name = CitizenSocietyHelpsCountDisplayName)]
        public int SocietyHelpsCount { get; set; }

        [Display(Name = CitizenSocietyActivitiesCountDisplayName)]
        public int SocietyActivitiesCount { get; set; }

        [Display(Name = CitizenSettingsCountDisplayName)]
        public int SettingsCount { get; set; }

        [Display(Name = EducationQualificationsCountDisplayName)]
        public int QualificationsCount { get; set; }

        [Display(Name = CareerWorkPositionsCountDisplayName)]
        public int WorkPositionsCount { get; set; }

        [Display(Name = MaterialStateAssetsCountDisplayName)]
        public int AssetsCount { get; set; }

        [Display(Name = LawStateLawProblemsCountDisplayName)]
        public int LawProblemsCount { get; set; }

        [Display(Name = WorldviewInterestsCountDisplayName)]
        public int InterestsCount { get; set; }        

        public IEnumerable<RelationshipViewModel> Relationships { get; set; }

        public IEnumerable<PartyPositionViewModel> PartyPositions { get; set; }

        public IEnumerable<MediaMaterialViewModel> MediaMaterials { get; set; }

        public IEnumerable<PartyMembershipViewModel> PartyMemberships { get; set; }

        public IEnumerable<SocietyHelpViewModel> SocietyHelps { get; set; }

        public IEnumerable<SocietyActivityViewModel> SocietyActivities { get; set; }

        public IEnumerable<SettingViewModel> Settings { get; set; }

        public IEnumerable<QualificationViewModel> Qualifications { get; set; }

        public IEnumerable<WorkPositionViewModel> WorkPositions { get; set; }

        public IEnumerable<AssetViewModel> Assets { get; set; }

        public IEnumerable<LawProblemViewModel> LawProblems { get; set; }

        public IEnumerable<InterestViewModel> Interests { get; set; }
    }
}