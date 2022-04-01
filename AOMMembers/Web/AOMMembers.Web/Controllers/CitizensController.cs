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
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: CitizensController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
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

        // GET: CitizensController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: CitizensController/Delete/"Id"
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