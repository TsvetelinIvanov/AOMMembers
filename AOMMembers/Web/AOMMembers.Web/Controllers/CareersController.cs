using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Common;

namespace AOMMembers.Web.Controllers
{
    public class CareersController : BaseController 
    {
        // GET: CareersController
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: CareersController/Details/5
        public ActionResult Details(int id)
        {
            return this.View();
        }

        // GET: CareersController/Create
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: CareersController/Create
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

        // GET: CareersController/Edit/5
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: CareersController/Edit/5
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

        // GET: CareersController/Delete/5
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.MemberRoleName)]
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: CareersController/Delete/5
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