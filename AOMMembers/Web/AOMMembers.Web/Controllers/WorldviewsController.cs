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
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: WorldviewsController/Edit/"Id"
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

        // GET: WorldviewsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: WorldviewsController/Delete/"Id"
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