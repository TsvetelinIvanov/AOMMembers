using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.SocietyHelps;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class SocietyHelpsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISocietyHelpsService societyHelpsService;

        public SocietyHelpsController(UserManager<ApplicationUser> userManager, ISocietyHelpsService societyHelpsService)
        {
            this.userManager = userManager;
            this.societyHelpsService = societyHelpsService;
        }

        // GET: SocietyHelpsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: SocietyHelpsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
        }

        // GET: SocietyHelpsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new SocietyHelpInputModel());
        }

        // POST: SocietyHelpsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(SocietyHelpInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/SocietyHelps/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string societyHelpId = await this.societyHelpsService.CreateAsync(inputModel, userId);
                if (societyHelpId == SocietyHelpCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(SocietyHelpCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/SocietyHelps/Details?id=" + societyHelpId);
            }
            catch
            {
                return this.View(new SocietyHelpInputModel());
            }
        }

        // GET: SocietyHelpsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: SocietyHelpsController/Edit/"Id"
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

        // GET: SocietyHelpsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: SocietyHelpsController/Delete/"Id"
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