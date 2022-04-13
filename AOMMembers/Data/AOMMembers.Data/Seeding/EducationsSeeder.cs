using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class EducationsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Educations.Any())
            {
                return;
            }

            List<(string Id, string Description, string CitizenId)> educations = new List<(string Id, string Description, string CitizenId)>
            {
                ("EducationIdIvanIvanov", "Имам висше образование от Историческия (бакалавър и магистър) и Педагогическия (магистър) факултети на Софийски университет \"Св. Климент Охридски\".", "CitizenIdIvanIvanov"),
                ("EducationIdStoyanDuvarov", "Завършил съм специалност \"Гранична полиция\" в Академията на МВР-София.", "CitizenIdStoyanDuvarov"),
                ("EducationIdBoykoArmeyski", "Завършил съм Национален военен университет \"Васил Левски\" във Велико Търново.", "CitizenIdBoykoArmeyski"),
                ("EducationIdYustinianZakonov", "Имам висше образование от Юридическия факултет (магистър) на Софийски университет \"Св. Климент Охридски\".", "CitizenIdYustinianZakonov"),
                ("EducationIdSokratPlatonov", "Имам висше образование от Философския факултет (бакалавър и магистър) на Софийски университет \"Св. Климент Охридски\".", "CitizenIdSokratPlatonov"),
                ("EducationIdArpaEfirova", "Завършила съм Националната гимназия за древни езици и култури \"Св. Константин-Кирил Философ\", а сега уча история в Софийски университет \"Св. Климент Охридски\".", "CitizenIdArpaEfirova"),
                ("EducationIdNikolaDaskalov", "Имам висше образование от Педагогическия факултет (магистър) на Софийски университет \"Св. Климент Охридски\".", "CitizenIdNikolaDaskalov"),
                ("EducationIdMariaGospojina", "Имам висше образование от Педагогическия факултет (магистър) на Софийски университет \"Св. Климент Охридски\".", "CitizenIdMariaGospojina"),
                ("EducationIdGabrielaFrankfurtska", "Завършила съм Националната гимназия за древни езици и култури \"Св. Константин-Кирил Философ\", а сега уча история в Софийски университет \"Св. Климент Охридски\".", "CitizenIdGabrielaFrankfurtska"),
                ("EducationIdSnaksMacNinsky", "Имам висше образование от Юридическия факултет (магистър) на Софийски университет \"Св. Климент Охридски\".", "CitizenIdSnaksMacNinsky"),
                ("EducationIdElenaRudolf", "Имам висше образование от Историческия факултет (бакалавър и магистър) на Софийски университет \"Св. Климент Охридски\".", "CitizenIdElenaRudolf"),
                ("EducationIdIbrahimKoch", "Имам висше образование от Техническия университет в София.", "CitizenIdIbrahimKoch"),
                ("EducationIdGoritsaDubova", "Имам висше образование от Историческия факултет (бакалавър и магистър) на Софийски университет \"Св. Климент Охридски\".", "CitizenIdGoritsaDubova")
            };

            foreach ((string Id, string Description, string CitizenId) education in educations)
            {
                Education dbEducation = new Education
                {                    
                    Description = education.Description,                    
                    CitizenId = education.CitizenId
                };
                dbEducation.Id = education.Id;
                await dbContext.Educations.AddAsync(dbEducation);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}