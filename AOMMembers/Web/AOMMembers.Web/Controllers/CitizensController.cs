using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Citizens;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class CitizensController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICitizensService citizensService;

        public CitizensController(UserManager<ApplicationUser> userManager, ICitizensService citizensService)
        {
            this.userManager = userManager;
            this.citizensService = citizensService;
        }

        // GET: CitizensController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: CitizensController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.citizensService.IsAbsent(id))
            {
                return this.NotFound();
            }

            return this.View();
        }

        // GET: CitizensController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new CitizenInputModel());
        }

        // POST: CitizensController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(CitizenInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Citizens/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string citizenId = await this.citizensService.CreateAsync(inputModel, userId);
                if (citizenId == CitizenCreateWithoutMemberBadResult)
                {
                    return this.BadRequest(CitizenCreateWithoutMemberBadRequest);
                }

                return this.Redirect("/Citizens/Details?id=" + citizenId);
            }
            catch
            {
                return this.View(new CitizenInputModel());
            }
        }

        // GET: CitizensController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.citizensService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.citizensService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            CitizenEditModel editModel = await this.citizensService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: CitizensController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, CitizenEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.citizensService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.citizensService.EditAsync(id, editModel);
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

        // GET: CitizensController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.citizensService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.citizensService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            CitizenDeleteModel deleteModel = await this.citizensService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: CitizensController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.citizensService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.citizensService.DeleteAsync(id);
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