using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AOMMembers.Data.Seeding
{
    public class ApplicationDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            ILogger logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(ApplicationDbContextSeeder));

            List<ISeeder> seeders = new List<ISeeder>
                          {
                              new RolesSeeder(),
                              new ApplicationUsersSeeder(),
                              new MembersSeeder(),
                              new CitizensSeeder(),
                              new PublicImagesSeeder(),
                              new MediaMaterialsSeeder(),
                              new RelationshipsSeeder(),
                              new PartyPositionsSeeder(),
                              new EducationsSeeder(),
                              new QualificationsSeeder(),
                              new CareersSeeder(),
                              new WorkPositionsSeeder(),
                              new MaterialStatesSeeder(),
                              new AssetsSeeder(),
                              new LawStatesSeeder(),
                              new LawProblemsSeeder(),
                              new WorldviewsSeeder(),
                              new InterestsSeeder(),
                              new PartyMembershipsSeeder(),
                              new SocietyHelpsSeeder(),
                              new SocietyActivitiesSeeder(),
                              new SettingsSeeder()
                          };

            foreach (ISeeder seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}