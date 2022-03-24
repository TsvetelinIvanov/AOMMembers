using Microsoft.AspNetCore.Mvc;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Administration.Dashboard;

namespace AOMMembers.Web.Areas.Administration.Controllers
{
    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount() };

            return this.View(viewModel);
        }
    }
}
