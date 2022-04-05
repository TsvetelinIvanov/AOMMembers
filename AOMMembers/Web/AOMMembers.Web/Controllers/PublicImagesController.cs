using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.PublicImages;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class PublicImagesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPublicImagesService publicImagesService;

        public PublicImagesController(UserManager<ApplicationUser> userManager, IPublicImagesService publicImagesService)
        {
            this.userManager = userManager;
            this.publicImagesService = publicImagesService;
        }

        // GET: PublicImagesController/Index/"Id"
        public async Task<ActionResult> Index(string id)
        {
            if (await this.publicImagesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            PublicImageViewModel viewModel = await this.publicImagesService.GetViewModelByIdAsync(id);
            IEnumerable<PublicImageViewModel> viewModelWrapper = new[] { viewModel };

            return this.View(viewModelWrapper);
        }

        // GET: PublicImagesController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.publicImagesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            PublicImageDetailsViewModel viewModel = await this.publicImagesService.GetDetailsByIdAsync(id);

            return this.View(viewModel);            
        }

        // GET: PublicImagesController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            string userId = this.userManager.GetUserId(this.User);
            if (this.publicImagesService.IsCreated(userId))
            {
                return this.BadRequest(CreateCreatedEntityBadResult);
            }

            return this.View(new PublicImageInputModel());
        }

        // POST: PublicImagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(PublicImageInputModel inputModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (this.publicImagesService.IsCreated(userId))
            {
                return this.BadRequest(CreateCreatedEntityBadResult);
            }

            if (!ModelState.IsValid)
            {
                return Redirect("/PublicImages/Create");
            }

            try
            {                
                string publicImageId = await this.publicImagesService.CreateAsync(inputModel, userId);
                if (publicImageId == PublicImageCreateWithoutMemberBadResult)
                {
                    return this.BadRequest(PublicImageCreateWithoutMemberBadRequest);
                }

                return this.Redirect("/PublicImages/Details?id=" + publicImageId);
            }
            catch
            {
                return this.View(new PublicImageInputModel());
            }
        }

        // GET: PublicImagesController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.publicImagesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.publicImagesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            PublicImageEditModel editModel = await this.publicImagesService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: PublicImagesController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, PublicImageEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.publicImagesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.publicImagesService.EditAsync(id, editModel);
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

        // GET: PublicImagesController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.publicImagesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.publicImagesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            PublicImageDeleteModel deleteModel = await this.publicImagesService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: PublicImagesController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.publicImagesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.publicImagesService.DeleteAsync(id);
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