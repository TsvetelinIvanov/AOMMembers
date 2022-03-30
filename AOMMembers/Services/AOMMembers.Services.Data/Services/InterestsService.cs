using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.Interests;

namespace AOMMembers.Services.Data.Services
{
    public class InterestsService : IInterestsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Interest> interestsRespository;

        public InterestsService(IMapper mapper, IDeletableEntityRepository<Interest> interestsRespository)
        {
            this.mapper = mapper;
            this.interestsRespository = interestsRespository;
        }        

        public async Task<string> CreateAsync(InterestInputModel inputModel, string worldviewId)
        {
            Interest interest = new Interest
            {                
                Description = inputModel.Description,
                WorldviewId = worldviewId,
                CreatedOn = DateTime.UtcNow
            };

            await this.interestsRespository.AddAsync(interest);
            await this.interestsRespository.SaveChangesAsync();

            return interest.Id;
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

        public async Task<bool> EditAsync(string id, InterestEditModel editModel)
        {
            Interest interest = this.interestsRespository.All().FirstOrDefault(i => i.Id == id);
            if (interest == null)
            {
                return false;
            }
            
            interest.Description = editModel.Description;
            interest.ModifiedOn = DateTime.UtcNow;

            await this.interestsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Interest interest = this.interestsRespository.All().FirstOrDefault(i => i.Id == id);
            if (interest == null)
            {
                return false;
            }

            this.interestsRespository.Delete(interest);
            await this.interestsRespository.SaveChangesAsync();

            return interest.IsDeleted;
        }

        public int GetCountFromMember(string worldviewId)
        {
            return this.interestsRespository.All().Where(i => i.WorldviewId == worldviewId).Count();
        }

        public IEnumerable<InterestViewModel> GetAllFromMember(string worldviewId)
        {
            List<InterestViewModel> interests = this.interestsRespository.AllAsNoTracking()
                .Where(i => i.WorldviewId == worldviewId)
                .OrderByDescending(i => i.CreatedOn)
                .To<InterestViewModel>().ToList();

            return interests;
        }
    }
}