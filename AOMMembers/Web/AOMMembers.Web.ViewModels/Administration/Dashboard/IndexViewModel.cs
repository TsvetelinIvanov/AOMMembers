using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Administration.Dashboard
{
    public class IndexViewModel
    {
        [Display(Name = DashboardApplicationUsersCountDisplayName)]
        public int ApplicationUsersCount { get; set; }

        [Display(Name = DashboardMembersCountDisplayName)]
        public int MembersCount { get; set; }

        [Display(Name = DashboardCitizensCountDisplayName)]
        public int CitizensCount { get; set; }

        [Display(Name = DashboardEducationsCountDisplayName)]
        public int EducationsCount { get; set; }

        [Display(Name = DashboardQualificationsCountDisplayName)]
        public int QualificationsCount { get; set; }

        [Display(Name = DashboardCareersCountDisplayName)]
        public int CareersCount { get; set; }

        [Display(Name = DashboardWorkPositionsCountDisplayName)]
        public int WorkPositionsCount { get; set; }

        [Display(Name = DashboardRelationshipsCountDisplayName)]
        public int RelationshipsCount { get; set; }

        [Display(Name = DashboardPartyPositionsCountDisplayName)]
        public int PartyPositionsCount { get; set; }

        [Display(Name = DashboardPartyMembershipsCountDisplayName)]
        public int PartyMembershipsCount { get; set; }

        [Display(Name = DashboardMaterialStatesCountDisplayName)]
        public int MaterialStatesCount { get; set; }

        [Display(Name = DashboardAssetsCountDisplayName)]
        public int AssetsCount { get; set; }

        [Display(Name = DashboardPublicImagesCountDisplayName)]
        public int PublicImagesCount { get; set; }

        [Display(Name = DashboardMediaMaterialsCountDisplayName)]
        public int MediaMaterialsCount { get; set; }

        [Display(Name = DashboardLawStatesCountDisplayName)]
        public int LawStatesCount { get; set; }

        [Display(Name = DashboardLawProblemsCountDisplayName)]
        public int LawProblemsCount { get; set; }

        [Display(Name = DashboardSocietyHelpsCountDisplayName)]
        public int SocietyHelpsCount { get; set; }

        [Display(Name = DashboardSocietyActivitiesCountDisplayName)]
        public int SocietyActivitiesCount { get; set; }

        [Display(Name = DashboardWorldviewsCountDisplayName)]
        public int WorldviewsCount { get; set; }

        [Display(Name = DashboardInterestsCountDisplayName)]
        public int InterestsCount { get; set; }

        [Display(Name = DashboardSettingsCountDisplayName)]
        public int SettingsCount { get; set; }
    }
}