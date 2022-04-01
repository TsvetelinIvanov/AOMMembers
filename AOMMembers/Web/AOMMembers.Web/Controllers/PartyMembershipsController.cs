using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.PartyMemberships;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class PartyMembershipsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPartyMembershipsService partyMembershipsService;

        public PartyMembershipsController(UserManager<ApplicationUser> userManager, IPartyMembershipsService partyMembershipsService)
        {
            this.userManager = userManager;
            this.partyMembershipsService = partyMembershipsService;
        }

        // GET: PartyMembershipsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: PartyMembershipsController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
        }

        // GET: PartyMembershipsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new PartyMembershipInputModel());
        }

        // POST: PartyMembershipsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(PartyMembershipInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/PartyMemberships/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string partyMembershipId = await this.partyMembershipsService.CreateAsync(inputModel, userId);
                if (partyMembershipId == PartyMembershipCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(PartyMembershipCreateWithoutCitizenBadRequest);
                }

                return this.Redirect("/PartyMemberships/Details?id=" + partyMembershipId);
            }
            catch
            {
                return this.View(new PartyMembershipInputModel());
            }
        }

        // GET: PartyMembershipsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: PartyMembershipsController/Edit/"Id"
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

        // GET: PartyMembershipsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: PartyMembershipsController/Delete/"Id"
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