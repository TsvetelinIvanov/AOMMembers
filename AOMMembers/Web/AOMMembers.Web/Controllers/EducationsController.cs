using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Educations;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class EducationsController : BaseController 
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEducationsService educationsService;

        public EducationsController(UserManager<ApplicationUser> userManager, IEducationsService educationsService)
        {
            this.userManager = userManager;
            this.educationsService = educationsService;
        }

        // GET: EducationsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: EducationsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
        }

        // GET: EducationsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new EducationInputModel());
        }

        // POST: EducationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(EducationInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Educations/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string educationId = await this.educationsService.CreateAsync(inputModel, userId);
                if (educationId == EducationCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(EducationCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/Educations/Details?id=" + educationId);
            }
            catch
            {
                return this.View(new EducationInputModel());
            }
        }

        // GET: EducationsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: EducationsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
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

        // GET: EducationsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: EducationsController/Delete/"Id"
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