using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.PartyPositions;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class PartyPositionsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPartyPositionsService partyPositionsService;

        public PartyPositionsController(UserManager<ApplicationUser> userManager, IPartyPositionsService partyPositionsService)
        {
            this.userManager = userManager;
            this.partyPositionsService = partyPositionsService;
        }

        // GET: PartyPositionsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: PartyPositionsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.partyPositionsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            return this.View();
        }

        // GET: PartyPositionsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new PartyPositionInputModel());
        }

        // POST: PartyPositionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(PartyPositionInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/PartyPositions/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string partyPositionId = await this.partyPositionsService.CreateAsync(inputModel, userId);
                if (partyPositionId == PartyPositionCreateWithoutMemberBadResult)
                {
                    return this.BadRequest(PartyPositionCreateWithoutMemberBadRequest);
                }

                return this.Redirect("/PartyPositions/Details?id=" + partyPositionId);
            }
            catch
            {
                return this.View(new PartyPositionInputModel());
            }
        }

        // GET: PartyPositionsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.partyPositionsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.partyPositionsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            PartyPositionEditModel editModel = await this.partyPositionsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: PartyPositionsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, PartyPositionEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.partyPositionsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.partyPositionsService.EditAsync(id, editModel);
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

        // GET: PartyPositionsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.partyPositionsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.partyPositionsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            PartyPositionDeleteModel deleteModel = await this.partyPositionsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: PartyPositionsController/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.partyPositionsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.partyPositionsService.DeleteAsync(id);
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