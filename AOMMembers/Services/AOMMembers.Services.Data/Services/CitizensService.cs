using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AutoMapper;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Citizens;
using AOMMembers.Web.ViewModels.PartyMemberships;
using AOMMembers.Web.ViewModels.SocietyHelps;
using AOMMembers.Web.ViewModels.SocietyActivities;
using AOMMembers.Web.ViewModels.Settings;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class CitizensService : ICitizensService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;
        private readonly IDeletableEntityRepository<Member> membersRespository;
        private readonly IDeletableEntityRepository<Education> educationsRespository;
        private readonly IDeletableEntityRepository<Qualification> qualificationsRespository;
        private readonly IDeletableEntityRepository<Career> careersRespository;
        private readonly IDeletableEntityRepository<WorkPosition> workPositionsRespository;
        private readonly IDeletableEntityRepository<MaterialState> materialStatesRespository;
        private readonly IDeletableEntityRepository<Asset> assetsRespository;
        private readonly IDeletableEntityRepository<LawState> lawStatesRespository;
        private readonly IDeletableEntityRepository<LawProblem> lawProblemsRespository;
        private readonly IDeletableEntityRepository<Worldview> worldviewsRespository;
        private readonly IDeletableEntityRepository<Interest> interestsRespository;
        private readonly IDeletableEntityRepository<PartyMembership> partyMembershipsRespository;
        private readonly IDeletableEntityRepository<SocietyHelp> societyHelpsRespository;
        private readonly IDeletableEntityRepository<SocietyActivity> societyActivitiesRespository;
        private readonly IDeletableEntityRepository<Setting> settingsRespository;

        public CitizensService(IMapper mapper, IDeletableEntityRepository<Citizen> citizensRespository, IDeletableEntityRepository<Member> membersRespository, IDeletableEntityRepository<Education> educationsRespository, IDeletableEntityRepository<Qualification> qualificationsRespository, IDeletableEntityRepository<Career> careersRespository, IDeletableEntityRepository<WorkPosition> workPositionsRespository, IDeletableEntityRepository<MaterialState> materialStatesRespository, IDeletableEntityRepository<Asset> assetsRespository, IDeletableEntityRepository<LawState> lawStatesRespository, IDeletableEntityRepository<LawProblem> lawProblemsRespository, IDeletableEntityRepository<Worldview> worldviewsRespository, IDeletableEntityRepository<Interest> interestsRespository, IDeletableEntityRepository<PartyMembership> partyMembershipsRespository, IDeletableEntityRepository<SocietyHelp> societyHelpsRespository, IDeletableEntityRepository<SocietyActivity> societyActivitiesRespository, IDeletableEntityRepository<Setting> settingsRespository)
        {
            this.mapper = mapper;
            this.citizensRespository = citizensRespository;
            this.membersRespository = membersRespository;
            this.educationsRespository = educationsRespository;
            this.qualificationsRespository = qualificationsRespository;
            this.careersRespository = careersRespository;
            this.workPositionsRespository = workPositionsRespository;
            this.materialStatesRespository = materialStatesRespository;
            this.assetsRespository = assetsRespository;
            this.lawStatesRespository = lawStatesRespository;
            this.lawProblemsRespository = lawProblemsRespository;
            this.worldviewsRespository = worldviewsRespository;
            this.interestsRespository = interestsRespository;
            this.partyMembershipsRespository = partyMembershipsRespository;
            this.societyHelpsRespository = societyHelpsRespository;
            this.societyActivitiesRespository = societyActivitiesRespository;
            this.settingsRespository = settingsRespository;            
        }

        public bool IsCreated(string userId)
        {
            return this.citizensRespository.AllAsNoTracking().Any(c => c.Member.ApplicationUserId == userId);
        }

        public async Task<string> CreateAsync(CitizenInputModel inputModel, string userId)
        {
            Member member = this.membersRespository.AllAsNoTracking().FirstOrDefault(m => m.ApplicationUserId == userId);
            if (member == null)
            {
                return CitizenCreateWithoutMemberBadResult;
            }

            Citizen citizen = new Citizen
            {
                FirstName = inputModel.FirstName,
                SecondName = inputModel.SecondName,
                LastName = inputModel.LastName,
                Gender = inputModel.Gender,
                Age = inputModel.Age,
                BirthDate = inputModel.BirthDate,
                DeathDate = inputModel.DeathDate,
                MemberId = member.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.citizensRespository.AddAsync(citizen);
            await this.citizensRespository.SaveChangesAsync();

            return citizen.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            Citizen citizen = await this.citizensRespository.GetByIdAsync(id);

            return citizen == null;
        }

        public async Task<CitizenViewModel> GetViewModelByIdAsync(string id)
        {
            Citizen citizen = await this.citizensRespository.GetByIdAsync(id);

            string workPositionName = "---";
            Career career = citizen.Career;
            if (career != null)
            {
                WorkPosition workPosition = career.WorkPositions.FirstOrDefault(wp => wp.IsCurrent);
                workPositionName = workPosition != null ? workPosition.Name : "---";
            }

            MaterialState materialState = citizen.MaterialState;
            decimal materialStateRiches = materialState != null ? materialState.Riches : 0;
            int assetsCount = materialState != null ? materialState.Assets.Count : 0;

            LawState lawState = citizen.LawState;
            string lawStateCondition = lawState != null ? lawState.Condition : "---";

            CitizenViewModel viewModel = new CitizenViewModel
            {
                Id = citizen.Id,
                FirstName = citizen.FirstName,
                SecondName = citizen.SecondName,
                LastName = citizen.LastName,
                Gender = citizen.Gender,
                Age = citizen.Age,
                BirthDate = citizen.BirthDate,
                DeathDate = citizen.DeathDate,                
                CurrentWorkPosition = workPositionName,
                MaterialState = materialStateRiches,
                AssetsCount = assetsCount,
                LawStateCondition = lawStateCondition,
                PartyMembershipsCount = citizen.PartyMemberships.Count,
                SocietyHelpsCount = citizen.SocietyHelps.Count,                
                SocietyActivitiesCount = citizen.SocietyActivities.Count
            };

            return viewModel;
        }

        public async Task<CitizenDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Citizen citizen = await this.citizensRespository.GetByIdAsync(id);

            string workPositionName = "---";
            Career career = citizen.Career;
            if (career != null)
            {
                WorkPosition workPosition = career.WorkPositions.FirstOrDefault(wp => wp.IsCurrent);
                workPositionName = workPosition != null ? workPosition.Name : "---";
            }

            MaterialState materialState = citizen.MaterialState;
            decimal materialStateRiches = materialState != null ? materialState.Riches : 0;
            int assetsCount = materialState != null ? materialState.Assets.Count : 0;

            LawState lawState = citizen.LawState;
            string lawStateCondition = lawState != null ? lawState.Condition : "---";

            HashSet<PartyMembershipViewModel> partyMemberships = new HashSet<PartyMembershipViewModel>();
            foreach (PartyMembership partyMembership in citizen.PartyMemberships)
            {
                PartyMembershipViewModel partyMembershipViewModel = this.mapper.Map<PartyMembershipViewModel>(partyMembership);
                partyMemberships.Add(partyMembershipViewModel);
            }

            HashSet<SocietyHelpViewModel> societyHelps = new HashSet<SocietyHelpViewModel>();
            foreach (SocietyHelp societyHelp in citizen.SocietyHelps)
            {
                SocietyHelpViewModel societyHelpViewModel = this.mapper.Map<SocietyHelpViewModel>(societyHelp);
                societyHelps.Add(societyHelpViewModel);
            }

            HashSet<SocietyActivityViewModel> societyActivities = new HashSet<SocietyActivityViewModel>();
            foreach (SocietyActivity societyActivity in citizen.SocietyActivities)
            {
                SocietyActivityViewModel societyActivityViewModel = this.mapper.Map<SocietyActivityViewModel>(societyActivity);
                societyActivities.Add(societyActivityViewModel);
            }

            HashSet<SettingViewModel> settings = new HashSet<SettingViewModel>();
            foreach (Setting setting in citizen.Settings)
            {
                SettingViewModel settingViewModel = this.mapper.Map<SettingViewModel>(setting);
                settings.Add(settingViewModel);
            }

            CitizenDetailsViewModel detailsViewModel = new CitizenDetailsViewModel
            {
                Id = citizen.Id,
                FirstName = citizen.FirstName,
                SecondName = citizen.SecondName,
                LastName = citizen.LastName,
                Gender = citizen.Gender,
                Age = citizen.Age,
                BirthDate = citizen.BirthDate,
                DeathDate= citizen.DeathDate,
                CreatedOn = citizen.CreatedOn,
                ModifiedOn = citizen.ModifiedOn,
                CurrentWorkPosition = workPositionName,
                MaterialState = materialStateRiches,
                AssetsCount = assetsCount,
                LawStateCondition = lawStateCondition,
                PartyMembershipsCount = citizen.PartyMemberships.Count,
                SocietyHelpsCount = citizen.SocietyHelps.Count,
                SocietyActivitiesCount = citizen.SocietyActivities.Count,
                MemberId = citizen.MemberId,
                EducationId = citizen.EducationId,
                CareerId = citizen.CareerId,
                MaterialStateId = citizen.MaterialStateId,
                LawStateId = citizen.LawStateId,
                WorldviewId = citizen.WorldviewId,
                PartyMemberships = partyMemberships,
                SocietyHelps = societyHelps,
                SocietyActivities = societyActivities,
                Settings = settings
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Citizen citizen = await this.citizensRespository.GetByIdAsync(id);

            return citizen.Member.ApplicationUserId == userId;
        }

        public async Task<CitizenEditModel> GetEditModelByIdAsync(string id)
        {
            Citizen citizen = await this.citizensRespository.GetByIdAsync(id);
            CitizenEditModel editModel = this.mapper.Map<CitizenEditModel>(citizen);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, CitizenEditModel editModel)
        {
            Citizen citizen = await this.citizensRespository.GetByIdAsync(id);
            if (citizen == null)
            {
                return false;
            }

            citizen.FirstName = editModel.FirstName;
            citizen.SecondName = editModel.SecondName;
            citizen.LastName = editModel.LastName;
            citizen.Gender = editModel.Gender;
            citizen.Age = editModel.Age;
            citizen.BirthDate = editModel.BirthDate;
            citizen.DeathDate = editModel.DeathDate;
            citizen.ModifiedOn = DateTime.UtcNow;

            await this.citizensRespository.SaveChangesAsync();

            return true;
        }

        public async Task<CitizenDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            Citizen citizen = await this.citizensRespository.GetByIdAsync(id);

            string workPositionName = "---";
            Career career = citizen.Career;
            if (career != null)
            {
                WorkPosition workPosition = career.WorkPositions.FirstOrDefault(wp => wp.IsCurrent);
                workPositionName = workPosition != null ? workPosition.Name : "---";
            }

            MaterialState materialState = citizen.MaterialState;
            decimal materialStateRiches = materialState != null ? materialState.Riches : 0;
            int assetsCount = materialState != null ? materialState.Assets.Count : 0;

            LawState lawState = citizen.LawState;
            string lawStateCondition = lawState != null ? lawState.Condition : "---";

            CitizenDeleteModel deleteModel = new CitizenDeleteModel
            {
                Id = citizen.Id,
                FirstName = citizen.FirstName,
                SecondName = citizen.SecondName,
                LastName = citizen.LastName,
                Gender = citizen.Gender,
                Age = citizen.Age,
                BirthDate = citizen.BirthDate,
                DeathDate = citizen.DeathDate,
                CreatedOn = citizen.CreatedOn,
                ModifiedOn = citizen.ModifiedOn,
                CurrentWorkPosition = workPositionName,
                MaterialState = materialStateRiches,
                AssetsCount = assetsCount,
                LawStateCondition = lawStateCondition,
                PartyMembershipsCount = citizen.PartyMemberships.Count,
                SocietyHelpsCount = citizen.SocietyHelps.Count,
                SocietyActivitiesCount = citizen.SocietyActivities.Count
            };

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Citizen citizen = await this.citizensRespository.GetByIdAsync(id);
            if (citizen == null)
            {
                return false;
            }

            Education education = citizen.Education;
            if (education != null)
            {
                foreach (Qualification qualification in education.Qualifications)
                {
                    this.qualificationsRespository.Delete(qualification);
                }

                await this.qualificationsRespository.SaveChangesAsync();

                this.educationsRespository.Delete(education);
                await this.educationsRespository.SaveChangesAsync();
            }

            Career career = citizen.Career;
            if (career != null)
            {
                foreach (WorkPosition workPosition in career.WorkPositions)
                {
                    this.workPositionsRespository.Delete(workPosition);
                }

                await this.workPositionsRespository.SaveChangesAsync();

                this.careersRespository.Delete(career);
                await this.careersRespository.SaveChangesAsync();
            }

            MaterialState materialState = citizen.MaterialState;
            if (materialState != null)
            {
                foreach (Asset asset in materialState.Assets)
                {
                    this.assetsRespository.Delete(asset);
                }

                await this.assetsRespository.SaveChangesAsync();

                this.materialStatesRespository.Delete(materialState);
                await this.materialStatesRespository.SaveChangesAsync();
            }

            LawState lawState = citizen.LawState;
            if (lawState != null)
            {
                foreach (LawProblem lawProblem in lawState.LawProblems)
                {
                    this.lawProblemsRespository.Delete(lawProblem);
                }

                await this.lawProblemsRespository.SaveChangesAsync();

                this.lawStatesRespository.Delete(lawState);
                await this.lawStatesRespository.SaveChangesAsync();
            }

            Worldview worldview = citizen.Worldview;
            if (worldview != null)
            {
                foreach (Interest interest in worldview.Interests)
                {
                    this.interestsRespository.Delete(interest);
                }

                await this.interestsRespository.SaveChangesAsync();

                this.worldviewsRespository.Delete(worldview);
                await this.worldviewsRespository.SaveChangesAsync();
            }

            foreach (PartyMembership partyMembership in citizen.PartyMemberships)
            {
                this.partyMembershipsRespository.Delete(partyMembership);
            }

            await this.partyMembershipsRespository.SaveChangesAsync();

            foreach (SocietyHelp societyHelp in citizen.SocietyHelps)
            {
                this.societyHelpsRespository.Delete(societyHelp);
            }

            await this.societyHelpsRespository.SaveChangesAsync();

            foreach (SocietyActivity societyActivity in citizen.SocietyActivities)
            {
                this.societyActivitiesRespository.Delete(societyActivity);
            }

            await this.societyActivitiesRespository.SaveChangesAsync();

            foreach (Setting setting in citizen.Settings)
            {
                this.settingsRespository.Delete(setting);
            }

            await this.settingsRespository.SaveChangesAsync();

            this.citizensRespository.Delete(citizen);
            await this.citizensRespository.SaveChangesAsync();

            return citizen.IsDeleted;
        }
    }
}