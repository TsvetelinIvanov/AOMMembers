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
            if (await this.lawProblemsService.IsAbsent(id))
            {
                return this.NotFound();
            }

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
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.lawProblemsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.lawProblemsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            LawProblemEditModel editModel = await this.lawProblemsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: LawProblemsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, LawProblemEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.lawProblemsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.lawProblemsService.EditAsync(id, editModel);
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

        // GET: LawProblemsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.lawProblemsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.lawProblemsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            LawProblemDeleteModel deleteModel = await this.lawProblemsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: LawProblemsController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.lawProblemsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.lawProblemsService.DeleteAsync(id);
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