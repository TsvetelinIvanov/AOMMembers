using AOMMembers.Web.ViewModels.MediaMaterials;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IMediaMaterialsService
    {
        Task<string> CreateAsync(MediaMaterialInputModel inputModel, string userId);

        Task<MediaMaterialDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, MediaMaterialEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<MediaMaterialViewModel> GetAllFromMember(string userId);
    }
}