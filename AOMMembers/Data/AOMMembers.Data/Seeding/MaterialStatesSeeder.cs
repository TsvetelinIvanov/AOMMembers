using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class MaterialStatesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.MaterialStates.Any())
            {
                return;
            }

            List<(string Id, decimal Riches, decimal Money, decimal MonthIncome, string Description, string CitizenId)> materialStates = new List<(string Id, decimal Riches, decimal Money, decimal MonthIncome, string Description, string CitizenId)>
            {
                ("MaterialStateIdIvanIvanov", 92000, 0, 1550, "Имам апартамент в София, квартал Люлин 6 и кола. Нямам пари на срочен влог. Месечните ми доходи са от учителска заплата на пълен работен ден и лекторски часове.", "CitizenIdIvanIvanov"),
                ("MaterialStateIdStoyanDuvarov", 345000, 10000, 2100, "Имам къща с дворно място в София, квартал Бояна и кола. Също така имам срочен влог на стойност 10 000 лв. Месечните ми доходи са от заплата като граничен полицай на пълен работен ден и отработени извънредни часове.", "CitizenIdStoyanDuvarov"),
                ("MaterialStateIdBoykoArmeyski", 370000, 20000, 2700, "Имам къща с дворно място в София, квартал Симеоново и кола. Освен това имам срочен влог на стойност 20 000 лв. Месечните ми доходи са от заплата на пълен работен ден.", "CitizenIdBoykoArmeyski"),
                ("MaterialStateIdYustinianZakonov", 140000, 0, 2100, "Имам апартамент в София, квартал Западен парк. Нямам пари на срочен влог. Месечните ми доходи са от заплата на пълен работен ден.", "CitizenIdYustinianZakonov"),
                ("MaterialStateIdSokratPlatonov", 130000, 0, 1450, "Имам апартамент в София, квартал Обеля. Нямам пари на срочен влог. Месечните ми доходи са от учителска заплата на пълен работен ден и лекторски часове.", "CitizenIdSokratPlatonov"),
                ("MaterialStateIdArpaEfirova", 9000, 5000, 0, "Имам кола и срочен влог на стойност 5 000 лв. Нямам месечни доходи.", "CitizenIdArpaEfirova"),
                ("MaterialStateIdNikolaDaskalov", 110000, 0, 1500, "Имам апартамент в София, квартал Манастирски ливади. Нямам пари на срочен влог. Месечните ми доходи са от учителска заплата на пълен работен ден и лекторски часове.", "CitizenIdNikolaDaskalov"),
                ("MaterialStateIdMariaGospojina", 210000, 0, 1450, "Имам апартамент в София, квартал Гео Милев и кола. Нямам пари на срочен влог. Месечните ми доходи са от учителска заплата на пълен работен ден и лекторски часове.", "CitizenIdMariaGospojina"),
                ("MaterialStateIdGabrielaFrankfurtska", 10000, 10000, 0, "Имам срочен влог на стойност 10 000 лв. Нямам месечни доходи.", "CitizenIdGabrielaFrankfurtska"),
                ("MaterialStateIdSnaksMacNinsky", 174000, 0, 2300, "Имам апартамент в София, квартал Дианабад и кола. Нямам пари на срочен влог. Месечните ми доходи са от заплата на пълен работен ден.", "CitizenIdSnaksMacNinsky"),
                ("MaterialStateIdElenaRudolf", 140000, 0, 1450, "Имам апартамент в София, квартал Надежда 1. Нямам пари на срочен влог. Месечните ми доходи са от учителска заплата на пълен работен ден и лекторски часове.", "CitizenIdElenaRudolf"),
                ("MaterialStateIdIbrahimKoch", 130000, 0, 2400, "Имам апартамент в София, квартал Красна поляна. Нямам пари на срочен влог. Месечните ми доходи са от заплата на пълен работен ден.", "CitizenIdIbrahimKoch"),
                ("MaterialStateIdGoritsaDubova", 120000, 0, 1450, "Имам апартамент в София, квартал Люлин 1. Нямам пари на срочен влог. Месечните ми доходи са от учителска заплата на пълен работен ден и лекторски часове.", "CitizenIdGoritsaDubova")
            };

            foreach ((string Id, decimal Riches, decimal Money, decimal MonthIncome, string Description, string CitizenId) materialState in materialStates)
            {
                MaterialState dbMaterialState = new MaterialState
                {
                    Riches = materialState.Riches,
                    Money = materialState.Money,
                    MonthIncome = materialState.MonthIncome,
                    Description = materialState.Description,
                    CitizenId = materialState.CitizenId
                };
                dbMaterialState.Id = materialState.Id;
                await dbContext.MaterialStates.AddAsync(dbMaterialState);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}