using AOMMembers.Web.ViewModels.PartyMemberships;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IPartyMembershipsService
    {
        Task<string> CreateAsync(PartyMembershipInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<PartyMembershipViewModel> GetViewModelByIdAsync(string id);

        Task<PartyMembershipDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<PartyMembershipEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, PartyMembershipEditModel editModel);

        Task<PartyMembershipDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<PartyMembershipViewModel> GetAllFromMember(string userId);
    }
}