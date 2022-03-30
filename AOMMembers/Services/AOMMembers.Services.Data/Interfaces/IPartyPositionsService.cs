using AOMMembers.Web.ViewModels.PartyPositions;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IPartyPositionsService
    {
        Task<string> CreateAsync(PartyPositionInputModel inputModel, string memberId);

        Task<PartyPositionDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, PartyPositionEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string memberId);

        IEnumerable<PartyPositionViewModel> GetAllFromMember(string memberId);
    }
}