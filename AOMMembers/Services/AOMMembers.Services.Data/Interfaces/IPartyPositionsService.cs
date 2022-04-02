using AOMMembers.Web.ViewModels.PartyPositions;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IPartyPositionsService
    {
        Task<string> CreateAsync(PartyPositionInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<PartyPositionDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<PartyPositionEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, PartyPositionEditModel editModel);

        Task<PartyPositionDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<PartyPositionViewModel> GetAllFromMember(string userId);
    }
}