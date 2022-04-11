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

            Member member = await this.membersRespository.GetByIdAsync(applicationUser.MemberId);
            if (member == null)
            {
                member = this.membersRespository.AllWithDeleted().FirstOrDefault(m => m.IsDeleted && m.ApplicationUserId == applicationUser.Id);
                if (member == null)
                {
                    return;
                }

                this.membersRespository.Undelete(member);
                await this.membersRespository.SaveChangesAsync();
            }

            Relationship[] relationships = relationshipsRespository.AllWithDeleted().Where(r => r.MemberId == member.Id && r.IsDeleted).ToArray();
            foreach (Relationship relationship in relationships)
            {
                this.relationshipsRespository.Undelete(relationship);
            }

            await this.relationshipsRespository.SaveChangesAsync();

            PartyPosition[] partyPositions = partyPositionsRespository.AllWithDeleted().Where(pp =>pp.MemberId == member.Id && pp.IsDeleted).ToArray();
            foreach (PartyPosition partyPosition in partyPositions)
            {
                this.partyPositionsRespository.Undelete(partyPosition);
            }

            await this.partyPositionsRespository.SaveChangesAsync();

            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(member.PublicImageId);
            if (publicImage == null)
            {
                publicImage = this.publicImagesRespository.AllWithDeleted().FirstOrDefault(pi => pi.MemberId == member.Id && pi.IsDeleted);
                if (publicImage != null)
                {
                    this.publicImagesRespository.Undelete(publicImage);
                    await this.publicImagesRespository.SaveChangesAsync();
                }
            }

            if (publicImage != null)
            {
                MediaMaterial[] mediaMaterials = mediaMaterialsRespository.AllWithDeleted().Where(mm => mm.PublicImageId == publicImage.Id && mm.IsDeleted).ToArray();
                foreach (MediaMaterial mediaMaterial in mediaMaterials)
                {
                    this.mediaMaterialsRespository.Undelete(mediaMaterial);
                }

                await this.mediaMaterialsRespository.SaveChangesAsync();
            }

            Citizen citizen = await this.citizensRespository.GetByIdAsync(member.CitizenId);
            if (citizen == null)
            {
                citizen = this.citizensRespository.AllWithDeleted().FirstOrDefault(c => c.IsDeleted && c.MemberId == member.Id);
                if (citizen != null)
                {
                    this.citizensRespository.Undelete(citizen);
                    await this.citizensRespository.SaveChangesAsync();
                }
            }

            if (citizen != null)
            {
                PartyMembership[] partyMemberships = partyMembershipsRespository.AllWithDeleted().Where(pm => pm.CitizenId == citizen.Id && pm.IsDeleted).ToArray();
                foreach (PartyMembership partyMembership in partyMemberships)
                {
                    this.partyMembershipsRespository.Undelete(partyMembership);
                }

                await this.partyMembershipsRespository.SaveChangesAsync();

                SocietyHelp[] societyHelps = societyHelpsRespository.AllWithDeleted().Where(sh => sh.CitizenId == citizen.Id && sh.IsDeleted).ToArray();
                foreach (SocietyHelp societyHelp in societyHelps)
                {
                    this.societyHelpsRespository.Undelete(societyHelp);
                }

                await this.societyHelpsRespository.SaveChangesAsync();

                SocietyActivity[] societyActivities = societyActivitiesRespository.AllWithDeleted().Where(sa => sa.CitizenId == citizen.Id && sa.IsDeleted).ToArray();
                foreach (SocietyActivity societyActivity in societyActivities)
                {
                    this.societyActivitiesRespository.Undelete(societyActivity);
                }

                await this.societyActivitiesRespository.SaveChangesAsync();

                Setting[] settings = settingsRespository.AllWithDeleted().Where(s => s.CitizenId == citizen.Id && s.IsDeleted).ToArray();
                foreach (Setting setting in settings)
                {
                    this.settingsRespository.Undelete(setting);                    
                }

                await this.settingsRespository.SaveChangesAsync();

                Education education = await this.educationsRespository.GetByIdAsync(citizen.EducationId);
                if (education == null)
                {
                    education = this.educationsRespository.AllWithDeleted().FirstOrDefault(e => e.CitizenId == citizen.Id && e.IsDeleted);
                    if (education != null)
                    {
                        this.educationsRespository.Undelete(education);
                        await this.educationsRespository.SaveChangesAsync();
                    }                    
                }

                if (education != null)
                {
                    Qualification[] qualifications = qualificationsRespository.AllWithDeleted().Where(q => q.EducationId == education.Id && q.IsDeleted).ToArray();
                    foreach (Qualification qualification in qualifications)
                    {
                        this.qualificationsRespository.Undelete(qualification);                        
                    }

                    await this.qualificationsRespository.SaveChangesAsync();
                }

                Career career = await this.careersRespository.GetByIdAsync(citizen.CareerId);
                if (career == null)
                {
                    career = this.careersRespository.AllWithDeleted().FirstOrDefault(c => c.CitizenId == citizen.Id && c.IsDeleted);
                    if (career != null)
                    {
                        this.careersRespository.Undelete(career);
                        await this.careersRespository.SaveChangesAsync();
                    }
                }

                if (career != null)
                {
                    WorkPosition[] workPositions = workPositionsRespository.AllWithDeleted().Where(wp => wp.CareerId == career.Id && wp.IsDeleted).ToArray();
                    foreach (WorkPosition workPosition in workPositions)
                    {
                        this.workPositionsRespository.Undelete(workPosition);
                    }

                    await this.workPositionsRespository.SaveChangesAsync();
                }

                MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(citizen.MaterialStateId);
                if (materialState == null)
                {
                    materialState = this.materialStatesRespository.AllWithDeleted().FirstOrDefault(ms => ms.CitizenId == citizen.Id && ms.IsDeleted);
                    if (materialState != null)
                    {
                        this.materialStatesRespository.Undelete(materialState);
                        await this.materialStatesRespository.SaveChangesAsync();
                    }
                }

                if (materialState != null)
                {
                    Asset[] assets = assetsRespository.AllWithDeleted().Where(a => a.MaterialStateId == materialState.Id && a.IsDeleted).ToArray();
                    foreach (Asset asset in assets)
                    {
                        this.assetsRespository.Undelete(asset);
                    }

                    await this.assetsRespository.SaveChangesAsync();
                }

                LawState lawState = await this.lawStatesRespository.GetByIdAsync(citizen.LawStateId);
                if (lawState == null)
                {
                    lawState = this.lawStatesRespository.AllWithDeleted().FirstOrDefault(ls => ls.CitizenId == citizen.Id && ls.IsDeleted);
                    if (lawState != null)
                    {
                        this.lawStatesRespository.Undelete(lawState);
                        await this.lawStatesRespository.SaveChangesAsync();
                    }
                }

                if (lawState != null)
                {
                    LawProblem[] lawProblems = lawProblemsRespository.AllWithDeleted().Where(lp => lp.LawStateId == lawState.Id && lp.IsDeleted).ToArray();
                    foreach (LawProblem lawProblem in lawProblems)
                    {
                        this.lawProblemsRespository.Undelete(lawProblem);
                    }

                    await this.lawProblemsRespository.SaveChangesAsync();
                }

                Worldview worldview = await this.worldviewsRespository.GetByIdAsync(citizen.WorldviewId);
                if (worldview == null)
                {
                    worldview = this.worldviewsRespository.AllWithDeleted().FirstOrDefault(w => w.CitizenId == citizen.Id && w.IsDeleted);
                    if (worldview != null)
                    {
                        this.worldviewsRespository.Undelete(worldview);
                        await this.worldviewsRespository.SaveChangesAsync();
                    }
                }

                if (worldview != null)
                {
                    Interest[] interests = interestsRespository.AllWithDeleted().Where(i => i.WorldviewId == worldview.Id && i.IsDeleted).ToArray();
                    foreach (Interest interest in interests)
                    {
                        this.interestsRespository.Undelete(interest);
                    }

                    await this.interestsRespository.SaveChangesAsync();
                }
            }
        }
    }
}