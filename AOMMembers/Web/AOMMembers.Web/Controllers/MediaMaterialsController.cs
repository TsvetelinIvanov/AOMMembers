using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Common;

namespace AOMMembers.Web.Controllers
{
    public class MediaMaterialsController : BaseController
    {
        // GET: MediaMaterialsController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: MediaMaterialsController/Details/5
        public ActionResult Details(int id)
        {
            return this.View();
        }

        // GET: MediaMaterialsController/Create
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: MediaMaterialsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Create(IFormCollection collection)
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

        // GET: MediaMaterialsController/Edit/5
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: MediaMaterialsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: MediaMaterialsController/Delete/5
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: MediaMaterialsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Delete(int id, IFormCollection collection)
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