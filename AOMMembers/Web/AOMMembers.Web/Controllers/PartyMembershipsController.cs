using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.PartyMemberships;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class PartyMembershipsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPartyMembershipsService partyMembershipsService;

        public PartyMembershipsController(UserManager<ApplicationUser> userManager, IPartyMembershipsService partyMembershipsService)
        {
            this.userManager = userManager;
            this.partyMembershipsService = partyMembershipsService;
        }

        // GET: PartyMembershipsController/Index/"Id"
        public async Task<ActionResult> Index(string id)
        {
            if (await this.partyMembershipsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            PartyMembershipViewModel viewModel = await this.partyMembershipsService.GetViewModelByIdAsync(id);
            IEnumerable<PartyMembershipViewModel> viewModelWrapper = new[] { viewModel };

            return this.View(viewModelWrapper);
        }

        // GET: PartyMembershipsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.partyMembershipsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            PartyMembershipDetailsViewModel viewModel = await this.partyMembershipsService.GetDetailsByIdAsync(id);

            return this.View(viewModel);
        }

        // GET: PartyMembershipsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new PartyMembershipInputModel());
        }

        // POST: PartyMembershipsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(PartyMembershipInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/PartyMemberships/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string partyMembershipId = await this.partyMembershipsService.CreateAsync(inputModel, userId);
                if (partyMembershipId == PartyMembershipCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(PartyMembershipCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/PartyMemberships/Details?id=" + partyMembershipId);
            }
            catch
            {
                return this.View(new PartyMembershipInputModel());
            }
        }

        // GET: PartyMembershipsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.partyMembershipsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.partyMembershipsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            PartyMembershipEditModel editModel = await this.partyMembershipsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: PartyMembershipsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, PartyMembershipEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.partyMembershipsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.partyMembershipsService.EditAsync(id, editModel);
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

        // GET: PartyMembershipsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.partyMembershipsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.partyMembershipsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            PartyMembershipDeleteModel deleteModel = await this.partyMembershipsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: PartyMembershipsController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.partyMembershipsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.partyMembershipsService.DeleteAsync(id);
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