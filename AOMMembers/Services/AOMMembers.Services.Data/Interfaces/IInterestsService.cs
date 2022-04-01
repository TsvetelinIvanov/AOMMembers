using AOMMembers.Web.ViewModels.Interests;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IInterestsService
    {
        Task<string> CreateAsync(InterestInputModel inputModel, string userId);

        Task<InterestDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, InterestEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<InterestViewModel> GetAllFromMember(string userId);
    }
}