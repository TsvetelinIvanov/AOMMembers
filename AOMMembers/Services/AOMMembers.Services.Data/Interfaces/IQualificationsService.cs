using AOMMembers.Web.ViewModels.Qualifications;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IQualificationsService
    {
        Task<string> CreateAsync(QualificationInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<QualificationViewModel> GetViewModelByIdAsync(string id);

        Task<QualificationDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<QualificationEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, QualificationEditModel editModel);

        Task<QualificationDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<QualificationViewModel> GetAllFromMember(string userId);
    }
}