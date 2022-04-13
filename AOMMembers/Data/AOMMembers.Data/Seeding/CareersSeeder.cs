using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class CareersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Careers.Any())
            {
                return;
            }

            List<(string Id, string Description, string CitizenId)> careers = new List<(string Id, string Description, string CitizenId)>
            {
                ("CareerIdIvanIvanov", "Работя като учител в Софийска гимназия по хлебни и сладкарски технологии.", "CitizenIdIvanIvanov"),
                ("CareerIdStoyanDuvarov", "Работя в Гранична полиция на летище София.", "CitizenIdStoyanDuvarov"),
                ("CareerIdBoykoArmeyski", "Работя като отговорник на група обекти във \"Форс Делта\" ООД.", "CitizenIdBoykoArmeyski"),
                ("CareerIdYustinianZakonov", "Работя в адвокатска кантора \"Расташки\".", "CitizenIdYustinianZakonov"),
                ("CareerIdSokratPlatonov", "Работя като учител в Софийска математическа гимназия \"Паисий Хилендарски\".", "CitizenIdSokratPlatonov"),
                ("CareerIdArpaEfirova", "В момента не работя, а уча история в Софийски университет \"Св. Климент Охридски\".", "CitizenIdArpaEfirova"),
                ("CareerIdNikolaDaskalov", "Работя като начален учител в 119 СУ \"Акад. Михаил Арнаудов\".", "CitizenIdNikolaDaskalov"),
                ("CareerIdMariaGospojina", "Работя като начален учител в 112 ОУ \"Стоян Заимов\".", "CitizenIdMariaGospojina"),
                ("CareerIdGabrielaFrankfurtska", "В момента не работя, а уча история в Софийски университет \"Св. Климент Охридски\".", "CitizenIdGabrielaFrankfurtska"),
                ("CareerIdSnaksMacNinsky", "Работя в адвокатска кантора \"Иванчов & Партньори\".", "CitizenIdSnaksMacNinsky"),
                ("CareerIdElenaRudolf", "Работя като учител в НПГ по прецизна техника и оптика \"М. В. Ломоносов\".", "CitizenIdElenaRudolf"),
                ("CareerIdIbrahimKoch", "Работя в Ел Инс Проект-ЕООД.", "CitizenIdIbrahimKoch"),
                ("CareerIdGoritsaDubova", "Работя като учител в Софийска професионална гимназия по туризъм.", "CitizenIdGoritsaDubova")
            };

            foreach ((string Id, string Description, string CitizenId) career in careers)
            {
                Career dbCareer = new Career
                {
                    Description = career.Description,
                    CitizenId = career.CitizenId
                };
                dbCareer.Id = career.Id;
                await dbContext.Careers.AddAsync(dbCareer);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}