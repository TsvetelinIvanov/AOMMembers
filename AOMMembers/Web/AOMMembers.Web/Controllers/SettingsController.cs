using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Settings;
using static AOMMembers.Common.GlobalConstants;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Web.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISettingsService settingsService;
        private readonly IDeletableEntityRepository<Setting> repository;

        public SettingsController(UserManager<ApplicationUser> userManager, ISettingsService settingsService, IDeletableEntityRepository<Setting> repository)
        {
            this.userManager = userManager;
            this.settingsService = settingsService;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            IEnumerable<SettingViewModel> settings = this.settingsService.GetAllFromMember("CitizenId");
            SettingsListViewModel model = new SettingsListViewModel { Settings = settings };

            return this.View(model);
        }

        public async Task<IActionResult> InsertSetting()
        {
            Random random = new Random();
            Setting setting = new Setting() { Name = $"Name_{random.Next()}", Value = $"Value_{random.Next()}", CitizenId = "CitizenId", CreatedOn = DateTime.UtcNow };

            await this.repository.AddAsync(setting);
            await this.repository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: SettingsController/Create
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Create()
        {
            return this.View(new SettingInputModel());
        }

        // POST: SettingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Create(SettingInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Settings/Create");
            }

            try
            {
                string userId = this.userManager.GetUserId(this.User);
                string settingId = await this.settingsService.CreateAsync(inputModel, userId);
                if (settingId == SettingCreateWithoutCitizenBadResult)
                {
                    return this.BadRequest(SettingCreateWithoutCitizenBadRequest);
                }
                
                return this.Redirect("/Settings/Details?id=" + settingId);
            }
            catch
            {
                return this.View(new SettingInputModel());
            }
        }

        // GET: SettingsController/Edit/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Edit(string id)
        {
            return this.View();
        }

        // POST: SettingsController/Edit/"Id"
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
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

        // GET: SettingsController/Delete/"Id"
        [Authorize(Roles = AdministratorRoleName + ", " + MemberRoleName)]
        public ActionResult Delete(string id)
        {
            return this.View();
        }

        // POST: SettingsController/Delete/"Id"
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