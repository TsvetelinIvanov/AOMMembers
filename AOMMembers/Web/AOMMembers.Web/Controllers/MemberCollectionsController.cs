using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Relationships;
using AOMMembers.Web.ViewModels.PartyPositions;
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
using AOMMembers.Web.ViewModels.MemberCollections;
using static AOMMembers.Common.GlobalConstants;

namespace AOMMembers.Web.Controllers
{
    [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
    public class MemberCollectionsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRelationshipsService relationshipsService;
        private readonly IPartyPositionsService partyPositionsService;
        private readonly IMediaMaterialsService mediaMaterialsService;
        private readonly IPartyMembershipsService partyMembershipsService;
        private readonly ISocietyHelpsService societyHelpsService;
        private readonly ISocietyActivitiesService societyActivitiesService;
        private readonly ISettingsService settingsService;
        private readonly IQualificationsService qualificationsService;
        private readonly IWorkPositionsService workPositionsService;
        private readonly IAssetsService assetsService;
        private readonly ILawProblemsService lawProblemsService;
        private readonly IInterestsService interestsService;

        public MemberCollectionsController(UserManager<ApplicationUser> userManager, IRelationshipsService relationshipsService, IPartyPositionsService partyPositionsService, IMediaMaterialsService mediaMaterialsService, IPartyMembershipsService partyMembershipsService, ISocietyHelpsService societyHelpsService, ISocietyActivitiesService societyActivitiesService, ISettingsService settingsService, IQualificationsService qualificationsService, IWorkPositionsService workPositionsService, IAssetsService assetsService, ILawProblemsService lawProblemsService, IInterestsService interestsService)
        {
            this.userManager = userManager;
            this.relationshipsService = relationshipsService;
            this.partyPositionsService = partyPositionsService;
            this.mediaMaterialsService = mediaMaterialsService;
            this.partyMembershipsService = partyMembershipsService;
            this.societyHelpsService = societyHelpsService;
            this.societyActivitiesService = societyActivitiesService;
            this.settingsService = settingsService;
            this.qualificationsService = qualificationsService;
            this.workPositionsService = workPositionsService;
            this.assetsService = assetsService;
            this.lawProblemsService = lawProblemsService;
            this.interestsService = interestsService;            
        }

        public IActionResult Collections()
        {
            string userId = this.userManager.GetUserId(this.User);

            int relationshipsCount = this.relationshipsService.GetCountFromMember(userId);
            int partyPositionsCount = this.partyPositionsService.GetCountFromMember(userId);
            int mediaMaterialsCount = this.mediaMaterialsService.GetCountFromMember(userId);
            int partyMembershipsCount = this.partyMembershipsService.GetCountFromMember(userId);
            int societyHelpsCount = this.societyHelpsService.GetCountFromMember(userId);
            int societyActivitiesCount = this.societyActivitiesService.GetCountFromMember(userId);
            int settingsCount = this.settingsService.GetCountFromMember(userId);
            int qualificationsCount = this.qualificationsService.GetCountFromMember(userId);
            int workPositionsCount = this.workPositionsService.GetCountFromMember(userId);
            int lawProblemsCount = this.lawProblemsService.GetCountFromMember(userId);
            int interestsCount = this.interestsService.GetCountFromMember(userId);

            IEnumerable<RelationshipViewModel> relationships = relationshipsService.GetAllFromMember(userId);
            IEnumerable<PartyPositionViewModel> partyPositions = partyPositionsService.GetAllFromMember(userId);
            IEnumerable<MediaMaterialViewModel> mediaMaterials = mediaMaterialsService.GetAllFromMember(userId);
            IEnumerable<PartyMembershipViewModel> partyMemberships = partyMembershipsService.GetAllFromMember(userId);
            IEnumerable<SocietyHelpViewModel> societyHelps = societyHelpsService.GetAllFromMember(userId);
            IEnumerable<SocietyActivityViewModel> societyActivities = societyActivitiesService.GetAllFromMember(userId);
            IEnumerable<SettingViewModel> settings = settingsService.GetAllFromMember(userId);
            IEnumerable<QualificationViewModel> qualifications = qualificationsService.GetAllFromMember(userId);
            IEnumerable<WorkPositionViewModel> workPositions = workPositionsService.GetAllFromMember(userId);
            IEnumerable<AssetViewModel> assets = assetsService.GetAllFromMember(userId);
            IEnumerable<LawProblemViewModel> lawProblems = lawProblemsService.GetAllFromMember(userId);
            IEnumerable<InterestViewModel> interests = interestsService.GetAllFromMember(userId);

            MemberCollectionsViewModel viewModel = new MemberCollectionsViewModel
            {
                RelationshipsCount = relationshipsCount,
                PartyPositionsCount = partyPositionsCount,
                MediaMaterialsCount = mediaMaterialsCount,
                PartyMembershipsCount = partyMembershipsCount,
                SocietyHelpsCount = societyHelpsCount,
                SocietyActivitiesCount = societyActivitiesCount,
                SettingsCount = settingsCount,
                QualificationsCount = qualificationsCount,
                WorkPositionsCount = workPositionsCount,
                LawProblemsCount = lawProblemsCount,
                InterestsCount = interestsCount,
                Relationships = relationships,
                PartyPositions = partyPositions,
                MediaMaterials = mediaMaterials,
                PartyMemberships = partyMemberships,
                SocietyHelps = societyHelps,
                SocietyActivities = societyActivities,
                Settings = settings,
                Qualifications = qualifications,
                WorkPositions = workPositions,
                Assets = assets,
                LawProblems = lawProblems,
                Interests = interests
            };

            return this.View(viewModel);
        }
    }
}