using Microsoft.EntityFrameworkCore;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Administration.ApplicationUsers;

namespace AOMMembers.Services.Data.Services
{
    public class ApplicationUsersService : IApplicationUsersService
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

        public ApplicationUsersService(IDeletableEntityRepository<ApplicationUser> applicationUsersRespository, IDeletableEntityRepository<Member> membersRespository, IDeletableEntityRepository<Citizen> citizensRespository, IDeletableEntityRepository<Education> educationsRespository, IDeletableEntityRepository<Qualification> qualificationsRespository, IDeletableEntityRepository<Career> careersRespository, IDeletableEntityRepository<WorkPosition> workPositionsRespository, IDeletableEntityRepository<Relationship> relationshipsRespository, IDeletableEntityRepository<PartyPosition> partyPositionsRespository, IDeletableEntityRepository<PartyMembership> partyMembershipsRespository, IDeletableEntityRepository<MaterialState> materialStatesRespository, IDeletableEntityRepository<Asset> assetsRespository, IDeletableEntityRepository<PublicImage> publicImagesRespository, IDeletableEntityRepository<MediaMaterial> mediaMaterialsRespository, IDeletableEntityRepository<LawState> lawStatesRespository, IDeletableEntityRepository<LawProblem> lawProblemsRespository, IDeletableEntityRepository<SocietyHelp> societyHelpsRespository, IDeletableEntityRepository<SocietyActivity> societyActivitiesRespository, IDeletableEntityRepository<Worldview> worldviewsRespository, IDeletableEntityRepository<Interest> interestsRespository, IDeletableEntityRepository<Setting> settingsRespository)
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

        public async Task<ApplicationUser> GetApplicationUserById(string id)
        {
            return await this.applicationUsersRespository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ApplicationUserViewModel>> GetUsers()
        {
            return await this.applicationUsersRespository.All()
                .Select(u => new ApplicationUserViewModel()
                {
                    Id = u.Id,
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task RestoreDeletedAsync(string id)
        {
            ApplicationUser applicationUser = await this.applicationUsersRespository.GetByIdAsync(id);
            if (applicationUser == null)
            {
                return;
            }

            Member member = applicationUser.Member;
            if (member == null)
            {
                member = this.membersRespository.AllWithDeleted().FirstOrDefault(m => m.IsDeleted && m.IsDeleted);
                if (member != null)
                {
                    membersRespository.Undelete(member);
                }
            }
        }
    }
}