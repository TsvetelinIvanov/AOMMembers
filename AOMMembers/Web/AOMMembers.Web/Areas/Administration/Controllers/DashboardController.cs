using Microsoft.AspNetCore.Mvc;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Administration.Dashboard;

namespace AOMMembers.Web.Areas.Administration.Controllers
{
    public class DashboardController : AdministrationController
    {
        private readonly IDashboardService dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = dashboardService.GetIndexViewModel();

            return this.View(viewModel);
        }
    }
}
