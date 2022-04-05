using AOMMembers.Web.ViewModels.MediaMaterials;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IMediaMaterialsService
    {
        Task<string> CreateAsync(MediaMaterialInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<MediaMaterialViewModel> GetViewModelByIdAsync(string id);

        Task<MediaMaterialDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<MediaMaterialEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, MediaMaterialEditModel editModel);

        Task<MediaMaterialDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<MediaMaterialViewModel> GetAllFromMember(string userId);
    }
}