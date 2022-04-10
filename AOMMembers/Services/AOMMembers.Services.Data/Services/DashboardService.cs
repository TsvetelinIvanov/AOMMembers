using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Administration.Dashboard;

namespace AOMMembers.Services.Data.Services
{
    public class DashboardService : IDashboardService
    {       
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUsersRespository;
        private readonly IDeletableEntityRepository<Member> membersRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;
        private readonly IDeletableEntityRepository<Education> educationsRespository;
        private readonly IDeletableEntityRepository<Qualification> qualificationsRespository;
        private readonly IDeletableEntityRepository<Career> careersRespository;
        private readonly IDeletableEntityRepository<WorkPosition> workPositionsRespository;
        private readonly IDeletableEntityRepository<Relationship> relationshipsRespository;
        private readonly IDeletableEntityRepository<PartyPosition> partyPositionsRespository;
        private readonly IDeletableEntityRepository<PartyMembership> partyMembershipsRespository;
        private readonly IDeletableEntityRepository<MaterialState> materialStatesRespository;
        private readonly IDeletableEntityRepository<Asset> assetsRespository;
        private readonly IDeletableEntityRepository<PublicImage> publicImagesRespository;
        private readonly IDeletableEntityRepository<MediaMaterial> mediaMaterialsRespository;
        private readonly IDeletableEntityRepository<LawState> lawStatesRespository;
        private readonly IDeletableEntityRepository<LawProblem> lawProblemsRespository;
        private readonly IDeletableEntityRepository<SocietyHelp> societyHelpsRespository;
        private readonly IDeletableEntityRepository<SocietyActivity> societyActivitiesRespository;
        private readonly IDeletableEntityRepository<Worldview> worldviewsRespository;
        private readonly IDeletableEntityRepository<Interest> interestsRespository;
        private readonly IDeletableEntityRepository<Setting> settingsRespository;        

        public DashboardService(IDeletableEntityRepository<ApplicationUser> applicationUsersRespository, IDeletableEntityRepository<Member> membersRespository, IDeletableEntityRepository<Citizen> citizensRespository, IDeletableEntityRepository<Education> educationsRespository, IDeletableEntityRepository<Qualification> qualificationsRespository, IDeletableEntityRepository<Career> careersRespository, IDeletableEntityRepository<WorkPosition> workPositionsRespository, IDeletableEntityRepository<Relationship> relationshipsRespository, IDeletableEntityRepository<PartyPosition> partyPositionsRespository, IDeletableEntityRepository<PartyMembership> partyMembershipsRespository, IDeletableEntityRepository<MaterialState> materialStatesRespository, IDeletableEntityRepository<Asset> assetsRespository, IDeletableEntityRepository<PublicImage> publicImagesRespository, IDeletableEntityRepository<MediaMaterial> mediaMaterialsRespository, IDeletableEntityRepository<LawState> lawStatesRespository, IDeletableEntityRepository<LawProblem> lawProblemsRespository, IDeletableEntityRepository<SocietyHelp> societyHelpsRespository, IDeletableEntityRepository<SocietyActivity> societyActivitiesRespository, IDeletableEntityRepository<Worldview> worldviewsRespository, IDeletableEntityRepository<Interest> interestsRespository, IDeletableEntityRepository<Setting> settingsRespository)
        {            
            this.applicationUsersRespository = applicationUsersRespository;
            this.membersRespository = membersRespository;
            this.citizensRespository = citizensRespository;
            this.educationsRespository = educationsRespository;
            this.qualificationsRespository = qualificationsRespository;
            this.careersRespository = careersRespository;
            this.workPositionsRespository = workPositionsRespository;
            this.relationshipsRespository = relationshipsRespository;
            this.partyPositionsRespository = partyPositionsRespository;
            this.partyMembershipsRespository = partyMembershipsRespository;
            this.materialStatesRespository = materialStatesRespository;
            this.assetsRespository = assetsRespository;
            this.publicImagesRespository = publicImagesRespository;
            this.mediaMaterialsRespository = mediaMaterialsRespository;
            this.lawStatesRespository = lawStatesRespository;
            this.lawProblemsRespository = lawProblemsRespository;
            this.societyHelpsRespository = societyHelpsRespository;
            this.societyActivitiesRespository = societyActivitiesRespository;
            this.worldviewsRespository = worldviewsRespository;
            this.interestsRespository = interestsRespository;
            this.settingsRespository = settingsRespository;            
        }

        public IndexViewModel GetIndexViewModel()
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                ApplicationUsersCount = this.applicationUsersRespository.All().Count(),
                MembersCount = this.membersRespository.All().Count(),
                CitizensCount = this.citizensRespository.All().Count(),
                EducationsCount = this.educationsRespository.All().Count(),
                QualificationsCount = this.qualificationsRespository.All().Count(),
                CareersCount = this.careersRespository.All().Count(),
                WorkPositionsCount = this.workPositionsRespository.All().Count(),
                RelationshipsCount = this.relationshipsRespository.All().Count(),
                PartyPositionsCount = this.partyPositionsRespository.All().Count(),
                PartyMembershipsCount = this.partyMembershipsRespository.All().Count(),
                MaterialStatesCount = this.materialStatesRespository.All().Count(),
                AssetsCount = this.assetsRespository.All().Count(),
                PublicImagesCount = this.publicImagesRespository.All().Count(),
                MediaMaterialsCount = this.mediaMaterialsRespository.All().Count(),
                LawStatesCount = this.lawStatesRespository.All().Count(),
                LawProblemsCount = this.lawProblemsRespository.All().Count(),
                SocietyHelpsCount = this.societyHelpsRespository.All().Count(),
                SocietyActivitiesCount = this.societyActivitiesRespository.All().Count(),
                WorldviewsCount = this.worldviewsRespository.All().Count(),
                InterestsCount = this.interestsRespository.All().Count(),
                SettingsCount = this.settingsRespository.All().Count()                
            };

            return viewModel;
        }
    }
}