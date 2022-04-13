using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class PartyPositionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PartyPositions.Any())
            {
                return;
            }

            List<(string Id, string Name, string Description, bool IsCurrent, DateTime StartDate, string MemberId)> partyPositions = new List<(string Id, string Name, string Description, bool IsCurrent, DateTime StartDate, string MemberId)>
            {
                ("PartyPositionIdIvanIvanov", "ГК и КИП", "Главен координатор и Координатор по икономически политики - координира действията на членовете и ръководи партията съгласно правата и задълженията дадени му от Устава (чл.27 - 32), а също така се грижи за партийните политики в икономическата област, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdIvanIvanov"),
                ("PartyPositionIdStoyanDuvarov", "КПВР", "Координатор на политики по вътрешните работи - координира и се грижи за партийните политики в областта на вътрешните работи, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdStoyanDuvarov"),
                ("PartyPositionIdBoykoArmeyski", "КПНСО", "Координатор на политики по национална сигурност и отбрана - координира и се грижи за партийните политики в областта на националната сигурност и отбраната, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdBoykoArmeyski"),
                ("PartyPositionIdYustinianZakonov", "КПП", "Координатор на политики за правосъдие - координира и се грижи за партийните политики в областта на правосъдието, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdYustinianZakonov"),
                ("PartyPositionIdSokratPlatonov", "КПНК", "Координатор на политики за наука и култура - координира и се грижи за партийните политики в областта на науката и културата, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdSokratPlatonov"),
                ("PartyPositionIdArpaEfirova", "КПМК", "Координатор на политики за медии и комуникации - координира и се грижи за партийните политики в областта на медиите и комуникациите, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdArpaEfirova"),
                ("PartyPositionIdNikolaDaskalov", "КПО", "Координатор на политики за образование - координира и се грижи за партийните политики в областта на образованието, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdNikolaDaskalov"),
                ("PartyPositionIdMariaGospojina", "КПСВ", "Координатор на политики по семейните въпроси - координира и се грижи за партийните политики в областта на семейните въпроси и раждаемостта, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdMariaGospojina"),
                ("PartyPositionIdGabrielaFrankfurtska", "КПЕЕ", "Координатор на политики по еврочленство и евроитеграция - координира и се грижи за партийните политики в областта на еврочленството на България, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdGabrielaFrankfurtska"),
                ("PartyPositionIdSnaksMacNinsky", "КПВП", "Координатор на политики за външна политика - координира и се грижи за партийните политики в областта на външната политика на България, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdSnaksMacNinsky"),
                ("PartyPositionIdElenaRudolf", "КПТ", "Координатор на политики за транспорт - координира и се грижи за партийните политики в областта на транспорта и пътната инфраструктура, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdElenaRudolf"),
                ("PartyPositionIdIbrahimKoch", "КПСС", "Координатор на политики за селско стопанство - координира и се грижи за партийните политики в областта на селското стопанство, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdIbrahimKoch"),
                ("PartyPositionIdGoritsaDubova", "КПЕЕ", "Координатор на политики за енергетика и екология - координира и се грижи за партийните политики в областта на енергетиката и екологията, съгласно правата и задълженията дадени от Устава за партийната длъжност Координатор на политики (чл.33 - 45).", true, new DateTime(2019, 12, 1), "MemberIdGoritsaDubova")
            };

            foreach ((string Id, string Name, string Description, bool IsCurrent, DateTime StartDate, string MemberId) partyPosition in partyPositions)
            {
                PartyPosition dbPartyPosition = new PartyPosition
                {
                    Name = partyPosition.Name,
                    Description = partyPosition.Description,
                    IsCurrent = partyPosition.IsCurrent,                    
                    StartDate = partyPosition.StartDate,
                    MemberId = partyPosition.MemberId
                };
                dbPartyPosition.Id = partyPosition.Id;
                await dbContext.PartyPositions.AddAsync(dbPartyPosition);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}