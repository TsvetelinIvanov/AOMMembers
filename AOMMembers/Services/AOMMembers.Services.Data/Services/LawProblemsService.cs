using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.LawProblems;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class LawProblemsService : ILawProblemsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<LawProblem> lawProblemsRespository;
        private readonly IDeletableEntityRepository<LawState> lawStatesRespository;

        public LawProblemsService(IMapper mapper, IDeletableEntityRepository<LawProblem> lawProblemsRespository, IDeletableEntityRepository<LawState> lawStatesRespository)
        {
            this.mapper = mapper;
            this.lawProblemsRespository = lawProblemsRespository;
            this.lawStatesRespository = lawStatesRespository;
        }

        public async Task<string> CreateAsync(LawProblemInputModel inputModel, string userId)
        {
            LawState lawState = this.lawStatesRespository.AllAsNoTracking().FirstOrDefault(ls => ls.Citizen.Member.ApplicationUserId == userId);
            if (lawState == null)
            {
                return LawProblemCreateWithoutLawStateBadResult;
            }

            LawProblem lawProblem = new LawProblem
            {
                Description = inputModel.Description,
                LawProblemLink = inputModel.LawProblemLink,
                LawStateId = lawState.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.lawProblemsRespository.AddAsync(lawProblem);
            await this.lawProblemsRespository.SaveChangesAsync();

            return lawProblem.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            LawProblem lawProblem = await this.lawProblemsRespository.GetByIdAsync(id);

            return lawProblem == null;
        }

        public async Task<LawProblemViewModel> GetViewModelByIdAsync(string id)
        {
            LawProblem lawProblem = await this.lawProblemsRespository.GetByIdAsync(id);
            LawProblemViewModel viewModel = this.mapper.Map<LawProblemViewModel>(lawProblem);

            return viewModel;
        }

        public async Task<LawProblemDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            LawProblem lawProblem = await this.lawProblemsRespository.GetByIdAsync(id);
            LawProblemDetailsViewModel detailsViewModel = this.mapper.Map<LawProblemDetailsViewModel>(lawProblem);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            LawProblem lawProblem = await this.lawProblemsRespository.GetByIdAsync(id);

            return lawProblem.LawState.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<LawProblemEditModel> GetEditModelByIdAsync(string id)
        {
            LawProblem lawProblem = await this.lawProblemsRespository.GetByIdAsync(id);
            LawProblemEditModel editModel = this.mapper.Map<LawProblemEditModel>(lawProblem);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, LawProblemEditModel editModel)
        {
            LawProblem lawProblem = await this.lawProblemsRespository.GetByIdAsync(id);
            if (lawProblem == null)
            {
                return false;
            }

            lawProblem.Description = editModel.Description;
            lawProblem.LawProblemLink = editModel.LawProblemLink;
            lawProblem.ModifiedOn = DateTime.UtcNow;

            await this.lawProblemsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<LawProblemDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            LawProblem lawProblem = await this.lawProblemsRespository.GetByIdAsync(id);
            LawProblemDeleteModel deleteModel = this.mapper.Map<LawProblemDeleteModel>(lawProblem);

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            LawProblem lawProblem = await this.lawProblemsRespository.GetByIdAsync(id);
            if (lawProblem == null)
            {
                return false;
            }

            this.lawProblemsRespository.Delete(lawProblem);
            await this.lawProblemsRespository.SaveChangesAsync();

            return lawProblem.IsDeleted;
        }

        public int GetCountFromMember(string userId)
        {
            LawState lawState = this.lawStatesRespository.AllAsNoTracking().FirstOrDefault(ls => ls.Citizen.Member.ApplicationUserId == userId);
            if (lawState == null)
            {
                return 0;
            }

            return this.lawProblemsRespository.All().Where(lp => lp.LawStateId == lawState.Id).Count();
        }

        public IEnumerable<LawProblemViewModel> GetAllFromMember(string userId)
        {
            LawState lawState = this.lawStatesRespository.AllAsNoTracking().FirstOrDefault(ls => ls.Citizen.Member.ApplicationUserId == userId);
            if (lawState == null)
            {
                return null;
            }

            List<LawProblemViewModel> lawProblems = this.lawProblemsRespository.All()
                .Where(lp => lp.LawStateId == lawState.Id)
                .OrderByDescending(lp => lp.CreatedOn)
                .To<LawProblemViewModel>().ToList();

            return lawProblems;
        }
    }
}