using AOMMembers.Web.ViewModels.PublicImages;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IPublicImagesService
    {
        Task<string> CreateAsync(PublicImageInputModel inputModel, string memberId);

        Task<PublicImageDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, PublicImageEditModel editModel);

        Task<bool> DeleteAsync(string id);
    }
}