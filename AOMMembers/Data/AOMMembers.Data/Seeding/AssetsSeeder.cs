using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class AssetsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Assets.Any())
            {
                return;
            }

            List<(string Id, string Name, string Description, decimal Worthiness, string MaterialStateId)> assets = new List<(string Id, string Name, string Description, decimal Worthiness, string MaterialStateId)>
            {
                ("AssetIdIvanIvanov1", "Апартамент", "Гарсониера в София, квартал Люлин 6.", 90000, "MaterialStateIdIvanIvanov"),
                ("AssetIdIvanIvanov2", "Кола", "Лек автомобил \"Renault 5\" от 1990 г.", 2000, "MaterialStateIdIvanIvanov"),
                ("AssetIdStoyanDuvarov1", "Къща с дворно място", "Двуетажна къща с дворно място в София, квартал Бояна.", 330000, "MaterialStateIdStoyanDuvarov"),
                ("AssetIdStoyanDuvarov2", "Кола", "Лек автомобил \"Volkswagen Passat\" от 2005 г.", 5000, "MaterialStateIdStoyanDuvarov"),
                ("AssetIdStoyanDuvarov3", "Срочен влог", "Едногодишен срочен влог в банка ДСК.", 10000, "MaterialStateIdStoyanDuvarov"),
                ("AssetIdBoykoArmeyski1", "Къща с дворно място", "Двуетажна къща с дворно място в София, квартал Симеоново.", 340000, "MaterialStateIdBoykoArmeyski"),
                ("AssetIdBoykoArmeyski2", "Кола", "Лек автомобил \"Audi A8\" от 2012 г.", 10000, "MaterialStateIdBoykoArmeyski"),
                ("AssetIdBoykoArmeyski3", "Срочен влог", "Тригодишен срочен влог в Пощенска банка (юридическо име \"Юробанк България\" АД).", 20000, "MaterialStateIdBoykoArmeyski"),
                ("AssetIdYustinianZakonov", "Апартамент", "Тристаен апартамент в София, квартал Западен парк.", 140000, "MaterialStateIdYustinianZakonov"),
                ("AssetIdSokratPlatonov", "Апартамент", "Тристаен апартамент в София, квартал Обеля.", 130000, "MaterialStateIdSokratPlatonov"),
                ("AssetIdArpaEfirova1", "Кола", "Лек автомобил \"Volkswagen Polo\" от 2009 г.", 4000, "MaterialStateIdArpaEfirova"),
                ("AssetIdArpaEfirova2", "Срочен влог", "Едногодишен срочен влог в банка ДСК.", 5000, "MaterialStateIdArpaEfirova"),
                ("AssetIdNikolaDaskalov", "Апартамент", "Двустаен апартамент в София, квартал Манастирски ливади.", 110000, "MaterialStateIdNikolaDaskalov"),
                ("AssetIdMariaGospojina1", "Апартамент", "Тристаен апартамент в София, квартал Гео Милев.", 200000, "MaterialStateIdMariaGospojina"),
                ("AssetIdMariaGospojina2", "Кола", "Лек автомобил \"Skoda Octavia\" от 2016 г.", 10000, "MaterialStateIdMariaGospojina"),
                ("AssetIdGabrielaFrankfurtska", "Срочен влог", "Едногодишен срочен влог в банка ДСК.", 10000, "MaterialStateIdGabrielaFrankfurtska"),
                ("AssetIdSnaksMacNinsky1", "Апартамент", "Тристаен апартамент в София, квартал Дианабад.", 170000, "MaterialStateIdSnaksMacNinsky"),
                ("AssetIdSnaksMacNinsky2", "Кола", "Лек автомобил \"Volkswagen Golf\" от 2006 г.", 4000, "MaterialStateIdSnaksMacNinsky"),
                ("AssetIdElenaRudolf", "Апартамент", "Тристаен апартамент в София, квартал Надежда 1.", 140000, "MaterialStateIdElenaRudolf"),
                ("AssetIdIbrahimKoch", "Апартамент", "Двустаен апартамент в София, квартал Красна поляна.", 130000, "MaterialStateIdIbrahimKoch"),
                ("AssetIdGoritsaDubova", "Апартамент", "Двустаен апартамент в София, квартал Люлин 1.", 120000, "MaterialStateIdGoritsaDubova")
            };

            foreach ((string Id, string Name, string Description, decimal Worthiness, string MaterialStateId) asset in assets)
            {
                Asset dbAsset = new Asset
                {
                    Name = asset.Name,
                    Description = asset.Description,
                    Worthiness = asset.Worthiness,                    
                    MaterialStateId = asset.MaterialStateId
                };
                dbAsset.Id = asset.Id;
                await dbContext.Assets.AddAsync(dbAsset);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}