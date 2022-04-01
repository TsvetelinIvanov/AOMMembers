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
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: SocietyActivitiesController/Edit/"Id"
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

        // GET: SocietyActivitiesController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: SocietyActivitiesController/Delete/"Id"
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