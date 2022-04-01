using AOMMembers.Web.ViewModels.Worldviews;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IWorldviewsService
    {
        Task<string> CreateAsync(WorldviewInputModel inputModel, string userId);

        Task<WorldviewDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, WorldviewEditModel editModel);

        Task<bool> DeleteAsync(string id);
    }
}