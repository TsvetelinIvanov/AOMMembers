using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.Interests;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class InterestsService : IInterestsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Interest> interestsRespository;
        private readonly IDeletableEntityRepository<Worldview> worldviewsRespository;

        public InterestsService(IMapper mapper, IDeletableEntityRepository<Interest> interestsRespository, IDeletableEntityRepository<Worldview> worldviewsRespository)
        {
            this.mapper = mapper;
            this.interestsRespository = interestsRespository;
            this.worldviewsRespository = worldviewsRespository;
        }

        public async Task<string> CreateAsync(InterestInputModel inputModel, string userId)
        {
            Worldview worldview = this.worldviewsRespository.AllAsNoTracking().FirstOrDefault(w => w.Citizen.Member.ApplicationUserId == userId);
            if (worldview == null)
            {
                return InterestCreateWithoutWorldviewBadResult;
            }

            Interest interest = new Interest
            {                
                Description = inputModel.Description,
                WorldviewId = worldview.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.interestsRespository.AddAsync(interest);
            await this.interestsRespository.SaveChangesAsync();

            return interest.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            Interest interest = await this.interestsRespository.GetByIdAsync(id);

            return interest == null;
        }

        public async Task<InterestViewModel> GetViewModelByIdAsync(string id)
        {
            Interest interest = await this.interestsRespository.GetByIdAsync(id);
            InterestViewModel viewModel = this.mapper.Map<InterestViewModel>(interest);

            return viewModel;
        }

        public async Task<InterestDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Interest interest = await this.interestsRespository.GetByIdAsync(id);
            InterestDetailsViewModel detailsViewModel = this.mapper.Map<InterestDetailsViewModel>(interest);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Interest interest = await this.interestsRespository.GetByIdAsync(id);

            return interest.Worldview.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<InterestEditModel> GetEditModelByIdAsync(string id)
        {
            Interest interest = await this.interestsRespository.GetByIdAsync(id);
            InterestEditModel editModel = this.mapper.Map<InterestEditModel>(interest);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, InterestEditModel editModel)
        {
            Interest interest = await this.interestsRespository.GetByIdAsync(id);
            if (interest == null)
            {
                return false;
            }
            
            interest.Description = editModel.Description;
            interest.ModifiedOn = DateTime.UtcNow;

            await this.interestsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<InterestDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            Interest interest = await this.interestsRespository.GetByIdAsync(id);
            InterestDeleteModel deleteModel = this.mapper.Map<InterestDeleteModel>(interest);

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Interest interest = await this.interestsRespository.GetByIdAsync(id);
            if (interest == null)
            {
                return false;
            }

            this.interestsRespository.Delete(interest);
            await this.interestsRespository.SaveChangesAsync();

            return interest.IsDeleted;
        }

        public int GetCountFromMember(string userId)
        {
            Worldview worldview = this.worldviewsRespository.AllAsNoTracking().FirstOrDefault(w => w.Citizen.Member.ApplicationUserId == userId);
            if (worldview == null)
            {
                return 0;
            }

            return this.interestsRespository.All().Where(i => i.WorldviewId == worldview.Id).Count();
        }

        public IEnumerable<InterestViewModel> GetAllFromMember(string userId)
        {
            Worldview worldview = this.worldviewsRespository.AllAsNoTracking().FirstOrDefault(w => w.Citizen.Member.ApplicationUserId == userId);
            if (worldview == null)
            {
                return null;
            }

            List<InterestViewModel> interests = this.interestsRespository.All()
                .Where(i => i.WorldviewId == worldview.Id)
                .OrderByDescending(i => i.CreatedOn)
                .To<InterestViewModel>().ToList();

            return interests;
        }
    }
}