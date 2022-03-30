using AOMMembers.Web.ViewModels.Settings;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ISettingsService
    {
        Task<string> CreateAsync(SettingInputModel inputModel, string citizenId);

        Task<SettingDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, SettingEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string citizenId);

        IEnumerable<SettingViewModel> GetAllFromMember(string citizenId);
    }
}