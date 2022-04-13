using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class CitizensSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Citizens.Any())
            {
                return;
            }

            List<(string Id, string FirstName, string SecondName, string LastName, string Gender, int Age, DateTime BirthDate, string MemberId)> citizens = new List<(string Id, string FirstName, string SecondName, string LastName, string Gender, int Age, DateTime BirthDate, string MemberId)>
            {
                ("CitizenIdIvanIvanov", "Иван", "Иванов", "Иванов", "мъж", 42, new DateTime(1980, 4, 7), "MemberIdIvanIvanov"),
                ("CitizenIdStoyanDuvarov", "Стоян", "Каменов", "Дуваров", "мъж", 43, new DateTime(1978, 11, 8), "MemberIdStoyanDuvarov"),
                ("CitizenIdBoykoArmeyski", "Бойко", "Войников", "Армейски", "мъж", 51, new DateTime(1970, 5, 6), "MemberIdBoykoArmeyski"),
                ("CitizenIdYustinianZakonov", "Юстиниан", "Управдов", "Законов", "мъж", 36, new DateTime(1985, 11, 14), "MemberIdYustinianZakonov"),
                ("CitizenIdSokratPlatonov", "Сократ", "Аристотелов", "Платонов", "мъж", 30, new DateTime(1991, 11, 1), "MemberIdSokratPlatonov"),
                ("CitizenIdArpaEfirova", "Арпа", "Меркуриева", "Ефирова", "жена", 21, new DateTime(2000, 5, 7), "MemberIdArpaEfirova"),
                ("CitizenIdNikolaDaskalov", "Никола", "Априлов", "Даскалов", "мъж", 61, new DateTime(1960, 5, 24), "MemberIdNikolaDaskalov"),
                ("CitizenIdMariaGospojina", "Мария", "Яковова", "Госпожина", "жена", 35, new DateTime(1986, 9, 8), "MemberIdMariaGospojina"),
                ("CitizenIdGabrielaFrankfurtska", "Габриела", "Шенгенова", "Франкфуртска", "жена", 20, new DateTime(2001, 5, 9), "MemberIdGabrielaFrankfurtska"),
                ("CitizenIdSnaksMacNinsky", "Снакс", "Аргиров", "Макнински", "мъж", 56, new DateTime(1966, 1, 1), "MemberIdSnaksMacNinsky"),
                ("CitizenIdElenaRudolf", "Елена", "Тодорова", "Рудолф", "жена", 26, new DateTime(1996, 4, 12), "MemberIdElenaRudolf"),
                ("CitizenIdIbrahimKoch", "Ибрахим", "Али", "Коч", "мъж", 41, new DateTime(1980, 6, 24), "MemberIdIbrahimKoch"),
                ("CitizenIdGoritsaDubova", "Горица", "Букова", "Дъбова", "жена", 31, new DateTime(1991, 3, 1), "MemberIdGoritsaDubova")
            };

            foreach ((string Id, string FirstName, string SecondName, string LastName, string Gender, int Age, DateTime BirthDate, string MemberId) citizen in citizens)
            {
                Citizen dbCitizen = new Citizen
                {
                    FirstName = citizen.FirstName,
                    SecondName = citizen.SecondName,
                    LastName = citizen.LastName,
                    Gender = citizen.Gender,
                    Age = citizen.Age,
                    BirthDate = citizen.BirthDate,
                    MemberId = citizen.MemberId                    
                };
                dbCitizen.Id = citizen.Id;
                await dbContext.Citizens.AddAsync(dbCitizen);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}