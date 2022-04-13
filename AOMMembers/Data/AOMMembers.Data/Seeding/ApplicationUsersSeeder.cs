using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using AOMMembers.Common;
using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class ApplicationUsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {            
            if (dbContext.Users.Any())
            {
                return;
            }

            List<(string Id, string Email)> users = new List<(string Id, string Email)>
            {
                ("ApplicationUserIdIvanIvanov", "ivanivanov@aom.bg"),
                ("ApplicationUserIdStoyanDuvarov", "stoyanduvarov@aom.bg"),
                ("ApplicationUserIdBoykoArmeyski", "boykoarmeyski@aom.bg"),
                ("ApplicationUserIdYustinianZakonov", "yustinianZakonov@aom.bg"),
                ("ApplicationUserIdSokratPlatonov", "sokratplatonov@aom.bg"),
                ("ApplicationUserIdArpaEfirova", "arpaefirova@aom.bg"),
                ("ApplicationUserIdNikolaDaskalov", "nikoladaskalov@aom.bg"),
                ("ApplicationUserIdMariaGospojina", "mariagospojina@aom.bg"),
                ("ApplicationUserIdGabrielaFrankfurtska", "gabrielafrankfurtska@aom.bg"),
                ("ApplicationUserIdSnaksMacNinsky", "snaksmacninsky@aom.bg"),
                ("ApplicationUserIdElenaRudolf", "elenarudolf@aom.bg"),
                ("ApplicationUserIdIbrahimKoch", "ibrahimkoch@aom.bg"),
                ("ApplicationUserIdGoritsaDubova", "goritsadubova@aom.bg")                
            };

            string password = "aom123456";
            foreach ((string Id, string Email) user in users)
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    Email = user.Email,
                    EmailConfirmed = true,
                    SecurityStamp = "SecurityStamp01"
                };
                applicationUser.Id = user.Id;

                UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                IdentityResult result = await userManager.CreateAsync(applicationUser, password);
                if (result.Succeeded)
                {
                    if (applicationUser.Id == "ApplicationUserIdIvanIvanov")
                    {
                        await userManager.AddToRoleAsync(applicationUser, GlobalConstants.AdministratorRoleName);
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(applicationUser, GlobalConstants.MemberRoleName);
                    }                    
                }                
            }
        }
    }
}