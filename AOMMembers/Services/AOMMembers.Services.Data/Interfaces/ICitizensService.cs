using AOMMembers.Web.ViewModels.Citizens;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ICitizensService
    {
        bool IsCreated(string userId);

        Task<string> CreateAsync(CitizenInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<CitizenViewModel> GetViewModelByIdAsync(string id);

        Task<CitizenDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<CitizenEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, CitizenEditModel editModel);

        Task<CitizenDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);
    }
}