using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Worldviews;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class WorldviewsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWorldviewsService worldviewsService;

        public WorldviewsController(UserManager<ApplicationUser> userManager, IWorldviewsService worldviewsService)
        {
            this.userManager = userManager;
            this.worldviewsService = worldviewsService;
        }

        // GET: WorldviewsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: WorldviewsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.worldviewsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            return this.View();
        }

        // GET: WorldviewsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new WorldviewInputModel());
        }

        // POST: WorldviewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(WorldviewInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Worldviews/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string worldviewId = await this.worldviewsService.CreateAsync(inputModel, userId);
                if (worldviewId == WorldviewCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(WorldviewCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/Worldviews/Details?id=" + worldviewId);
            }
            catch
            {
                return this.View(new WorldviewInputModel());
            }
        }

        // GET: WorldviewsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.worldviewsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.worldviewsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            WorldviewEditModel editModel = await this.worldviewsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: WorldviewsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, WorldviewEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.worldviewsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.worldviewsService.EditAsync(id, editModel);
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

        // GET: WorldviewsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.worldviewsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.worldviewsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            WorldviewDeleteModel deleteModel = await this.worldviewsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: WorldviewsController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.worldviewsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.worldviewsService.DeleteAsync(id);
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