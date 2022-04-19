using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Xunit;
using Moq;
using AOMMembers.Data;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Data.Repositories;
using AOMMembers.Services.Data.Services;
using AOMMembers.Web.ViewModels.Settings;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class SettingsServiceTests
    {
        private readonly IMapper mapper;

        public SettingsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            Mock<IDeletableEntityRepository<Setting>> moqSettingsRepository = new Mock<IDeletableEntityRepository<Setting>>();
            Mock<IDeletableEntityRepository<Citizen>> moqCitizensRepository = new Mock<IDeletableEntityRepository<Citizen>>();

            moqSettingsRepository.Setup(r => r.All()).Returns(new List<Setting>
                                                        {
                                                            new Setting() { CitizenId = "CitizenId" },
                                                            new Setting() { CitizenId = "CitizenId" },
                                                            new Setting() { CitizenId = "CitizenId" }
                                                        }.AsQueryable());
            SettingsService service = new SettingsService(this.mapper, moqSettingsRepository.Object, moqCitizensRepository.Object);
            Assert.Equal(3, service.GetCountFromMember("CitizenId"));
            moqSettingsRepository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Unique_Database_Name") // Give a Unique name to the DB
                .Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            dbContext.Settings.Add(new Setting() { CitizenId = "CitizenId" });
            dbContext.Settings.Add(new Setting() { CitizenId = "CitizenId" });
            dbContext.Settings.Add(new Setting() { CitizenId = "CitizenId" });
            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensrepository);
            int count = service.GetCountFromMember("CitizenId");
            Assert.Equal(3, count);
        }
    }
}