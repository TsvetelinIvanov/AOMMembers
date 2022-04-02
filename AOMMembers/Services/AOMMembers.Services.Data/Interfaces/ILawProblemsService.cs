using AOMMembers.Web.ViewModels.LawProblems;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ILawProblemsService
    {
        Task<string> CreateAsync(LawProblemInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<LawProblemDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<LawProblemEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, LawProblemEditModel editModel);

        Task<LawProblemDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<LawProblemViewModel> GetAllFromMember(string userId);
    }
}