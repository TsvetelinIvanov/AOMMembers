using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Citizens;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class CitizensController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICitizensService citizensService;

        public CitizensController(UserManager<ApplicationUser> userManager, ICitizensService citizensService)
        {
            this.userManager = userManager;
            this.citizensService = citizensService;
        }

        // GET: CitizensController/Index/"Id"
        public async Task<ActionResult> Index(string id)
        {
            if (await this.citizensService.IsAbsent(id))
            {
                return this.NotFound();
            }

            CitizenViewModel viewModel = await this.citizensService.GetViewModelByIdAsync(id);
            IEnumerable<CitizenViewModel> viewModelWrapper = new[] { viewModel };

            return this.View(viewModelWrapper);
        }

        // GET: CitizensController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.citizensService.IsAbsent(id))
            {
                return this.NotFound();
            }

            CitizenDetailsViewModel viewModel = await this.citizensService.GetDetailsByIdAsync(id);

            return this.View(viewModel);
        }

        // GET: CitizensController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            string userId = this.userManager.GetUserId(this.User);
            if (this.citizensService.IsCreated(userId))
            {
                return this.BadRequest(CreateCreatedEntityBadResult);
            }

            return this.View(new CitizenInputModel());
        }

        // POST: CitizensController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(CitizenInputModel inputModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (this.citizensService.IsCreated(userId))
            {
                return this.BadRequest(CreateCreatedEntityBadResult);
            }

            if (!ModelState.IsValid)
            {
                return Redirect("/Citizens/Create");
            }

            try
            {                
                string citizenId = await this.citizensService.CreateAsync(inputModel, userId);
                if (citizenId == CitizenCreateWithoutMemberBadResult)
                {
                    return this.BadRequest(CitizenCreateWithoutMemberBadRequest);
                }

                return this.Redirect("/Citizens/Details?id=" + citizenId);
            }
            catch
            {
                return this.View(new CitizenInputModel());
            }
        }

        // GET: CitizensController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.citizensService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.citizensService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            CitizenEditModel editModel = await this.citizensService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: CitizensController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, CitizenEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.citizensService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.citizensService.EditAsync(id, editModel);
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

        // GET: CitizensController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.citizensService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.citizensService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            CitizenDeleteModel deleteModel = await this.citizensService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: CitizensController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.citizensService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.citizensService.DeleteAsync(id);
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