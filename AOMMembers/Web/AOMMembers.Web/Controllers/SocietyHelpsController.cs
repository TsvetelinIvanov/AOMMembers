using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.SocietyHelps;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class SocietyHelpsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISocietyHelpsService societyHelpsService;

        public SocietyHelpsController(UserManager<ApplicationUser> userManager, ISocietyHelpsService societyHelpsService)
        {
            this.userManager = userManager;
            this.societyHelpsService = societyHelpsService;
        }

        // GET: SocietyHelpsController/Index/"Id"
        public async Task<ActionResult> Index(string id)
        {
            if (await this.societyHelpsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            SocietyHelpViewModel viewModel = await this.societyHelpsService.GetViewModelByIdAsync(id);
            IEnumerable<SocietyHelpViewModel> viewModelWrapper = new[] { viewModel };

            return this.View(viewModelWrapper);
        }

        // GET: SocietyHelpsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.societyHelpsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            SocietyHelpDetailsViewModel viewModel = await this.societyHelpsService.GetDetailsByIdAsync(id);

            return this.View(viewModel);
        }

        // GET: SocietyHelpsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new SocietyHelpInputModel());
        }

        // POST: SocietyHelpsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(SocietyHelpInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/SocietyHelps/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string societyHelpId = await this.societyHelpsService.CreateAsync(inputModel, userId);
                if (societyHelpId == SocietyHelpCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(SocietyHelpCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/SocietyHelps/Details?id=" + societyHelpId);
            }
            catch
            {
                return this.View(new SocietyHelpInputModel());
            }
        }

        // GET: SocietyHelpsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.societyHelpsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.societyHelpsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            SocietyHelpEditModel editModel = await this.societyHelpsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: SocietyHelpsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, SocietyHelpEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.societyHelpsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.societyHelpsService.EditAsync(id, editModel);
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

        // GET: SocietyHelpsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.societyHelpsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.societyHelpsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            SocietyHelpDeleteModel deleteModel = await this.societyHelpsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: SocietyHelpsController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.societyHelpsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.societyHelpsService.DeleteAsync(id);
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