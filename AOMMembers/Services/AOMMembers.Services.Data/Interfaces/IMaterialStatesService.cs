using AOMMembers.Web.ViewModels.MaterialStates;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IMaterialStatesService
    {
        Task<string> CreateAsync(MaterialStateInputModel inputModel, string citizenId);

        Task<MaterialStateDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, MaterialStateEditModel editModel);

        Task<bool> DeleteAsync(string id);
    }
}