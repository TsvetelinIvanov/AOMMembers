using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Members;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class MembersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMembersService membersService;

        public MembersController(UserManager<ApplicationUser> userManager, IMembersService membersService)
        {
            this.userManager = userManager;
            this.membersService = membersService;
        }

        // GET: MembersController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: MembersController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
        }

        // GET: MembersController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new MemberInputModel());
        }

        // POST: MembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(MemberInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Members/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string memberId = await this.membersService.CreateAsync(inputModel, userId);
                if (memberId == MemberCreateWithoutApplicationUserBadResult)
                {
                    return this.BadRequest(MemberCreateWithoutApplicationUserBadRequest);
                }

                return this.Redirect("/Members/Details?id=" + memberId);
            }
            catch
            {
                return this.View(new MemberInputModel());
            }
        }

        // GET: MembersController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: MembersController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: MembersController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: MembersController/Delete/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}