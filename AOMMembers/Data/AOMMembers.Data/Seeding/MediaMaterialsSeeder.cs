using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class MediaMaterialsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.MediaMaterials.Any())
            {
                return;
            }

            List<(string Id, string Kind, string MediaName, DateTime IssueDate, string Author, string Heading, string Description, string MediaMaterialLink, string PublicImageId)> mediaMaterials = new List<(string Id, string Kind, string MediaName, DateTime IssueDate, string Author, string Heading, string Description, string MediaMaterialLink, string PublicImageId)>
            {
                ("MediaMaterialIdArpaEfirova", "Електронна статия", "Раздел \"Новини\" от сайта на АОМ", new DateTime(2019, 12, 9), "Арпа Ефирова", "Гледаемост и доверие към масмедиите в България", "Българите гледат обществените медии, но не им вярват, те не вярват на тези медии, но ги смятат за важни за демокрацията и се информират от тях ежедневно.", "https://tsvetelinivanov.github.io/CSS-Advanced-AOM/news/media.html", "PublicImageIdArpaEfirova"),
                ("MediaMaterialIdMariaGospojina1", "Електронна статия", "Раздел \"Новини\" от сайта на АОМ", new DateTime(2019, 12, 8), "Мария Госпожина", "Демографската криза в България в края на ХХ и началото на ХХI век", "Отделът за населението към ООН излезе с доклад, според който през 2050 г. населението в България ще бъде с 27,9 % по-малко от това през 2015 г.", "https://tsvetelinivanov.github.io/CSS-Advanced-AOM/news/demographic-collapse.html", "PublicImageIdMariaGospojina"),
                ("MediaMaterialIdMariaGospojina2", "Електронна статия", "Раздел \"Новини\" от сайта на АОМ", new DateTime(2019, 12, 9), "Мария Госпожина", "Демографската ситуация в България в началото на третото хилядолетие", "Демографската ситуация в България е обект на много проучвания и е определяна както като криза, така и като трансформация, дори и в селата.", "https://tsvetelinivanov.github.io/CSS-Advanced-AOM/news/demographic-situation.html", "PublicImageIdMariaGospojina"),
                ("MediaMaterialIdMariaGospojina3", "Електронна статия", "Раздел \"Новини\" от сайта на АОМ", new DateTime(2019, 12, 10), "Мария Госпожина", "Възможности за промяна на демографската ситуация в България", "Демографската ситуация в България въобще не е радостна и спешното подпомагане на семейството е едно от важните неща които трябва да бъдат направени за обръщане на негативната тенденция.", "https://tsvetelinivanov.github.io/CSS-Advanced-AOM/news/family-strategy.html", "PublicImageIdMariaGospojina"),
                ("MediaMaterialIdGabrielaFrankfurtska", "Електронна статия", "Раздел \"Новини\" от сайта на АОМ", new DateTime(2019, 12, 10), "Габриела Франкфуртска", "Новият бюджет на ЕС", "С приемането на бюджета за следващия програмен период, съответните органи на Европейския съюз, за пореден път насочиха огромни средства към нерентабилни, според нас, направления.", "https://tsvetelinivanov.github.io/CSS-Advanced-AOM/news/budget-eu.html", "PublicImageIdGabrielaFrankfurtska"),
                ("MediaMaterialIdSnaksMacNinsky", "Електронна статия", "Раздел \"Новини\" от сайта на АОМ", new DateTime(2019, 12, 8), "Снакс Макнински", "Възходът на страните от Югоизточна Азия", "Развитието на т.нар. \"бързо развиващи се\" или \"изгряващи\" (\"emerging\") страни в Югоизточна Азия показва, че в близко бъдеще от тях могат да дойдат голямо количество капитали.", "https://tsvetelinivanov.github.io/CSS-Advanced-AOM/news/emerging-countries.html", "PublicImageIdSnaksMacNinsky"),
                ("MediaMaterialIdIbrahimKoch", "Електронна статия", "Раздел \"Новини\" от сайта на АОМ", new DateTime(2019, 12, 8), "Ибрахим Али Коч", "Поредна криза в земеделието", "Очаква се кадрова криза в българското земеделие, като се прогнозира и намаляване на доходността в сектора за целия ЕС.", "https://tsvetelinivanov.github.io/CSS-Advanced-AOM/news/agriculture-problems.html", "PublicImageIdIbrahimKoch")
            };

            foreach ((string Id, string Kind, string MediaName, DateTime IssueDate, string Author, string Heading, string Description, string MediaMaterialLink, string PublicImageId) mediaMaterial in mediaMaterials)
            {
                MediaMaterial dbMediaMaterial = new MediaMaterial
                {
                    Kind = mediaMaterial.Kind,
                    MediaName = mediaMaterial.MediaName,
                    IssueDate = mediaMaterial.IssueDate,
                    Author = mediaMaterial.Author,
                    Heading = mediaMaterial.Heading,
                    Description = mediaMaterial.Description,
                    MediaMaterialLink = mediaMaterial.MediaMaterialLink,
                    PublicImageId = mediaMaterial.PublicImageId,

                };
                dbMediaMaterial.Id = mediaMaterial.Id;
                await dbContext.MediaMaterials.AddAsync(dbMediaMaterial);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}