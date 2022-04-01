using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using AOMMembers.Data;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Data.Repositories;
using AOMMembers.Services.Data.Services;

namespace AOMMembers.Services.Data.Tests
{
    public class SettingsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            Mock<IDeletableEntityRepository<Setting>> moqRepository = new Mock<IDeletableEntityRepository<Setting>>();            

            moqRepository.Setup(r => r.All()).Returns(new List<Setting>
                                                        {
                                                            new Setting() { CitizenId = "CitizenId" },
                                                            new Setting() { CitizenId = "CitizenId" },
                                                            new Setting() { CitizenId = "CitizenId" }
                                                        }.AsQueryable());
            SettingsService service = new SettingsService(null, moqRepository.Object, null);
            Assert.Equal(3, service.GetCountFromMember("CitizenId"));
            moqRepository.Verify(x => x.All(), Times.Once);
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

            EfDeletableEntityRepository<Setting> repository = new EfDeletableEntityRepository<Setting>(dbContext);
            SettingsService service = new SettingsService(null, repository, null);
            int count = service.GetCountFromMember("CitizenId");
            Assert.Equal(3, count);
        }
    }
}