using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.SocietyActivities;

namespace AOMMembers.Services.Data.Services
{
    public class SocietyActivitiesService : ISocietyActivitiesService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<SocietyActivity> societyActivitiesRespository;

        public SocietyActivitiesService(IMapper mapper, IDeletableEntityRepository<SocietyActivity> societyActivitiesRespository)
        {
            this.mapper = mapper;
            this.societyActivitiesRespository = societyActivitiesRespository;
        }        

        public async Task<string> CreateAsync(SocietyActivityInputModel inputModel, string citizenId)
        {
            SocietyActivity societyActivity = new SocietyActivity
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                Result = inputModel.Result,
                EventLink = inputModel.EventLink,
                CitizenId = citizenId,
                CreatedOn = DateTime.UtcNow
            };

            await this.societyActivitiesRespository.AddAsync(societyActivity);
            await this.societyActivitiesRespository.SaveChangesAsync();

            return societyActivity.Id;
        }

        public async Task<SocietyActivityDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            SocietyActivity societyActivity = await this.societyActivitiesRespository.GetByIdAsync(id);
            SocietyActivityDetailsViewModel detailsViewModel = this.mapper.Map<SocietyActivityDetailsViewModel>(societyActivity);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            SocietyActivity societyActivity = await this.societyActivitiesRespository.GetByIdAsync(id);

            return societyActivity.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, SocietyActivityEditModel editModel)
        {
            SocietyActivity societyActivity = this.societyActivitiesRespository.All().FirstOrDefault(sa => sa.Id == id);
            if (societyActivity == null)
            {
                return false;
            }

            societyActivity.Name = editModel.Name;
            societyActivity.Description = editModel.Description;
            societyActivity.Result = editModel.Result;
            societyActivity.EventLink = editModel.EventLink;
            societyActivity.ModifiedOn = DateTime.UtcNow;

            await this.societyActivitiesRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            SocietyActivity societyActivity = this.societyActivitiesRespository.All().FirstOrDefault(sa => sa.Id == id);
            if (societyActivity == null)
            {
                return false;
            }

            this.societyActivitiesRespository.Delete(societyActivity);
            await this.societyActivitiesRespository.SaveChangesAsync();

            return societyActivity.IsDeleted;
        }

        public int GetCountFromMember(string citizenId)
        {
            return this.societyActivitiesRespository.All().Where(sa => sa.CitizenId == citizenId).Count();
        }

        public IEnumerable<SocietyActivityViewModel> GetAllFromMember(string citizenId)
        {
            List<SocietyActivityViewModel> societyActivities = this.societyActivitiesRespository.AllAsNoTracking()
                .Where(sa => sa.CitizenId == citizenId)
                .OrderByDescending(sa => sa.CreatedOn)
                .To<SocietyActivityViewModel>().ToList();

            return societyActivities;
        }
    }
}