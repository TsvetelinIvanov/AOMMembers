using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.LawStates;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class LawStatesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILawStatesService lawStatesService;

        public LawStatesController(UserManager<ApplicationUser> userManager, ILawStatesService lawStatesService)
        {
            this.userManager = userManager;
            this.lawStatesService = lawStatesService;
        }

        // GET: LawStatesController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: LawStatesController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
        }

        // GET: LawStatesController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new LawStateInputModel());
        }

        // POST: LawStatesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(LawStateInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/LawStates/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string lawStateId = await this.lawStatesService.CreateAsync(inputModel, userId);
                if (lawStateId == LawStateCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(LawStateCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/LawStates/Details?id=" + lawStateId);
            }
            catch
            {
                return this.View(new LawStateInputModel());
            }
        }

        // GET: LawStatesController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: LawStatesController/Edit/"Id"
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

        // GET: LawStatesController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: LawStatesController/Delete/"Id"
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