using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Citizens;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class CitizensService : ICitizensService
    {
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;
        private readonly IDeletableEntityRepository<Member> membersRespository;

        public CitizensService(IDeletableEntityRepository<Citizen> citizensRespository, IDeletableEntityRepository<Member> membersRespository)
        {
            this.citizensRespository = citizensRespository;
            this.membersRespository = membersRespository;
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
                SocietyActivitiesCount = citizen.SocietyActivities.Count
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Citizen citizen = await this.citizensRespository.GetByIdAsync(id);

            return citizen.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, CitizenEditModel editModel)
        {
            Citizen citizen = this.citizensRespository.All().FirstOrDefault(c => c.Id == id);
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

        public async Task<bool> DeleteAsync(string id)
        {
            Citizen citizen = this.citizensRespository.All().FirstOrDefault(c => c.Id == id);
            if (citizen == null)
            {
                return false;
            }

            this.citizensRespository.Delete(citizen);
            await this.citizensRespository.SaveChangesAsync();

            return citizen.IsDeleted;
        }
    }
}