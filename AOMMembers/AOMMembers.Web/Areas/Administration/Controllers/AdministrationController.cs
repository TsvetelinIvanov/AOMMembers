using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AOMMembers.Common;
using AOMMembers.Web.Controllers;

namespace AOMMembers.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}