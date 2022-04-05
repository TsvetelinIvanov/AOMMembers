using AOMMembers.Web.ViewModels.SocietyActivities;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ISocietyActivitiesService
    {
        Task<string> CreateAsync(SocietyActivityInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<SocietyActivityViewModel> GetViewModelByIdAsync(string id);

        Task<SocietyActivityDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<SocietyActivityEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, SocietyActivityEditModel editModel);

        Task<SocietyActivityDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<SocietyActivityViewModel> GetAllFromMember(string userId);
    }
}