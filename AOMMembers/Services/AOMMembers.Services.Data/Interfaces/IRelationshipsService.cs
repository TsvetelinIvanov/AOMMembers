using AOMMembers.Web.ViewModels.Relationships;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IRelationshipsService
    {
        Task<string> CreateAsync(RelationshipInputModel inputModel, string userId);

        Task<RelationshipDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, RelationshipEditModel editModel);

        Task<bool> DeleteAsync(string id);

        int GetCountFromMember(string userId);

        IEnumerable<RelationshipViewModel> GetAllFromMember(string userId);
    }
}