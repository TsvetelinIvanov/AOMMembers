using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class MembersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Members.Any())
            {
                return;
            }

            List<(string Id, string FullName, string Email, string PhoneNumber, string PictureUrl, string ApplicationUserId)> members = new List<(string Id, string FullName, string Email, string PhoneNumber, string PictureUrl, string ApplicationUserId)>
            {
                ("MemberIdIvanIvanov", "Иван Иванов Иванов", "ivanivanov@aom.bg", "0000000001", "wwwroot/Images/images-personalPhoto.png","ApplicationUserIdIvanIvanov"),
                ("MemberIdStoyanDuvarov", "Стоян Каменов Дуваров", "stoyanduvarov@aom.bg", "0000000010", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdStoyanDuvarov"),
                ("MemberIdBoykoArmeyski", "Бойко Войников Армейски", "boykoarmeyski@aom.bg", "0000000011", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdBoykoArmeyski"),
                ("MemberIdYustinianZakonov", "Юстиниан Управдов Законов", "yustinianZakonov@aom.bg", "0000000100", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdYustinianZakonov"),
                ("MemberIdSokratPlatonov", "Сократ Аристотелов Платонов", "sokratplatonov@aom.bg", "0000000101", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdSokratPlatonov"),
                ("MemberIdArpaEfirova", "Арпа Меркуриева Ефирова", "arpaefirova@aom.bg", "0000000110", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdArpaEfirova"),
                ("MemberIdNikolaDaskalov", "Никола Априлов Даскалов", "nikoladaskalov@aom.bg", "0000000111", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdNikolaDaskalov"),
                ("MemberIdMariaGospojina", "Мария Яковова Госпожина", "mariagospojina@aom.bg", "0000001000", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdMariaGospojina"),
                ("MemberIdGabrielaFrankfurtska", "Габриела Шенгенова Франкфуртска", "gabrielafrankfurtska@aom.bg", "0000001001", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdGabrielaFrankfurtska"),
                ("MemberIdSnaksMacNinsky", "Снакс Аргиров Макнински", "snaksmacninsky@aom.bg", "0000001010", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdSnaksMacNinsky"),
                ("MemberIdElenaRudolf", "Елена Тодорова Рудолф", "elenarudolf@aom.bg", "0000001011", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdElenaRudolf"),
                ("MemberIdIbrahimKoch", "Ибрахим Али Коч", "ibrahimkoch@aom.bg", "0000001100", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdIbrahimKoch"),
                ("MemberIdGoritsaDubova", "Горица Букова Дъбова", "goritsadubova@aom.bg", "0000001101", "wwwroot/Images/images-personalPhoto.png", "ApplicationUserIdGoritsaDubova")
            };

            foreach ((string Id, string FullName, string Email, string PhoneNumber, string PictureUrl, string ApplicationUserId) member in members)
            {
                Member dbMember = new Member
                {
                    FullName = member.FullName,
                    Email = member.Email,
                    PhoneNumber = member.PhoneNumber,
                    PictureUrl = member.PictureUrl,
                    ApplicationUserId = member.ApplicationUserId
                };
                dbMember.Id = member.Id;
                await dbContext.Members.AddAsync(dbMember);                
            }

            await dbContext.SaveChangesAsync();
        }
    }
}