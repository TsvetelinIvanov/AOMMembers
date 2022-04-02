using AOMMembers.Web.ViewModels.MaterialStates;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IMaterialStatesService
    {
        Task<string> CreateAsync(MaterialStateInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<MaterialStateDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<MaterialStateEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, MaterialStateEditModel editModel);

        Task<MaterialStateDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);
    }
}