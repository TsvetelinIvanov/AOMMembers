using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class PublicImagesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PublicImages.Any())
            {
                return;
            }

            List<(string Id, int Rating, string MemberId)> publicImages = new List<(string Id, int Rating, string MemberId)>
            {
                ("PublicImageIdIvanIvanov", 9, "MemberIdIvanIvanov"),
                ("PublicImageIdStoyanDuvarov", 8, "MemberIdStoyanDuvarov"),
                ("PublicImageIdBoykoArmeyski", 9, "MemberIdBoykoArmeyski"),
                ("PublicImageIdYustinianZakonov", 8, "MemberIdYustinianZakonov"),
                ("PublicImageIdSokratPlatonov", 8, "MemberIdSokratPlatonov"),
                ("PublicImageIdArpaEfirova", 9, "MemberIdArpaEfirova"),
                ("PublicImageIdNikolaDaskalov", 9, "MemberIdNikolaDaskalov"),
                ("PublicImageIdMariaGospojina", 9, "MemberIdMariaGospojina"),
                ("PublicImageIdGabrielaFrankfurtska", 8, "MemberIdGabrielaFrankfurtska"),
                ("PublicImageIdSnaksMacNinsky", 9, "MemberIdSnaksMacNinsky"),
                ("PublicImageIdElenaRudolf", 9, "MemberIdElenaRudolf"),
                ("PublicImageIdIbrahimKoch", 9, "MemberIdIbrahimKoch"),
                ("PublicImageIdGoritsaDubova", 9, "MemberIdGoritsaDubova")
            };

            foreach ((string Id, int Rating, string MemberId) publicImage in publicImages)
            {
                PublicImage dbPublicImage = new PublicImage
                {                    
                    Rating = publicImage.Rating,                    
                    MemberId = publicImage.MemberId
                };
                dbPublicImage.Id = publicImage.Id;
                await dbContext.PublicImages.AddAsync(dbPublicImage);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}