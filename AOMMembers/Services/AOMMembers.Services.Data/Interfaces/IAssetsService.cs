using AOMMembers.Web.ViewModels.Assets;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IAssetsService
    {
        Task<string> CreateAsync(AssetInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<AssetViewModel> GetViewModelByIdAsync(string id);

        Task<AssetDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<AssetEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, AssetEditModel editModel);

        Task<AssetDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<AssetViewModel> GetAllFromMember(string userId);
    }
}