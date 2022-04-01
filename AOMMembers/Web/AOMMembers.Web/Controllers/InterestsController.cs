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

        // GET: InterestsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: InterestsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
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
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: InterestsController/Edit/"Id"
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

        // GET: InterestsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: InterestsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}