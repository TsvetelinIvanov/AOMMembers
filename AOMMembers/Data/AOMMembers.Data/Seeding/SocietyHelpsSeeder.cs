﻿using AOMMembers.Data.Models;

namespace AOMMembers.Data.Seeding
{
    public class SocietyHelpsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SocietyHelps.Any())
            {
                return;
            }

            List<(string Id, string Name, string Description, string Result, string CitizenId)> societyHelps = new List<(string Id, string Name, string Description, string Result, string CitizenId)>
            {
                ("SocietyHelpIdIvanIvanov", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdIvanIvanov"),
                ("SocietyHelpIdStoyanDuvarov", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdStoyanDuvarov"),
                ("SocietyHelpIdBoykoArmeyski", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdBoykoArmeyski"),
                ("SocietyHelpIdYustinianZakonov", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdYustinianZakonov"),
                ("SocietyHelpIdSokratPlatonov", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdSokratPlatonov"),
                ("SocietyHelpIdArpaEfirova", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdArpaEfirova"),
                ("SocietyHelpIdNikolaDaskalov", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdNikolaDaskalov"),
                ("SocietyHelpIdMariaGospojina", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdMariaGospojina"),
                ("SocietyHelpIdGabrielaFrankfurtska", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdGabrielaFrankfurtska"),
                ("SocietyHelpIdSnaksMacNinsky", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdSnaksMacNinsky"),
                ("SocietyHelpIdElenaRudolf", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdElenaRudolf"),
                ("SocietyHelpIdIbrahimKoch", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdIbrahimKoch"),
                ("SocietyHelpIdGoritsaDubova", "Базар", "Организиране и провеждане на Благотворителен коледен базар на 21.XII.2021 г. в имот на координатора на политики по национална сигурност и отбрана на АОМ Бойко Армейски. Събитието мина спокойно и без произшествия. Събрани бяха 59 467 лева, които бяха предоставени на нуждаещи се многодетни семейства.", "59 467 лева раздадени на нуждаещи се семейства", "CitizenIdGoritsaDubova")
            };

            foreach ((string Id, string Name, string Description, string Result, string CitizenId) societyHelp in societyHelps)
            {
                SocietyHelp dbSocietyHelp = new SocietyHelp
                {
                    Name = societyHelp.Name,
                    Description = societyHelp.Description,
                    Result = societyHelp.Result,                    
                    CitizenId = societyHelp.CitizenId
                };
                dbSocietyHelp.Id = societyHelp.Id;
                await dbContext.SocietyHelps.AddAsync(dbSocietyHelp);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}