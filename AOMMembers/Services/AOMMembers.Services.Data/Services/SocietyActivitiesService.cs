using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.SocietyActivities;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class SocietyActivitiesService : ISocietyActivitiesService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<SocietyActivity> societyActivitiesRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;

        public SocietyActivitiesService(IMapper mapper, IDeletableEntityRepository<SocietyActivity> societyActivitiesRespository, IDeletableEntityRepository<Citizen> citizensRespository)
        {
            this.mapper = mapper;
            this.societyActivitiesRespository = societyActivitiesRespository;
            this.citizensRespository = citizensRespository;
        }        

        public async Task<string> CreateAsync(SocietyActivityInputModel inputModel, string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return SocietyActivityCreateWithoutCitizenBadResult;
            }

            SocietyActivity societyActivity = new SocietyActivity
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                Result = inputModel.Result,
                EventLink = inputModel.EventLink,
                CitizenId = citizen.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.societyActivitiesRespository.AddAsync(societyActivity);
            await this.societyActivitiesRespository.SaveChangesAsync();

            return societyActivity.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            SocietyActivity societyActivity = await this.societyActivitiesRespository.GetByIdAsync(id);

            return societyActivity == null;
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

        public async Task<SocietyActivityEditModel> GetEditModelByIdAsync(string id)
        {
            SocietyActivity societyActivity = await this.societyActivitiesRespository.GetByIdAsync(id);
            SocietyActivityEditModel editModel = this.mapper.Map<SocietyActivityEditModel>(societyActivity);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, SocietyActivityEditModel editModel)
        {
            SocietyActivity societyActivity = await this.societyActivitiesRespository.GetByIdAsync(id);
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

        public async Task<SocietyActivityDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            SocietyActivity societyActivity = await this.societyActivitiesRespository.GetByIdAsync(id);
            SocietyActivityDeleteModel deleteModel = this.mapper.Map<SocietyActivityDeleteModel>(societyActivity);

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            SocietyActivity societyActivity = await this.societyActivitiesRespository.GetByIdAsync(id);
            if (societyActivity == null)
            {
                return false;
            }

            this.societyActivitiesRespository.Delete(societyActivity);
            await this.societyActivitiesRespository.SaveChangesAsync();

            return societyActivity.IsDeleted;
        }

        public int GetCountFromMember(string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return 0;
            }

            return this.societyActivitiesRespository.All().Where(sa => sa.CitizenId == citizen.Id).Count();
        }

        public IEnumerable<SocietyActivityViewModel> GetAllFromMember(string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return null;
            }

            List<SocietyActivityViewModel> societyActivities = this.societyActivitiesRespository.AllAsNoTracking()
                .Where(sa => sa.CitizenId == citizen.Id)
                .OrderByDescending(sa => sa.CreatedOn)
                .To<SocietyActivityViewModel>().ToList();

            return societyActivities;
        }
    }
}