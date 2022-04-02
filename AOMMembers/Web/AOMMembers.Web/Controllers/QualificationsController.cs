using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Qualifications;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class QualificationsController : BaseController 
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IQualificationsService qualificationsService;

        public QualificationsController(UserManager<ApplicationUser> userManager, IQualificationsService qualificationsService)
        {
            this.userManager = userManager;
            this.qualificationsService = qualificationsService;
        }

        // GET: QualificationsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: QualificationsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.qualificationsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            return this.View();
        }

        // GET: QualificationsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new QualificationInputModel());
        }

        // POST: QualificationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(QualificationInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Qualifications/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string qualificationId = await this.qualificationsService.CreateAsync(inputModel, userId);
                if (qualificationId == QualificationCreateWithoutEducationBadResult)
                {
                    return this.BadRequest(QualificationCreateWithoutEducationBadRequest);
                }

                return this.Redirect("/Qualifications/Details?id=" + qualificationId);
            }
            catch
            {
                return this.View(new QualificationInputModel());
            }
        }

        // GET: QualificationsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.qualificationsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.qualificationsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            QualificationEditModel editModel = await this.qualificationsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: QualificationsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, QualificationEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.qualificationsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.qualificationsService.EditAsync(id, editModel);
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

        // GET: QualificationsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.qualificationsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.qualificationsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            QualificationDeleteModel deleteModel = await this.qualificationsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: QualificationsController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.qualificationsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.qualificationsService.DeleteAsync(id);
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