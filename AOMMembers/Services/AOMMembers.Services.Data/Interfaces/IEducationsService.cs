using AOMMembers.Web.ViewModels.Educations;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IEducationsService
    {
        Task<string> CreateAsync(EducationInputModel inputModel, string citizenId);

        Task<EducationDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, EducationEditModel editModel);

        Task<bool> DeleteAsync(string id);
    }
}