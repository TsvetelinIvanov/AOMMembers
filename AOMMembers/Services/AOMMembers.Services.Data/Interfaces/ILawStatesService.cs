using AOMMembers.Web.ViewModels.LawStates;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ILawStatesService
    {
        Task<string> CreateAsync(LawStateInputModel inputModel, string citizenId);

        Task<LawStateDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, LawStateEditModel editModel);

        Task<bool> DeleteAsync(string id);
    }
}