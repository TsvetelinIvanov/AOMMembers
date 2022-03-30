using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Settings;

namespace AOMMembers.Services.Data.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Setting> settingsRespository;

        public SettingsService(IMapper mapper, IDeletableEntityRepository<Setting> settingsRespository)
        {
            this.mapper = mapper;
            this.settingsRespository = settingsRespository;
        }        

        public async Task<string> CreateAsync(SettingInputModel inputModel, string citizenId)
        {
            Setting setting = new Setting
            {
                Name = inputModel.Name,
                Value = inputModel.Value,
                CitizenId = citizenId,
                CreatedOn = DateTime.UtcNow
            };

            await this.settingsRespository.AddAsync(setting);
            await this.settingsRespository.SaveChangesAsync();

            return setting.Id;
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

        public async Task<bool> EditAsync(string id, SettingEditModel editModel)
        {
            Setting setting = this.settingsRespository.All().FirstOrDefault(s => s.Id == id);
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

        public async Task<bool> DeleteAsync(string id)
        {
            Setting setting = this.settingsRespository.All().FirstOrDefault(s => s.Id == id);
            if (setting == null)
            {
                return false;
            }

            this.settingsRespository.Delete(setting);
            await this.settingsRespository.SaveChangesAsync();

            return setting.IsDeleted;
        }

        public int GetCountFromMember(string citizenId)
        {
            return this.settingsRespository.All().Where(s => s.CitizenId == citizenId).Count();
        }

        public IEnumerable<SettingViewModel> GetAllFromMember(string citizenId)
        {
            List<SettingViewModel> settings = this.settingsRespository.AllAsNoTracking()
                .Where(s => s.CitizenId == citizenId)
                .OrderByDescending(s => s.CreatedOn)
                .To<SettingViewModel>().ToList();

            return settings;
        }
    }
}