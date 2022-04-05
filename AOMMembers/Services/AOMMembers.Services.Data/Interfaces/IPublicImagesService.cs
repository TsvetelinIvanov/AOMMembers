using AOMMembers.Web.ViewModels.PublicImages;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IPublicImagesService
    {
        bool IsCreated(string userId);

        Task<string> CreateAsync(PublicImageInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<PublicImageViewModel> GetViewModelByIdAsync(string id);

        Task<PublicImageDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<PublicImageEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, PublicImageEditModel editModel);

        Task<PublicImageDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);
    }
}