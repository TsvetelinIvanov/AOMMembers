using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.LawProblems;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class LawProblemsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILawProblemsService lawProblemsService;

        public LawProblemsController(UserManager<ApplicationUser> userManager, ILawProblemsService lawProblemsService)
        {
            this.userManager = userManager;
            this.lawProblemsService = lawProblemsService;
        }

        // GET: LawProblemsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: LawProblemsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
        }

        // GET: LawProblemsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new LawProblemInputModel());
        }

        // POST: LawProblemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(LawProblemInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/LawProblems/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string lawProblemId = await this.lawProblemsService.CreateAsync(inputModel, userId);
                if (lawProblemId == LawProblemCreateWithoutLawStateBadResult)
                {
                    return this.BadRequest(LawProblemCreateWithoutLawStateBadRequest);
                }

                return this.Redirect("/LawProblems/Details?id=" + lawProblemId);
            }
            catch
            {
                return this.View(new LawProblemInputModel());
            }
        }

        // GET: LawProblemsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: LawProblemsController/Edit/"Id"
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

        // GET: LawProblemsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: LawProblemsController/Delete/"Id"
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