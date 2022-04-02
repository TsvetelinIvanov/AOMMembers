using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.SocietyActivities;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class SocietyActivitiesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISocietyActivitiesService societyActivitiesService;

        public SocietyActivitiesController(UserManager<ApplicationUser> userManager, ISocietyActivitiesService societyActivitiesService)
        {
            this.userManager = userManager;
            this.societyActivitiesService = societyActivitiesService;
        }

        // GET: SocietyActivitiesController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: SocietyActivitiesController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.societyActivitiesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            return this.View();
        }

        // GET: SocietyActivitiesController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new SocietyActivityInputModel());
        }

        // POST: SocietyActivitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(SocietyActivityInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/SocietyActivities/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string societyActivityId = await this.societyActivitiesService.CreateAsync(inputModel, userId);
                if (societyActivityId == SocietyActivityCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(SocietyActivityCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/SocietyActivities/Details?id=" + societyActivityId);
            }
            catch
            {
                return this.View(new SocietyActivityInputModel());
            }
        }

        // GET: SocietyActivitiesController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.societyActivitiesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.societyActivitiesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            SocietyActivityEditModel editModel = await this.societyActivitiesService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: SocietyActivitiesController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, SocietyActivityEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.societyActivitiesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.societyActivitiesService.EditAsync(id, editModel);
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

        // GET: SocietyActivitiesController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.societyActivitiesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.societyActivitiesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            SocietyActivityDeleteModel deleteModel = await this.societyActivitiesService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: SocietyActivitiesController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.societyActivitiesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.societyActivitiesService.DeleteAsync(id);
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