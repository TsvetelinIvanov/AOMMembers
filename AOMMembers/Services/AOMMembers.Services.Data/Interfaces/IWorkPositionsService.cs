using AOMMembers.Web.ViewModels.WorkPositions;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IWorkPositionsService
    {
        Task<string> CreateAsync(WorkPositionInputModel inputModel, string userId);

        Task<WorkPositionDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, WorkPositionEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<WorkPositionViewModel> GetAllFromMember(string userId);
    }
}