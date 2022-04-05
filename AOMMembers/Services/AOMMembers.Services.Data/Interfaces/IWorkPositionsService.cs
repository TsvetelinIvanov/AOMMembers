using AOMMembers.Web.ViewModels.WorkPositions;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IWorkPositionsService
    {
        Task<string> CreateAsync(WorkPositionInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<WorkPositionViewModel> GetViewModelByIdAsync(string id);

        Task<WorkPositionDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<WorkPositionEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, WorkPositionEditModel editModel);

        Task<WorkPositionDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<WorkPositionViewModel> GetAllFromMember(string userId);
    }
}