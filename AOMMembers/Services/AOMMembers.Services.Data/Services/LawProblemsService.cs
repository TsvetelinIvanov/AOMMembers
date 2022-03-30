using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.LawProblems;

namespace AOMMembers.Services.Data.Services
{
    public class LawProblemsService : ILawProblemsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<LawProblem> lawProblemsRespository;

        public LawProblemsService(IMapper mapper, IDeletableEntityRepository<LawProblem> lawProblemsRespository)
        {
            this.mapper = mapper;
            this.lawProblemsRespository = lawProblemsRespository;
        }        

        public async Task<string> CreateAsync(LawProblemInputModel inputModel, string lawStateId)
        {
            LawProblem lawProblem = new LawProblem
            {
                Description = inputModel.Description,
                LawProblemLink = inputModel.LawProblemLink,
                LawStateId = lawStateId,
                CreatedOn = DateTime.UtcNow
            };

            await this.lawProblemsRespository.AddAsync(lawProblem);
            await this.lawProblemsRespository.SaveChangesAsync();

            return lawProblem.Id;
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

        public async Task<bool> EditAsync(string id, LawProblemEditModel editModel)
        {
            LawProblem lawProblem = this.lawProblemsRespository.All().FirstOrDefault(lp => lp.Id == id);
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

        public async Task<bool> DeleteAsync(string id)
        {
            LawProblem lawProblem = this.lawProblemsRespository.All().FirstOrDefault(lp => lp.Id == id);
            if (lawProblem == null)
            {
                return false;
            }

            this.lawProblemsRespository.Delete(lawProblem);
            await this.lawProblemsRespository.SaveChangesAsync();

            return lawProblem.IsDeleted;
        }

        public int GetCountFromMember(string lawStateId)
        {
            return this.lawProblemsRespository.All().Where(lp => lp.LawStateId == lawStateId).Count();
        }

        public IEnumerable<LawProblemViewModel> GetAllFromMember(string lawStateId)
        {
            List<LawProblemViewModel> lawProblems = this.lawProblemsRespository.AllAsNoTracking()
                .Where(lp => lp.LawStateId == lawStateId)
                .OrderByDescending(lp => lp.CreatedOn)
                .To<LawProblemViewModel>().ToList();

            return lawProblems;
        }
    }
}