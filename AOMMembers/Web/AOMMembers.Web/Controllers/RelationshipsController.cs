using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Relationships;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class RelationshipsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRelationshipsService relationshipsService;

        public RelationshipsController(UserManager<ApplicationUser> userManager, IRelationshipsService relationshipsService)
        {
            this.userManager = userManager;
            this.relationshipsService = relationshipsService;
        }

        // GET: RelationshipsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: RelationshipsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            if (await this.relationshipsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            return this.View();
        }

        // GET: RelationshipsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new RelationshipInputModel());
        }

        // POST: RelationshipsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(RelationshipInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Relationships/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string relationshipId = await this.relationshipsService.CreateAsync(inputModel, userId);
                if (relationshipId == RelationshipCreateWithoutMemberBadResult)
                {
                    return this.BadRequest(RelationshipCreateWithoutMemberBadRequest);
                }

                if (relationshipId == RelationshipCreateWithInexistantCitizenBadResult)
                {
                    return this.BadRequest(RelationshipCreateWithInexistantCitizenBadRequest);
                }

                return this.Redirect("/Relationships/Details?id=" + relationshipId);
            }
            catch
            {
                return this.View(new RelationshipInputModel());
            }
        }

        // GET: RelationshipsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (await this.relationshipsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.relationshipsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            RelationshipEditModel editModel = await this.relationshipsService.GetEditModelByIdAsync(id);

            return this.View(editModel);
        }

        // POST: RelationshipsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(string id, RelationshipEditModel editModel)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.relationshipsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedEditMessage);
            }

            if (!ModelState.IsValid)
            {
                return this.View(editModel);
            }

            try
            {
                bool isEdited = await this.relationshipsService.EditAsync(id, editModel);
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

        // GET: RelationshipsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (await this.relationshipsService.IsAbsent(id))
            {
                return this.NotFound();
            }

            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.relationshipsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            RelationshipDeleteModel deleteModel = await this.relationshipsService.GetDeleteModelByIdAsync(id);

            return this.View(deleteModel);
        }

        // POST: RelationshipsController/Delete/"Id"
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string userId = this.userManager.GetUserId(this.User);
            if (!this.User.IsInRole(AdministratorRoleName) && !await this.relationshipsService.IsFromMember(id, userId))
            {
                return this.Unauthorized(UnauthorizedDeleteMessage);
            }

            try
            {
                bool isDeleted = await this.relationshipsService.DeleteAsync(id);
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