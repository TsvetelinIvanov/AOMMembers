using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.LawStates;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class LawStatesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILawStatesService lawStatesService;

        public LawStatesController(UserManager<ApplicationUser> userManager, ILawStatesService lawStatesService)
        {
            this.userManager = userManager;
            this.lawStatesService = lawStatesService;
        }

        // GET: LawStatesController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: LawStatesController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (await this.lawStatesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            return this.View();
        }

        // GET: LawStatesController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new LawStateInputModel());
        }

        // POST: LawStatesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(LawStateInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/LawStates/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string lawStateId = await this.lawStatesService.CreateAsync(inputModel, userId);
                if (lawStateId == LawStateCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(LawStateCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/LawStates/Details?id=" + lawStateId);
            }
            catch
            {
                return this.View(new LawStateInputModel());
            }
        }

        // GET: LawStatesController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.lawStatesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.lawStatesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            LawStateEditModel editModel = await this.lawStatesService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: LawStatesController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, LawStateEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.lawStatesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.lawStatesService.EditAsync(id, editModel);
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

        // GET: LawStatesController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.lawStatesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.lawStatesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            LawStateDeleteModel deleteModel = await this.lawStatesService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: LawStatesController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.lawStatesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.lawStatesService.DeleteAsync(id);
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