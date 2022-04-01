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

        // GET: WorkPositionsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: WorkPositionsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
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
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: WorkPositionsController/Edit/"Id"
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

        // GET: WorkPositionsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: WorkPositionsController/Delete/"Id"
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