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
            if (await this.careersService.IsAbsent(id))
            {
                return this.NotFound();
            }

            return this.View();
        }

        // GET: CareersController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new CareerInputModel());
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
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.careersService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.careersService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            CareerEditModel editModel = await this.careersService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: CareersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, CareerEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.careersService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.careersService.EditAsync(id, editModel);
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

        // GET: CareersController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.careersService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.careersService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            CareerDeleteModel deleteModel = await this.careersService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: CareersController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.careersService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.careersService.DeleteAsync(id);
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