using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Settings;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Setting> settingsRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;

        public SettingsService(IMapper mapper, IDeletableEntityRepository<Setting> settingsRespository, IDeletableEntityRepository<Citizen> citizensRespository)
        {
            this.mapper = mapper;
            this.settingsRespository = settingsRespository;
            this.citizensRespository = citizensRespository;
        }

        public async Task<string> CreateAsync(SettingInputModel inputModel, string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return SettingCreateWithoutCitizenBadResult;
            }

            Setting setting = new Setting
            {
                Name = inputModel.Name,
                Value = inputModel.Value,
                CitizenId = citizen.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.settingsRespository.AddAsync(setting);
            await this.settingsRespository.SaveChangesAsync();

            return setting.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            Setting setting = await this.settingsRespository.GetByIdAsync(id);

            return setting == null;
        }

        public async Task<SettingViewModel> GetViewModelByIdAsync(string id)
        {
            Setting setting = await this.settingsRespository.GetByIdAsync(id);
            SettingViewModel viewModel = this.mapper.Map<SettingViewModel>(setting);

            return viewModel;
        }

        public async Task<SettingDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Setting setting = await this.settingsRespository.GetByIdAsync(id);
            SettingDetailsViewModel detailsViewModel = this.mapper.Map<SettingDetailsViewModel>(setting);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Setting setting = await this.settingsRespository.GetByIdAsync(id);

            return setting.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<SettingEditModel> GetEditModelByIdAsync(string id)
        {
            Setting setting = await this.settingsRespository.GetByIdAsync(id);
            SettingEditModel editModel = this.mapper.Map<SettingEditModel>(setting);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, SettingEditModel editModel)
        {
            Setting setting = await this.settingsRespository.GetByIdAsync(id);
            if (setting == null)
            {
                return false;
            }

            setting.Name = editModel.Name;
            setting.Value = editModel.Value;
            setting.ModifiedOn = DateTime.UtcNow;

            await this.settingsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<SettingDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            Setting setting = await this.settingsRespository.GetByIdAsync(id);
            SettingDeleteModel deleteModel = this.mapper.Map<SettingDeleteModel>(setting);

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Setting setting = await this.settingsRespository.GetByIdAsync(id);
            if (setting == null)
            {
                return false;
            }

            this.settingsRespository.Delete(setting);
            await this.settingsRespository.SaveChangesAsync();

            return setting.IsDeleted;
        }

        public int GetCountFromMember(string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return 0;
            }

            return this.settingsRespository.All().Where(s => s.CitizenId == citizen.Id).Count();
        }

        public IEnumerable<SettingViewModel> GetAllFromMember(string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return null;
            }

            List<SettingViewModel> settings = this.settingsRespository.AllAsNoTracking()
                .Where(s => s.CitizenId == citizen.Id)
                .OrderByDescending(s => s.CreatedOn)
                .To<SettingViewModel>().ToList();

            return settings;
        }
    }
}