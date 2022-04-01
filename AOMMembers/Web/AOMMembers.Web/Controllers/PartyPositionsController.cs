using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.PartyPositions;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class PartyPositionsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPartyPositionsService partyPositionsService;

        public PartyPositionsController(UserManager<ApplicationUser> userManager, IPartyPositionsService partyPositionsService)
        {
            this.userManager = userManager;
            this.partyPositionsService = partyPositionsService;
        }

        // GET: PartyPositionsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: PartyPositionsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
        }

        // GET: PartyPositionsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new PartyPositionInputModel());
        }

        // POST: PartyPositionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(PartyPositionInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/PartyPositions/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string partyPositionId = await this.partyPositionsService.CreateAsync(inputModel, userId);
                if (partyPositionId == PartyPositionCreateWithoutMemberBadResult)
                {
                    return this.BadRequest(PartyPositionCreateWithoutMemberBadRequest);
                }

                return this.Redirect("/PartyPositions/Details?id=" + partyPositionId);
            }
            catch
            {
                return this.View(new PartyPositionInputModel());
            }
        }

        // GET: PartyPositionsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: PartyPositionsController/Edit/"Id"
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

        // GET: PartyPositionsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: PartyPositionsController/Delete/5
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