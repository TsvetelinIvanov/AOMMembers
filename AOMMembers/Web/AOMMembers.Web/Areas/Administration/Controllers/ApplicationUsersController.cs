using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Administration.ApplicationUsers;
using static AOMMembers.Common.GlobalConstants;

namespace AOMMembers.Web.Areas.Administration.Controllers
{
    public class ApplicationUsersController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IApplicationUsersService applicationUsersService;

        public ApplicationUsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IApplicationUsersService applicationUsersService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.applicationUsersService = applicationUsersService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ApplicationUserViewModel> applicationUsers = await applicationUsersService.GetUsers();

            return this.View(applicationUsers);
        }

        public async Task<IActionResult> UpdateRoles()
        {
            bool administratorRoleExists = await this.roleManager.RoleExistsAsync(AdministratorRoleName);
            if (!administratorRoleExists)
            {
                await this.roleManager.CreateAsync(new ApplicationRole(AdministratorRoleName)
                {
                    CreatedOn = DateTime.UtcNow
                });
            }

            bool memberRoleExists = await this.roleManager.RoleExistsAsync(AdministratorRoleName);
            if (!memberRoleExists)
            {
                await this.roleManager.CreateAsync(new ApplicationRole(MemberRoleName)
                {
                    CreatedOn = DateTime.UtcNow
                });
            }           

            return this.Ok();
        }

        public async Task<IActionResult> AddRoleToUser(string id)
        {
            ApplicationUser user = await this.applicationUsersService.GetApplicationUserById(id);
            RolesApplicationUserModel model = new RolesApplicationUserModel()
            {
                UserId = user.Id,
                Email = user.Email
            };


            ViewBag.RoleItems = this.roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = this.userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(RolesApplicationUserModel model)
        {
            ApplicationUser user = await this.applicationUsersService.GetApplicationUserById(model.UserId);
            IList<string> userRoles = await this.userManager.GetRolesAsync(user);
            await this.userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames?.Length > 0)
            {
                await this.userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return this.RedirectToAction(nameof(Index));
        }
                
        public async Task<IActionResult> RemoveRolesFromUser(string id)
        {
            ApplicationUser user = await this.applicationUsersService.GetApplicationUserById(id);
            IList<string> userRoles = await this.userManager.GetRolesAsync(user);
            await this.userManager.RemoveFromRolesAsync(user, userRoles);            

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RestoreDeletedInformation(string id)
        {
            try
            {
                await this.applicationUsersService.RestoreDeletedAsync(id);

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.BadRequest();
            }            
        }
    }
}