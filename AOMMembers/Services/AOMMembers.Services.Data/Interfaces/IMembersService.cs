using AOMMembers.Web.ViewModels.Members;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IMembersService
    {
        bool IsCreated(string userId);

        Task<string> CreateAsync(MemberInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        IEnumerable<MemberViewModel> GetViewModels();

        Task<MemberDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<MemberEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, MemberEditModel editModel);

        Task<MemberDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);
    }
}