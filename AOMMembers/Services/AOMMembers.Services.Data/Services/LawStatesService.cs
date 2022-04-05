using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.LawStates;
using AOMMembers.Web.ViewModels.LawProblems;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class LawStatesService : ILawStatesService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<LawState> lawStatesRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;
        private readonly IDeletableEntityRepository<LawProblem> lawProblemsRespository;

        public LawStatesService(IMapper mapper, IDeletableEntityRepository<LawState> lawStatesRespository, IDeletableEntityRepository<Citizen> citizensRespository, IDeletableEntityRepository<LawProblem> lawProblemsRespository)
        {
            this.mapper = mapper;
            this.lawStatesRespository = lawStatesRespository;
            this.citizensRespository = citizensRespository;
            this.lawProblemsRespository = lawProblemsRespository;
        }

        public bool IsCreated(string userId)
        {
            return this.lawStatesRespository.AllAsNoTracking().Any(ls => ls.Citizen.Member.ApplicationUserId == userId);
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

        public async Task<bool> IsAbsent(string id)
        {
            LawState lawState = await this.lawStatesRespository.GetByIdAsync(id);

            return lawState == null;
        }

        public async Task<LawStateViewModel> GetViewModelByIdAsync(string id)
        {
            LawState lawState = await this.lawStatesRespository.GetByIdAsync(id);
            LawStateViewModel viewModel = new LawStateViewModel
            {
                Id = lawState.Id,
                Condition = lawState.Condition,                
                LawProblemsCount = lawState.LawProblems.Count
            };

            return viewModel;
        }

        public async Task<LawStateDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            LawState lawState = await this.lawStatesRespository.GetByIdAsync(id);

            HashSet<LawProblemViewModel> lawProblems = new HashSet<LawProblemViewModel>();
            foreach (LawProblem lawProblem in lawState.LawProblems)
            {
                LawProblemViewModel lawProblemViewModel = this.mapper.Map<LawProblemViewModel>(lawProblem);
                lawProblems.Add(lawProblemViewModel);
            }

            LawStateDetailsViewModel detailsViewModel = new LawStateDetailsViewModel
            {
                Id = lawState.Id,
                Condition = lawState.Condition,
                CitizenId = lawState.CitizenId,
                CreatedOn = lawState.CreatedOn,
                ModifiedOn = lawState.ModifiedOn,
                LawProblemsCount = lawState.LawProblems.Count,
                LawProblems = lawProblems
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            LawState lawState = await this.lawStatesRespository.GetByIdAsync(id);

            return lawState.Citizen.Member.ApplicationUserId == userId;
        }        

        public async Task<LawStateEditModel> GetEditModelByIdAsync(string id)
        {
            LawState lawState = await this.lawStatesRespository.GetByIdAsync(id);
            LawStateEditModel editModel = this.mapper.Map<LawStateEditModel>(lawState);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, LawStateEditModel editModel)
        {
            LawState lawState = await this.lawStatesRespository.GetByIdAsync(id);
            if (lawState == null)
            {
                return false;
            }

            lawState.Condition = editModel.Condition;
            lawState.ModifiedOn = DateTime.UtcNow;

            await this.lawStatesRespository.SaveChangesAsync();

            return true;
        }

        public async Task<LawStateDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            LawState lawState = await this.lawStatesRespository.GetByIdAsync(id);
            LawStateDeleteModel deleteModel = new LawStateDeleteModel
            {
                Id = lawState.Id,
                Condition = lawState.Condition,
                CreatedOn = lawState.CreatedOn,
                ModifiedOn = lawState.ModifiedOn,
                LawProblemsCount = lawState.LawProblems.Count
            };

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            LawState lawState = await this.lawStatesRespository.GetByIdAsync(id);
            if (lawState == null)
            {
                return false;
            }

            foreach (LawProblem lawProblem in lawState.LawProblems)
            {
                this.lawProblemsRespository.Delete(lawProblem);
            }

            await this.lawProblemsRespository.SaveChangesAsync();

            this.lawStatesRespository.Delete(lawState);
            await this.lawStatesRespository.SaveChangesAsync();

            return lawState.IsDeleted;
        }
    }
}