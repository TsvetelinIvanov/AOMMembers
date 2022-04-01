using AOMMembers.Web.ViewModels.Citizens;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ICitizensService
    {
        Task<string> CreateAsync(CitizenInputModel inputModel, string userId);

        Task<CitizenDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, CitizenEditModel editModel);

        Task<bool> DeleteAsync(string id);
    }
}