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

        // GET: MaterialStatesController/Index/"Id"
        public async Task<ActionResult> Index(string id)
        {
            if (await this.materialStatesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            MaterialStateViewModel viewModel = await this.materialStatesService.GetViewModelByIdAsync(id);
            IEnumerable<MaterialStateViewModel> viewModelWrapper = new[] { viewModel };

            return this.View(viewModelWrapper);
        }

        // GET: MaterialStatesController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.materialStatesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            MaterialStateDetailsViewModel viewModel = await this.materialStatesService.GetDetailsByIdAsync(id);            

            return this.View(viewModel);
        }

        // GET: MaterialStatesController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            string userId = this.userManager.GetUserId(this.User);
            if (this.materialStatesService.IsCreated(userId))
            {
                return this.BadRequest(CreateCreatedEntityBadResult);
            }

            return this.View(new MaterialStateInputModel());
        }

        // POST: MaterialStatesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(MaterialStateInputModel inputModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (this.materialStatesService.IsCreated(userId))
            {
                return this.BadRequest(CreateCreatedEntityBadResult);
            }

            if (!ModelState.IsValid)
            {
                return Redirect("/MaterialStates/Create");
            }

            try
            {                
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
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.materialStatesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.materialStatesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            MaterialStateEditModel editModel = await this.materialStatesService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: MaterialStatesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, MaterialStateEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.materialStatesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.materialStatesService.EditAsync(id, editModel);
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

        // GET: MaterialStatesController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.materialStatesService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.materialStatesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            MaterialStateDeleteModel deleteModel = await this.materialStatesService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: MaterialStatesController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.materialStatesService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.materialStatesService.DeleteAsync(id);
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