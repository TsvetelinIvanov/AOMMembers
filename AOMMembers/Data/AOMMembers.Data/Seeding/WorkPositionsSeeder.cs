using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class WorkPositionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.WorkPositions.Any())
            {
                return;
            }

            List<(string Id, string Name, string Description, bool IsCurrent, DateTime StartDate, string CareerId)> workPositions = new List<(string Id, string Name, string Description, bool IsCurrent, DateTime StartDate, string CareerId)>
            {
                ("WorkPositionIdIvanIvanov", "Учител", "Учител по \"История и цивилизация\" в Софийска гимназия по хлебни и сладкарски технологии.", true, new DateTime(2009, 9, 1), "CareerIdIvanIvanov"),                
                ("WorkPositionIdStoyanDuvarov", "Граничен полицай", "Началник смяна в Гранична полиция на летище София.", true, new DateTime(2004, 10, 1), "CareerIdStoyanDuvarov"),
                ("WorkPositionIdBoykoArmeyski", "Началник физическа охрана", "Началник физическа охрана на група обекти във \"Форс Делта\" ООД", true, new DateTime(1994, 1, 1), "CareerIdBoykoArmeyski"),
                ("WorkPositionIdYustinianZakonov", "Адвокатски сътрудник", "Адвокатски сътрудник в адвокатска кантора \"Расташки\".", true, new DateTime(2017, 6, 1), "CareerIdYustinianZakonov"),
                ("WorkPositionIdSokratPlatonov", "Учител", "Учител по философски дисциплини в Софийска математическа гимназия \"Паисий Хилендарски\".", true, new DateTime(2015, 9, 1), "CareerIdSokratPlatonov"),
                ("WorkPositionIdNikolaDaskalov", "Учител", "Начален учител в 119 СУ \"Акад. Михаил Арнаудов\".", true, new DateTime(1986, 9, 1), "CareerIdNikolaDaskalov"),
                ("WorkPositionIdMariaGospojina", "Учител", "Начален учител в 112 ОУ \"Стоян Заимов\".", true, new DateTime(2009, 9, 1), "CareerIdMariaGospojina"),
                ("WorkPositionIdSnaksMacNinsky", "Консултант", "Консултант в адвокатска кантора \"Иванчов & Партньори\".", true, new DateTime(2013, 1, 1), "CareerIdSnaksMacNinsky"),
                ("WorkPositionIdElenaRudolf", "Учител", "Учител по \"История и цивилизация\" в НПГ по прецизна техника и оптика \"М. В. Ломоносов\".", true, new DateTime(2021, 9, 1), "CareerIdElenaRudolf"),                
                ("WorkPositionIdIbrahimKoch", "Техник", "Техник в Ел Инс Проект-ЕООД.", true, new DateTime(2007, 1, 1), "CareerIdIbrahimKoch"),
                ("WorkPositionIdGoritsaDubova", "История", "Учител по \"История и цивилизация\" в Софийска професионална гимназия по туризъм.", true, new DateTime(2016, 9, 1), "CareerIdGoritsaDubova")
            };

            foreach ((string Id, string Name, string Description, bool IsCurrent, DateTime StartDate, string CareerId) workPosition in workPositions)
            {
                WorkPosition dbWorkPosition = new WorkPosition
                {
                    Name = workPosition.Name,
                    Description = workPosition.Description,
                    IsCurrent = workPosition.IsCurrent,
                    StartDate = workPosition.StartDate,                    
                    CareerId = workPosition.CareerId
                };
                dbWorkPosition.Id = workPosition.Id;
                await dbContext.WorkPositions.AddAsync(dbWorkPosition);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}