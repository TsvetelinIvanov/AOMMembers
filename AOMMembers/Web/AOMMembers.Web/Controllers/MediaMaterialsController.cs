using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.MediaMaterials;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class MediaMaterialsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMediaMaterialsService mediaMaterialsService;

        public MediaMaterialsController(UserManager<ApplicationUser> userManager, IMediaMaterialsService mediaMaterialsService)
        {
            this.userManager = userManager;
            this.mediaMaterialsService = mediaMaterialsService;
        }

        // GET: MediaMaterialsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: MediaMaterialsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.mediaMaterialsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            return this.View();
        }

        // GET: MediaMaterialsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new MediaMaterialInputModel());
        }

        // POST: MediaMaterialsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(MediaMaterialInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/MediaMaterials/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string mediaMaterialId = await this.mediaMaterialsService.CreateAsync(inputModel, userId);
                if (mediaMaterialId == MediaMaterialCreateWithoutPublicImageBadResult)
                {
                    return this.BadRequest(MediaMaterialCreateWithoutPublicImageBadRequest);
                }

                return this.Redirect("/MediaMaterials/Details?id=" + mediaMaterialId);
            }
            catch
            {
                return this.View(new MediaMaterialInputModel());
            }
        }

        // GET: MediaMaterialsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.mediaMaterialsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.mediaMaterialsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            MediaMaterialEditModel editModel = await this.mediaMaterialsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: MediaMaterialsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, MediaMaterialEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.mediaMaterialsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.mediaMaterialsService.EditAsync(id, editModel);
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

        // GET: MediaMaterialsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.mediaMaterialsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.mediaMaterialsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            MediaMaterialDeleteModel deleteModel = await this.mediaMaterialsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: MediaMaterialsController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.mediaMaterialsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.mediaMaterialsService.DeleteAsync(id);
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