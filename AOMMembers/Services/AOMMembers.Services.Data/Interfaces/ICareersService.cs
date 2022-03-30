using AOMMembers.Web.ViewModels.Careers;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ICareersService
    {
        Task<string> CreateAsync(CareerInputModel inputModel, string citizenId);

        Task<CareerDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<bool> EditAsync(string id, CareerEditModel editModel);

        Task<bool> DeleteAsync(string id);
    }
}