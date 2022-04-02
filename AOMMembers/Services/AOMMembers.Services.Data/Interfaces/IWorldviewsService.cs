using AOMMembers.Web.ViewModels.Worldviews;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IWorldviewsService
    {
        Task<string> CreateAsync(WorldviewInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<WorldviewDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<WorldviewEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, WorldviewEditModel editModel);

        Task<WorldviewDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);
    }
}