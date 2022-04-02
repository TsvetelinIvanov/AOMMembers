using AOMMembers.Web.ViewModels.Settings;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ISettingsService
    {
        Task<string> CreateAsync(SettingInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<SettingDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<SettingEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, SettingEditModel editModel);

        Task<SettingDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<SettingViewModel> GetAllFromMember(string userId);
    }
}