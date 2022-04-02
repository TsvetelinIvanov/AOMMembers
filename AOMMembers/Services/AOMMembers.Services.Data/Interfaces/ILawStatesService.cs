using AOMMembers.Web.ViewModels.LawStates;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ILawStatesService
    {
        Task<string> CreateAsync(LawStateInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<LawStateDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<LawStateEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, LawStateEditModel editModel);

        Task<LawStateDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);
    }
}