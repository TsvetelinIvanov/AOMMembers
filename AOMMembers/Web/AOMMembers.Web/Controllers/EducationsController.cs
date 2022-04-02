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
            if (await this.educationsService.IsAbsent(id))
            {
                return this.NotFound();
            }

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
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.educationsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.educationsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            EducationEditModel editModel = await this.educationsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: EducationsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, EducationEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.educationsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.educationsService.EditAsync(id, editModel);
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

        // GET: EducationsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.educationsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!await this.educationsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            EducationDeleteModel deleteModel = await this.educationsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: EducationsController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!await this.educationsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.educationsService.DeleteAsync(id);
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