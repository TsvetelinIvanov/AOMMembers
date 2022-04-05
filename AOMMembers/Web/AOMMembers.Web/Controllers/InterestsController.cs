using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Interests;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class InterestsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IInterestsService interestsService;

        public InterestsController(UserManager<ApplicationUser> userManager, IInterestsService interestsService)
        {
            this.userManager = userManager;
            this.interestsService = interestsService;
        }

        // GET: InterestsController/Index/"Id"
        public async Task<ActionResult> Index(string id)
        {
            if (await this.interestsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            InterestViewModel viewModel = await this.interestsService.GetViewModelByIdAsync(id);
            IEnumerable<InterestViewModel> viewModelWrapper = new[] { viewModel };

            return this.View(viewModelWrapper);
        }

        // GET: InterestsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.interestsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            InterestDetailsViewModel viewModel = await this.interestsService.GetDetailsByIdAsync(id);

            return this.View(viewModel);
        }

        // GET: InterestsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new InterestInputModel());
        }

        // POST: InterestsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(InterestInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Interests/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string interestId = await this.interestsService.CreateAsync(inputModel, userId);
                if (interestId == InterestCreateWithoutWorldviewBadResult)
                {
                    return this.BadRequest(InterestCreateWithoutWorldviewBadRequest);
                }

                return this.Redirect("/Interests/Details?id=" + interestId);
            }
            catch
            {
                return this.View(new InterestInputModel());
            }
        }

        // GET: InterestsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.interestsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.interestsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            InterestEditModel editModel = await this.interestsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: InterestsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, InterestEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.interestsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.interestsService.EditAsync(id, editModel);
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

        // GET: InterestsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.interestsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.interestsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            InterestDeleteModel deleteModel = await this.interestsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: InterestsController/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.interestsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.interestsService.DeleteAsync(id);
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