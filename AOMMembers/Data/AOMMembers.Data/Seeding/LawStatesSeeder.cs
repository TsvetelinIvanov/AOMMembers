using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class LawStatesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.LawStates.Any())
            {
                return;
            }

            List<(string Id, string Condition, string CitizenId)> lawStates = new List<(string Id, string Condition, string CitizenId)>
            {
                ("LawStateIdIvanIvanov", "Неосъждан", "CitizenIdIvanIvanov"),
                ("LawStateIdStoyanDuvarov", "Неосъждан", "CitizenIdStoyanDuvarov"),
                ("LawStateIdBoykoArmeyski", "Неосъждан", "CitizenIdBoykoArmeyski"),
                ("LawStateIdYustinianZakonov", "Неосъждан", "CitizenIdYustinianZakonov"),
                ("LawStateIdSokratPlatonov", "Неосъждан", "CitizenIdSokratPlatonov"),
                ("LawStateIdArpaEfirova", "Неосъждан", "CitizenIdArpaEfirova"),
                ("LawStateIdNikolaDaskalov", "Неосъждан", "CitizenIdNikolaDaskalov"),
                ("LawStateIdMariaGospojina", "Неосъждан", "CitizenIdMariaGospojina"),
                ("LawStateIdGabrielaFrankfurtska", "Неосъждан", "CitizenIdGabrielaFrankfurtska"),
                ("LawStateIdSnaksMacNinsky", "Неосъждан", "CitizenIdSnaksMacNinsky"),
                ("LawStateIdElenaRudolf", "Неосъждан", "CitizenIdElenaRudolf"),
                ("LawStateIdIbrahimKoch", "Неосъждан", "CitizenIdIbrahimKoch"),
                ("LawStateIdGoritsaDubova", "Неосъждан", "CitizenIdGoritsaDubova")
            };

            foreach ((string Id, string Condition, string CitizenId) lawState in lawStates)
            {
                LawState dbLawState = new LawState
                {
                    Condition = lawState.Condition,
                    CitizenId = lawState.CitizenId
                };
                dbLawState.Id = lawState.Id;
                await dbContext.LawStates.AddAsync(dbLawState);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}