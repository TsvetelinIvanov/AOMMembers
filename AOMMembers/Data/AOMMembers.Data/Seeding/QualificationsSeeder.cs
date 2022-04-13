using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class QualificationsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Qualifications.Any())
            {
                return;
            }

            List<(string Id, string Name, string Description, DateTime StartDate, DateTime EndDate, string EducationId)> qualifications = new List<(string Id, string Name, string Description, DateTime StartDate, DateTime EndDate, string EducationId)>
            {
                ("QualificationIdIvanIvanov1", "История", "Бакалавърска специалност \"История\" от Историческия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2001, 10, 1), new DateTime(2005, 7, 11), "EducationIdIvanIvanov"),
                ("QualificationIdIvanIvanov2", "Балканска история", "Магистърска специалност \"Балканска история\" от Историческия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2005, 11, 1), new DateTime(2007, 7, 19), "EducationIdIvanIvanov"),
                ("QualificationIdIvanIvanov3", "Педагогика на девиантното поведение", "Магистърска специалност \"Педагогика на девиантното поведение\" от Педагогическия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2007, 11, 1), new DateTime(2009, 7, 17), "EducationIdIvanIvanov"),
                ("QualificationIdStoyanDuvarov", "Гранична полиция", "Магистърска специалност \"Гранична полиция\" в Академия на МВР-София", new DateTime(1999, 10, 1), new DateTime(2004, 7, 10), "EducationIdStoyanDuvarov"),
                ("QualificationIdBoykoArmeyski", "Стопанска логистика", "Магистърска специалност \"Стопанска логистика\" в Национален военен университет \"Васил Левски\" във Велико Търново", new DateTime(1988, 10, 1), new DateTime(1993, 7, 16), "EducationIdBoykoArmeyski"),
                ("QualificationIdYustinianZakonov", "Право", "Магистърска специалност \"Право\" от Юридическия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2002, 10, 1), new DateTime(2007, 7, 18), "EducationIdYustinianZakonov"),
                ("QualificationIdSokratPlatonov1", "Философия", "Бакалавърска специалност \"Философия\" от Философския факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2009, 10, 1), new DateTime(2013, 7, 11), "EducationIdSokratPlatonov"),
                ("QualificationIdSokratPlatonov2", "Културна антропология", "Магистърска специалност \"Културна антропология\" от Философския факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2013, 10, 1), new DateTime(2015, 7, 19), "EducationIdSokratPlatonov"),
                ("QualificationIdArpaEfirova", "Профил \"Хуманитарен\"", "Средно образование от Националната гимназия за древни езици и култури \"Св. Константин-Кирил Философ\".", new DateTime(2014, 9, 15), new DateTime(2019, 5, 24), "EducationIdArpaEfirova"),
                ("QualificationIdNikolaDaskalov", "Педагогика", "Магистърска специалност \"Педагогика\" от Педагогическия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(1980, 10, 1), new DateTime(1986, 7, 17), "EducationIdNikolaDaskalov"),                
                ("QualificationIdMariaGospojina1", "Педагогика", "Бакалавърска специалност \"Педагогика\" от Педагогическия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2002, 10, 1), new DateTime(2007, 7, 10), "EducationIdMariaGospojina"),
                ("QualificationIdMariaGospojina2", "Педагогика на девиантното поведение", "Магистърска специалност \"Педагогика на девиантното поведение\" от Педагогическия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2007, 11, 1), new DateTime(2009, 7, 17), "EducationIdMariaGospojina"),
                ("QualificationIdGabrielaFrankfurtska", "Профил \"Хуманитарен\"", "Средно образование от Националната гимназия за древни езици и култури \"Св. Константин-Кирил Философ\".", new DateTime(2015, 9, 15), new DateTime(2020, 5, 24), "EducationIdGabrielaFrankfurtska"),
                ("QualificationIdSnaksMacNinsky", "Международни отношения", "Магистърска специалност \"Международни отношения\" от Юридическия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(1986, 10, 1), new DateTime(1992, 7, 16), "EducationIdSnaksMacNinsky"),
                ("QualificationIdElenaRudolf1", "История", "Бакалавърска специалност \"История\" от Историческия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2015, 10, 1), new DateTime(2019, 7, 10), "EducationIdElenaRudolf"),
                ("QualificationIdElenaRudolf2", "Европейски Югоизток", "Магистърска специалност \"Европейски Югоизток\" от Историческия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2019, 11, 1), new DateTime(2021, 7, 18), "EducationIdElenaRudolf"),
                ("QualificationIdIbrahimKoch1", "Електротехника", "Бакалавърска специалност \"Електротехника\" от Електротехническия факултет на Техническия университет в София.", new DateTime(2000, 10, 1), new DateTime(2004, 7, 11), "EducationIdIbrahimKoch"),
                ("QualificationIdIbrahimKoch2", "Електротехника", "Магистърска специалност \"Електротехника\" от Електротехническия факултет на Техническия университет в София.", new DateTime(2004, 10, 1), new DateTime(2006, 7, 17), "EducationIdIbrahimKoch"),
                ("QualificationIdGoritsaDubova1", "История", "Бакалавърска специалност \"История\" от Историческия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2009, 10, 1), new DateTime(2014, 7, 9), "EducationIdGoritsaDubova"),
                ("QualificationIdGoritsaDubova2", "Античност и средновековие", "Магистърска специалност \"Античност и средновековие\" от Историческия факултет на Софийски университет \"Св. Климент Охридски\".", new DateTime(2014, 10, 1), new DateTime(2016, 7, 19), "EducationIdGoritsaDubova")
            };

            foreach ((string Id, string Name, string Description, DateTime StartDate, DateTime EndDate, string EducationId) qualification in qualifications)
            {
                Qualification dbQualification = new Qualification
                {
                    Name = qualification.Name,
                    Description = qualification.Description,
                    StartDate = qualification.StartDate,
                    EndDate = qualification.EndDate,
                    EducationId = qualification.EducationId
                };
                dbQualification.Id = qualification.Id;
                await dbContext.Qualifications.AddAsync(dbQualification);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}