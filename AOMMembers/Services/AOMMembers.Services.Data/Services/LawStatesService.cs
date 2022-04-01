using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.LawStates;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class LawStatesService : ILawStatesService
    {
        private readonly IDeletableEntityRepository<LawState> lawStatesRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;

        public LawStatesService(IDeletableEntityRepository<LawState> lawStatesRespository, IDeletableEntityRepository<Citizen> citizensRespository)
        {
            this.lawStatesRespository = lawStatesRespository;
            this.citizensRespository = citizensRespository;
        }        

        public async Task<string> CreateAsync(LawStateInputModel inputModel, string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return LawStateCreateWithoutCitizenBadResult;
            }

            LawState lawState = new LawState
            {
                Condition = inputModel.Condition,                
                CitizenId = citizen.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.lawStatesRespository.AddAsync(lawState);
            await this.lawStatesRespository.SaveChangesAsync();

            return lawState.Id;
        }

        public async Task<LawStateDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            LawState lawState = await this.lawStatesRespository.GetByIdAsync(id);
            LawStateDetailsViewModel detailsViewModel = new LawStateDetailsViewModel
            {
                Id = lawState.Id,
                Condition = lawState.Condition,               
                CitizenId = lawState.CitizenId,
                CreatedOn = lawState.CreatedOn,
                ModifiedOn = lawState.ModifiedOn,
                LawProblemsCount = lawState.LawProblems.Count
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            LawState lawState = await this.lawStatesRespository.GetByIdAsync(id);

            return lawState.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, LawStateEditModel editModel)
        {
            LawState lawState = this.lawStatesRespository.All().FirstOrDefault(ls => ls.Id == id);
            if (lawState == null)
            {
                return false;
            }

            lawState.Condition = editModel.Condition;
            lawState.ModifiedOn = DateTime.UtcNow;

            await this.lawStatesRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            LawState lawState = this.lawStatesRespository.All().FirstOrDefault(ls => ls.Id == id);
            if (lawState == null)
            {
                return false;
            }

            this.lawStatesRespository.Delete(lawState);
            await this.lawStatesRespository.SaveChangesAsync();

            return lawState.IsDeleted;
        }
    }
}