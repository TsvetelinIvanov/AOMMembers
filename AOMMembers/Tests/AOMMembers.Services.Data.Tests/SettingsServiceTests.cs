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
                                                            new Setting(),
                                                            new Setting(),
                                                            new Setting(),
                                                        }.AsQueryable());
            SettingsService service = new SettingsService(moqRepository.Object);
            Assert.Equal(3, service.GetCount());
            moqRepository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Find_User_Database") // Give a Unique name to the DB
                .Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            dbContext.Settings.Add(new Setting());
            dbContext.Settings.Add(new Setting());
            dbContext.Settings.Add(new Setting());
            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Setting> repository = new EfDeletableEntityRepository<Setting>(dbContext);
            SettingsService service = new SettingsService(repository);
            int count = service.GetCount();
            Assert.Equal(3, count);
        }
    }
}