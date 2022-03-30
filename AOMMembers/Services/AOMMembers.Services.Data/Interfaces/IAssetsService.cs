using AOMMembers.Web.ViewModels.Assets;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IAssetsService
    {
        Task<string> CreateAsync(AssetInputModel inputModel, string materialStateId);

        Task<AssetDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, AssetEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string materialStateId);

        IEnumerable<AssetViewModel> GetAllFromMember(string materialStateId);
    }
}