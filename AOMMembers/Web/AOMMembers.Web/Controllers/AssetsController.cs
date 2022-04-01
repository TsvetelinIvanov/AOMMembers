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
                return Redirect("/Assets/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string assetId = await this.assetsService.CreateAsync(inputModel, userId);
                if (assetId == AssetCreateWithoutMaterialStateBadResult)
                {
                    return this.BadRequest(AssetCreateWithoutMaterialStateBadRequest);
                }

                return this.Redirect("/Assets/Details?id=" + assetId);
            }
            catch
            {
                return this.View(new AssetInputModel());
            }
        }

        // GET: AssetsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: AssetsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: AssetsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: AssetsController/Delete/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}