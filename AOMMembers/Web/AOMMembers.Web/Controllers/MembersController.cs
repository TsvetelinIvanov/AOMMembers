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

        // GET: MembersController/Index/"Id"
        public ActionResult Index()
        {
            IEnumerable<MemberViewModel> viewModels = this.membersService.GetViewModels();

            return this.View(viewModels);
        }

        // GET: MembersController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.membersService.IsAbsent(id))
            {
                return this.NotFound();
            }

            MemberDetailsViewModel viewModel = await this.membersService.GetDetailsByIdAsync(id);

            return this.View(viewModel);
        }

        // GET: MembersController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            string userId = this.userManager.GetUserId(this.User);
            if (this.membersService.IsCreated(userId))
            {
                return this.BadRequest(CreateCreatedEntityBadResult);
            }

            return this.View(new MemberInputModel());
        }

        // POST: MembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(MemberInputModel inputModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (this.membersService.IsCreated(userId))
            {
                return this.BadRequest(CreateCreatedEntityBadResult);
            }

            if (!ModelState.IsValid)
            {
                return Redirect("/Members/Create");
            }

            try
            {                
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
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.membersService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.membersService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            MemberEditModel editModel = await this.membersService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: MembersController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, MemberEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.membersService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.membersService.EditAsync(id, editModel);
                if (!isEdited)
                {
                    return this.BadRequest();
                }

                return this.RedirectToAction(nameof(Details), new { id });
            }
            catch
            {
                return this.View(editModel);
            }
        }

        // GET: MembersController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.membersService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.membersService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            MemberDeleteModel deleteModel = await this.membersService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: MembersController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.membersService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.membersService.DeleteAsync(id);
                if (!isDeleted)
                {
                    return this.BadRequest();
                }

                return this.Redirect("/Home/Index"); ;
            }
            catch
            {
                return this.BadRequest();
            }
        }
    }
}