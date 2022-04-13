using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class LawProblemsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.LawProblems.Any())
            {
                return;
            }

            List<(string Id, string Description, string LawStateId)> lawProblems = new List<(string Id, string Description, string LawStateId)>
            {
                ("LawProblemIdIvanIvanov", "Задържане за 24 часа в IV РПУ София без предявяване на обвинение на 19.IX.2009 г.", "LawStateIdIvanIvanov"),                
                ("LawProblemIdSnaksMacNinsky1", "Задържане за 24 часа в I РПУ София без предявяване на обвинение на 1.I.2000 г.", "LawStateIdSnaksMacNinsky"),
                ("LawProblemIdSnaksMacNinsky2", "Задържане за 24 часа в I РПУ София без предявяване на обвинение на 18.IX.2011 г.", "LawStateIdSnaksMacNinsky")
            };

            foreach ((string Id, string Description, string LawStateId) lawProblem in lawProblems)
            {
                LawProblem dbLawProblem = new LawProblem
                {
                    Description = lawProblem.Description,
                    LawStateId = lawProblem.LawStateId
                };
                dbLawProblem.Id = lawProblem.Id;
                await dbContext.LawProblems.AddAsync(dbLawProblem);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}