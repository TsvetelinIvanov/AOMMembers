using AOMMembers.Web.ViewModels.SocietyActivities;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ISocietyActivitiesService
    {
        Task<string> CreateAsync(SocietyActivityInputModel inputModel, string citizenId);

        Task<SocietyActivityDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, SocietyActivityEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string citizenId);

        IEnumerable<SocietyActivityViewModel> GetAllFromMember(string citizenId);
    }
}