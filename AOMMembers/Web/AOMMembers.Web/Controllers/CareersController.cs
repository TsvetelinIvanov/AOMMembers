using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Careers;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class CareersController : BaseController 
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICareersService careersService;

        public CareersController(UserManager<ApplicationUser> userManager, ICareersService careersService)
        {
            this.userManager = userManager;
            this.careersService = careersService;
        }

        // GET: CareersController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: CareersController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View(new CareerInputModel());
        }

        // GET: CareersController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: CareersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(CareerInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Careers/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string careerId = await this.careersService.CreateAsync(inputModel, userId);
                if (careerId == CareerCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(CareerCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/Careers/Details?id=" + careerId);
            }
            catch
            {
                return this.View(new CareerInputModel());
            }
        }

        // GET: CareersController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: CareersController/Edit/5
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

        // GET: CareersController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: CareersController/Delete/"Id"
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