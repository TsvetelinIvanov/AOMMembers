using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class PartyMembershipsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PartyMemberships.Any())
            {
                return;
            }

            List<(string Id, string PartyName, string Description, bool IsCurrent, DateTime StartDate, string CitizenId)> partyMemberships = new List<(string Id, string PartyName, string Description, bool IsCurrent, DateTime StartDate, string CitizenId)>
            {
                ("PartyMembershipIdIvanIvanov", "АОМ", "Съосновател, Главен координатор и Координатор по икономически политики в АОМ.", true, new DateTime(2019, 12, 1), "CitizenIdIvanIvanov"),
                ("PartyMembershipIdStoyanDuvarov", "АОМ", "Съосновател и Координатор на политики по вътрешните работи в АОМ.", true, new DateTime(2019, 12, 1), "CitizenIdStoyanDuvarov"),
                ("PartyMembershipIdBoykoArmeyski", "АОМ", "Съосновател и Координатор на политики по национална сигурност и отбрана в АОМ.", true, new DateTime(2019, 12, 12), "CitizenIdBoykoArmeyski"),
                ("PartyMembershipIdYustinianZakonov", "АОМ", "Съосновател и Координатор на политики за правосъдие в АОМ.", true, new DateTime(2019, 12, 1), "CitizenIdYustinianZakonov"),
                ("PartyMembershipIdSokratPlatonov", "АОМ", "Съосновател и Координатор на политики за наука и култура в АОМ.", true, new DateTime(2019, 12, 1), "CitizenIdSokratPlatonov"),
                ("PartyMembershipIdArpaEfirova", "АОМ", "Съосновател и Координатор на политики за медии и комуникации в АОМ.", true, new DateTime(2019, 12, 1), "CitizenIdArpaEfirova"),
                ("PartyMembershipIdNikolaDaskalov", "АОМ", "Съосновател и Координатор на политики за образование в АОМ.", true, new DateTime(2019, 12, 12), "CitizenIdNikolaDaskalov"),
                ("PartyMembershipIdMariaGospojina", "АОМ", "Съосновател и Координатор на политики по семейните въпроси в АОМ.", true, new DateTime(2019, 12, 1), "CitizenIdMariaGospojina"),
                ("PartyMembershipIdGabrielaFrankfurtska", "АОМ", "Съосновател и Координатор на политики по еврочленство и евроитеграция в АОМ.", true, new DateTime(2019, 12, 1), "CitizenIdGabrielaFrankfurtska"),
                ("PartyMembershipIdSnaksMacNinsky", "АОМ", "Съосновател и Координатор на политики за външна политика в АОМ.", true, new DateTime(2019, 12, 1), "CitizenIdSnaksMacNinsky"),
                ("PartyMembershipIdElenaRudolf", "АОМ", "Съосновател и Координатор на политики за транспорт в АОМ", true, new DateTime(2019, 12, 1), "CitizenIdElenaRudolf"),
                ("PartyMembershipIdIbrahimKoch", "АОМ", "Съосновател и Координатор на политики за селско стопанство в АОМ.", true, new DateTime(2019, 12, 1), "CitizenIdIbrahimKoch"),
                ("PartyMembershipIdGoritsaDubova", "АОМ", "Съосновател и Координатор на политики за енергетика и екология в АОМ.", true, new DateTime(2019, 12, 1), "CitizenIdGoritsaDubova")
            };

            foreach ((string Id, string PartyName, string Description, bool IsCurrent, DateTime StartDate, string CitizenId) partyMembership in partyMemberships)
            {
                PartyMembership dbPartyMembership = new PartyMembership
                {
                    PartyName = partyMembership.PartyName,
                    Description = partyMembership.Description,
                    IsCurrent = partyMembership.IsCurrent,
                    StartDate = partyMembership.StartDate,                    
                    CitizenId = partyMembership.CitizenId
                };
                dbPartyMembership.Id = partyMembership.Id;
                await dbContext.PartyMemberships.AddAsync(dbPartyMembership);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}