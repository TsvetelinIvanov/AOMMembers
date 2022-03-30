using AOMMembers.Web.ViewModels.Members;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IMembersService
    {
        Task<string> CreateAsync(MemberInputModel inputModel, string applicationUserId);

        Task<MemberDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, MemberEditModel editModel);

        Task<bool> DeleteAsync(string id);
    }
}