using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    internal class SettingsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Settings.Any())
            {
                return;
            }

            List<(string Id, string Name, string Value, string CitizenId)> settings = new List<(string Id, string Name, string Value, string CitizenId)>
            {
                ("SettingIdIvanIvanov", "Бъбречни проблеми", "В бъбреците ми се образуват камъни и има опасност да получа криза по време на заседание.", "CitizenIdIvanIvanov"),
                ("SettingIdBoykoArmeyski", "Проблеми със зрението", "Зрението ми е отслабнало и се налага да ползвам очила или лещи не само при четене.", "CitizenIdBoykoArmeyski"),
                ("SettingIdYustinianZakonov", "Детски мечти", "Когато бях дете мечтаех да стана космонавт.", "CitizenIdYustinianZakonov"),
                ("SettingIdArpaEfirova", "Произход на името ми", "Кръстена съм на ARPA (Advanced Research Projects Agency), която сега се казва DARPA (Defense Advanced Research Projects Agency) и е участвала в разработването на ARPANET от която произлиза Internet.", "CitizenIdArpaEfirova"),
                ("SettingIdNikolaDaskalov", "Учителска традиция", "Родителите ми и техните родители, също като мен са били учители", "CitizenIdNikolaDaskalov"),
                ("SettingIdMariaGospojina", "Многодетна майка", "Майка съм на 6 деца.", "CitizenIdMariaGospojina")
            };

            foreach ((string Id, string Name, string Value, string CitizenId) setting in settings)
            {
                Setting dbSetting = new Setting
                {
                    Name = setting.Name,
                    Value = setting.Value,                    
                    CitizenId = setting.CitizenId
                };
                dbSetting.Id = setting.Id;
                await dbContext.Settings.AddAsync(dbSetting);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}