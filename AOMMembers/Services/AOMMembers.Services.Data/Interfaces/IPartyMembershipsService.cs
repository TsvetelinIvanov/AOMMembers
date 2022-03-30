using AOMMembers.Web.ViewModels.PartyMemberships;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IPartyMembershipsService
    {
        Task<string> CreateAsync(PartyMembershipInputModel inputModel, string citizenId);

        Task<PartyMembershipDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, PartyMembershipEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string citizenId);

        IEnumerable<PartyMembershipViewModel> GetAllFromMember(string citizenId);
    }
}