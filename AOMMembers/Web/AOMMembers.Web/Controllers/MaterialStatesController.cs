using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.MaterialStates;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class MaterialStatesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMaterialStatesService materialStatesService;

        public MaterialStatesController(UserManager<ApplicationUser> userManager, IMaterialStatesService materialStatesService)
        {
            this.userManager = userManager;
            this.materialStatesService = materialStatesService;
        }

        // GET: MaterialStatesController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: MaterialStatesController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
        }

        // GET: MaterialStatesController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new MaterialStateInputModel());
        }

        // POST: MaterialStatesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(MaterialStateInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/MaterialStates/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string materialStateId = await this.materialStatesService.CreateAsync(inputModel, userId);
                if (materialStateId == MaterialStateCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(MaterialStateCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/MaterialStates/Details?id=" + materialStateId);
            }
            catch
            {
                return this.View(new MaterialStateInputModel());
            }
        }

        // GET: MaterialStatesController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: MaterialStatesController/Edit/5
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

        // GET: MaterialStatesController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: MaterialStatesController/Delete/"Id"
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