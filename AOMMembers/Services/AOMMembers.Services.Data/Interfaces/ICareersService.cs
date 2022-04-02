using AOMMembers.Web.ViewModels.Careers;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface ICareersService
    {
        Task<string> CreateAsync(CareerInputModel inputModel, string userId);

        Task<bool> IsAbsent(string id);

        Task<CareerDetailsViewModel> GetDetailsByIdAsync(string id);

        Task<bool> IsFromMember(string id, string userId);

        Task<CareerEditModel> GetEditModelByIdAsync(string id);

        Task<bool> EditAsync(string id, CareerEditModel editModel);

        Task<CareerDeleteModel> GetDeleteModelByIdAsync(string id);

        Task<bool> DeleteAsync(string id);
    }
}