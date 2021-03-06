using AOMMembers.Web.ViewModels.Interests;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IInterestsService
    {
        Task<string> CreateAsync(InterestInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<InterestViewModel> GetViewModelByIdAsync(string id);

        Task<InterestDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<InterestEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, InterestEditModel editModel);

        Task<InterestDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<InterestViewModel> GetAllFromMember(string userId);
    }
}