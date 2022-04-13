using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class InterestsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Interests.Any())
            {
                return;
            }

            List<(string Id, string Description, string WorldviewId)> interests = new List<(string Id, string Description, string WorldviewId)>
            {
                ("InterestIdIvanIvanov", "Обичам да се виждам с приятели.", "WorldviewIdIvanIvanov"),
                ("InterestIdStoyanDuvarov", "Обичам да се виждам с приятели.", "WorldviewIdStoyanDuvarov"),
                ("InterestIdBoykoArmeyski", "Обичам да се виждам с приятели.", "WorldviewIdBoykoArmeyski"),
                ("InterestIdYustinianZakonov", "Обичам да се виждам с приятели.", "WorldviewIdYustinianZakonov"),
                ("InterestIdSokratPlatonov", "Обичам да се виждам с приятели.", "WorldviewIdSokratPlatonov"),
                ("InterestIdArpaEfirova", "Обичам да се виждам с приятели.", "WorldviewIdArpaEfirova"),
                ("InterestIdNikolaDaskalov", "Обичам да се виждам с приятели.", "WorldviewIdNikolaDaskalov"),
                ("InterestIdMariaGospojina", "Обичам да се виждам с приятели.", "WorldviewIdMariaGospojina"),
                ("InterestIdGabrielaFrankfurtska", "Обичам да се виждам с приятели.", "WorldviewIdGabrielaFrankfurtska"),
                ("InterestIdSnaksMacNinsky", "Обичам да се виждам с приятели.", "WorldviewIdSnaksMacNinsky"),
                ("InterestIdElenaRudolf", "Обичам да се виждам с приятели.", "WorldviewIdElenaRudolf"),
                ("InterestIdIbrahimKoch", "Обичам да се виждам с приятели.", "WorldviewIdIbrahimKoch"),
                ("InterestIdGoritsaDubova", "Обичам да се виждам с приятели.", "WorldviewIdGoritsaDubova")
            };

            foreach ((string Id, string Description, string WorldviewId) interest in interests)
            {
                Interest dbInterest = new Interest
                {
                    Description = interest.Description,
                    WorldviewId = interest.WorldviewId
                };
                dbInterest.Id = interest.Id;
                await dbContext.Interests.AddAsync(dbInterest);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}