using AOMMembers.Web.ViewModels.Educations;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IEducationsService
    {
        Task<string> CreateAsync(EducationInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<EducationDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<EducationEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, EducationEditModel editModel);

        Task<EducationDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);
    }
}