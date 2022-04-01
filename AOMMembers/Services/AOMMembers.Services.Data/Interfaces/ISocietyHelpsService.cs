using AOMMembers.Web.ViewModels.SocietyHelps;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ISocietyHelpsService
    {
        Task<string> CreateAsync(SocietyHelpInputModel inputModel, string userId);

        Task<SocietyHelpDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, SocietyHelpEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<SocietyHelpViewModel> GetAllFromMember(string userId);
    }
}