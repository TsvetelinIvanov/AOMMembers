using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Common;

namespace AOMMembers.Web.Controllers
{
    public class CitizensController : BaseController
    {
        // GET: CitizensController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: CitizensController/Details/5
        public ActionResult Details(int id)
        {
            return this.View();
        }

        // GET: CitizensController/Create
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: CitizensController/Create
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

        // GET: CitizensController/Edit/5
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: CitizensController/Edit/5
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

        // GET: CitizensController/Delete/5
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: CitizensController/Delete/5
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