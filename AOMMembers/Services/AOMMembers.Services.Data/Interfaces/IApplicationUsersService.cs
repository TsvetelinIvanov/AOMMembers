using AOMMembers.Data.Models;
using AOMMembers.Web.ViewModels.Administration.ApplicationUsers;

namespace AOMMembers.Services.Data.Interfaces
{
    public interface IApplicationUsersService
    {
        Task<IEnumerable<ApplicationUserViewModel>> GetUsers();        

        Task<ApplicationUser> GetApplicationUserById(string id);

        Task RestoreDeletedAsync(string id);
    }
}