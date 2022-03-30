using AOMMembers.Web.ViewModels.Qualifications;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IQualificationsService
    {
        Task<string> CreateAsync(QualificationInputModel inputModel, string educationId);

        Task<QualificationDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, QualificationEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string educationId);

        IEnumerable<QualificationViewModel> GetAllFromMember(string educationId);
    }
}