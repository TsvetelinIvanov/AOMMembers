using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Assets;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class AssetsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAssetsService assetsService;

        public AssetsController(UserManager<ApplicationUser> userManager, IAssetsService assetsService)
        {
            this.userManager = userManager;
            this.assetsService = assetsService;
        }

        // GET: AssetsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: AssetsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.assetsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            return this.View();
        }

        // GET: AssetsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new AssetInputModel());
        }

        // POST: AssetsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(AssetInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Assets/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string assetId = await this.assetsService.CreateAsync(inputModel, userId);
                if (assetId == AssetCreateWithoutMaterialStateBadResult)
                {
                    return this.BadRequest(AssetCreateWithoutMaterialStateBadRequest);
                }

                //return this.RedirectToAction(nameof(Details), new { id = assetId });
                return this.Redirect("/Assets/Details?id=" + assetId);
            }
            catch
            {
                return this.View(new AssetInputModel());
            }
        }

        // GET: AssetsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.assetsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.assetsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            AssetEditModel editModel = await this.assetsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: AssetsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, AssetEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.assetsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.assetsService.EditAsync(id, editModel);
                if (!isEdited)
                {
                    return this.BadRequest();
                }

                return this.RedirectToAction(nameof(Details), new {id});
            }
            catch
            {
                return this.View(editModel);
            }
        }

        // GET: AssetsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.assetsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.assetsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            AssetDeleteModel deleteModel = await this.assetsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: AssetsController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.assetsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.assetsService.DeleteAsync(id);
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