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
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: QualificationsController/Edit/"Id"
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

        // GET: QualificationsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: QualificationsController/Delete/"Id"
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