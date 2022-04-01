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
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: MediaMaterialsController/Edit/5
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

        // GET: MediaMaterialsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: MediaMaterialsController/Delete/"Id"
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