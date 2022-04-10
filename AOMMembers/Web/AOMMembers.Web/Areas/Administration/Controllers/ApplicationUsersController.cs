using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;

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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRoleFromUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole()
        {
            return View();
        }

        public async Task<IActionResult> Details()
        {
            return View();
        }
    }
}