using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.WorkPositions;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class WorkPositionsController : BaseController 
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWorkPositionsService workPositionsService;

        public WorkPositionsController(UserManager<ApplicationUser> userManager, IWorkPositionsService workPositionsService)
        {
            this.userManager = userManager;
            this.workPositionsService = workPositionsService;
        }

        // GET: WorkPositionsController/Index/"Id"
        public async Task<ActionResult> Index(string id)
        {
            if (await this.workPositionsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            WorkPositionViewModel viewModel = await this.workPositionsService.GetViewModelByIdAsync(id);
            IEnumerable<WorkPositionViewModel> viewModelWrapper = new[] { viewModel };

            return this.View(viewModelWrapper);
        }

        // GET: WorkPositionsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.workPositionsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            WorkPositionDetailsViewModel viewModel = await this.workPositionsService.GetDetailsByIdAsync(id);

            return this.View(viewModel);
        }

        // GET: WorkPositionsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new WorkPositionInputModel());
        }

        // POST: WorkPositionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(WorkPositionInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/WorkPositions/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string workPositionId = await this.workPositionsService.CreateAsync(inputModel, userId);
                if (workPositionId == WorkPositionCreateWithoutCareerBadResult)
                {
                    return this.BadRequest(WorkPositionCreateWithoutCareerBadRequest);
                }

                return this.Redirect("/WorkPositions/Details?id=" + workPositionId);
            }
            catch
            {
                return this.View(new WorkPositionInputModel());
            }
        }

        // GET: WorkPositionsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.workPositionsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.workPositionsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            WorkPositionEditModel editModel = await this.workPositionsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: WorkPositionsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, WorkPositionEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.workPositionsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.workPositionsService.EditAsync(id, editModel);
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

        // GET: WorkPositionsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.workPositionsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.workPositionsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            WorkPositionDeleteModel deleteModel = await this.workPositionsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: WorkPositionsController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.workPositionsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.workPositionsService.DeleteAsync(id);
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