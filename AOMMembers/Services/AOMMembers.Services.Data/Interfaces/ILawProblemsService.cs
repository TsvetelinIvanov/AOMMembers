using AOMMembers.Web.ViewModels.LawProblems;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ILawProblemsService
    {
        Task<string> CreateAsync(LawProblemInputModel inputModel, string userId);

        Task<LawProblemDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, LawProblemEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<LawProblemViewModel> GetAllFromMember(string userId);
    }
}