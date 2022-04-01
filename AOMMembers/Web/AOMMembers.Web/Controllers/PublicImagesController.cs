using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.PublicImages;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class PublicImagesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPublicImagesService publicImagesService;

        public PublicImagesController(UserManager<ApplicationUser> userManager, IPublicImagesService publicImagesService)
        {
            this.userManager = userManager;
            this.publicImagesService = publicImagesService;
        }

        // GET: PublicImagesController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: PublicImagesController/Details/"Id"
        public async Task<ActionResult> Details(string id)
        {
            return this.View();
        }

        // GET: PublicImagesController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new PublicImageInputModel());
        }

        // POST: PublicImagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(PublicImageInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/PublicImages/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string publicImageId = await this.publicImagesService.CreateAsync(inputModel, userId);
                if (publicImageId == PublicImageCreateWithoutMemberBadResult)
                {
                    return this.BadRequest(PublicImageCreateWithoutMemberBadRequest);
                }

                return this.Redirect("/PublicImages/Details?id=" + publicImageId);
            }
            catch
            {
                return this.View(new PublicImageInputModel());
            }
        }

        // GET: PublicImagesController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: PublicImagesController/Edit/"Id"
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

        // GET: PublicImagesController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: PublicImagesController/Delete/"Id"
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