using System;
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
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestSettingId = "TestSettingId";
        private const string TestInexistantSettingId = "TestInexistantSettingId";
        private const string TestSameUserSettingId = "TestSameUserSettingId";
        private const string TestOtherSettingId = "TestOtherSettingId";
        private const string TestName = "TestName";
        private const string TestEditedName = "TestEditedName";
        private const string TestValue = "TestValue";
        private const string TestEditedValue = "TestEditedValue";
        private const string TestCitizenId = "TestCitizenId";
        private const string TestOtherCitizenId = "TestOtherCitizenId";

        private readonly IMapper mapper;

        public SettingsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        //[Fact]
        //public void GetCountShouldReturnCorrectNumber()
        //{
        //    Mock<IDeletableEntityRepository<Setting>> moqSettingsRepository = new Mock<IDeletableEntityRepository<Setting>>();
        //    Mock<IDeletableEntityRepository<Citizen>> moqCitizensRepository = new Mock<IDeletableEntityRepository<Citizen>>();

        //    moqSettingsRepository.Setup(r => r.All()).Returns(new List<Setting>
        //                                                {
        //                                                    new Setting() { CitizenId = "CitizenId" },
        //                                                    new Setting() { CitizenId = "CitizenId" },
        //                                                    new Setting() { CitizenId = "CitizenId" }
        //                                                }.AsQueryable());
        //    SettingsService service = new SettingsService(this.mapper, moqSettingsRepository.Object, moqCitizensRepository.Object);
        //    Assert.Equal(3, service.GetCountFromMember("CitizenId"));
        //    moqSettingsRepository.Verify(x => x.All(), Times.Once);
        //}

        //[Fact]
        //public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        //{
        //    DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase("Unique_Database_Name") // Give a Unique name to the DB
        //        .Options;
        //    ApplicationDbContext dbContext = new ApplicationDbContext(options);
        //    dbContext.Settings.Add(new Setting() { CitizenId = "CitizenId" });
        //    dbContext.Settings.Add(new Setting() { CitizenId = "CitizenId" });
        //    dbContext.Settings.Add(new Setting() { CitizenId = "CitizenId" });
        //    await dbContext.SaveChangesAsync();

        //    EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
        //    EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);
        //    SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensrepository);
        //    int count = service.GetCountFromMember("CitizenId");
        //    Assert.Equal(3, count);
        //}

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SettingInputModel inputModel = new SettingInputModel()
            {
                Name = TestName,
                Value = TestValue
            };

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.Settings.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SettingInputModel inputModel = new SettingInputModel()
            {
                Name = TestName,
                Value = TestValue
            };

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Setting setting = dbContext.Settings.FirstOrDefault();

            Assert.NotNull(setting);
            Assert.IsType<Setting>(setting);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SettingInputModel inputModel = new SettingInputModel()
            {
                Name = TestName,
                Value = TestValue
            };

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Setting setting = dbContext.Settings.First();

            Assert.Equal(TestName, setting.Name);
            Assert.Equal(TestValue, setting.Value);
            Assert.Equal(TestCitizenId, setting.CitizenId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SettingInputModel inputModel = new SettingInputModel()
            {
                Name = TestName,
                Value = TestValue
            };

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            Setting setting = dbContext.Settings.First();

            Assert.Equal(setting.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingInputModel inputModel = new SettingInputModel()
            {
                Name = TestName,
                Value = TestValue
            };

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(SettingCreateWithoutCitizenBadResult, badResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(TestInexistantUserId)]
        public async Task CreateAsyncReturnsBadResultIfInexistantUserId(string inexistantUserId)
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SettingInputModel inputModel = new SettingInputModel()
            {
                Name = TestName,
                Value = TestValue
            };

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(SettingCreateWithoutCitizenBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestSettingId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestSettingId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantSettingId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            SettingViewModel viewModel = await service.GetViewModelByIdAsync(TestSettingId);

            Assert.NotNull(viewModel);
            Assert.IsType<SettingViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            SettingViewModel viewModel = await service.GetViewModelByIdAsync(TestSettingId);

            Assert.Equal(TestName, viewModel.Name);
            Assert.Equal(TestValue, viewModel.Value);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            SettingDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestSettingId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<SettingDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            SettingDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestSettingId);

            Assert.Equal(TestName, detailsViewModel.Name);
            Assert.Equal(TestValue, detailsViewModel.Value);
            Assert.Equal(TestCitizenId, detailsViewModel.CitizenId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestSettingId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestSettingId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantSettingId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestSettingId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantSettingId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            SettingEditModel editModel = await service.GetEditModelByIdAsync(TestSettingId);

            Assert.NotNull(editModel);
            Assert.IsType<SettingEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            SettingEditModel editModel = await service.GetEditModelByIdAsync(TestSettingId);

            Assert.Equal(TestName, editModel.Name);
            Assert.Equal(TestValue, editModel.Value);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingEditModel editModel = new SettingEditModel()
            {
                Name = TestEditedName,
                Value = TestEditedValue
            };

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestSettingId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingEditModel editModel = new SettingEditModel()
            {
                Name = TestEditedName,
                Value = TestEditedValue
            };

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestSettingId, editModel);

            Setting editedSetting = dbContext.Settings.First();

            Assert.Equal(TestEditedName, editedSetting.Name);
            Assert.Equal(TestEditedValue, editedSetting.Value);
            Assert.Equal(TestCitizenId, editedSetting.CitizenId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingEditModel editModel = new SettingEditModel()
            {
                Name = TestEditedName,
                Value = TestEditedValue
            };

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestInexistantSettingId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingEditModel editModel = new SettingEditModel()
            {
                Name = TestEditedName,
                Value = TestEditedValue
            };

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestSettingId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            SettingDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestSettingId);

            Assert.NotNull(deleteModel);
            Assert.IsType<SettingDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            SettingDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestSettingId);

            Assert.Equal(TestName, deleteModel.Name);
            Assert.Equal(TestValue, deleteModel.Value);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestSettingId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestSettingId);

            Setting deleted = dbContext.Settings.FirstOrDefault(s => s.IsDeleted == false);

            Assert.Null(deleted);

            Setting deletedSoft = dbContext.Settings.FirstOrDefault(s => s.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await settingsRepository.AddAsync(setting);
            await settingsRepository.SaveChangesAsync();

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantSettingId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestSettingId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task GetCountFromMemberReturnsCountCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await dbContext.Settings.AddAsync(setting);

            Setting sameUserSetting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSetting.Id = TestSameUserSettingId;

            await dbContext.Settings.AddAsync(sameUserSetting);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            otherCitizen.Id = TestOtherCitizenId;
            await dbContext.Citizens.AddAsync(otherCitizen);
            await dbContext.SaveChangesAsync();

            Setting otherSetting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = otherCitizen,
                CitizenId = TestOtherCitizenId
            };
            otherSetting.Id = TestOtherSettingId;

            await dbContext.Settings.AddAsync(otherSetting);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            int count = service.GetCountFromMember(TestUserId);

            Assert.Equal(2, count);
        }

        [Fact]
        public async Task GetCountFromMemberReturns0IfNoInCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await dbContext.Settings.AddAsync(setting);

            Setting sameUserSetting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSetting.Id = TestSameUserSettingId;

            await dbContext.Settings.AddAsync(sameUserSetting);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            int count = service.GetCountFromMember(TestUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task GetAllFromMemberReturnsViewModelsCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await dbContext.Settings.AddAsync(setting);

            Setting sameUserSetting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSetting.Id = TestSameUserSettingId;

            await dbContext.Settings.AddAsync(sameUserSetting);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            otherCitizen.Id = TestOtherCitizenId;
            await dbContext.Citizens.AddAsync(otherCitizen);
            await dbContext.SaveChangesAsync();

            Setting otherSetting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = otherCitizen,
                CitizenId = TestOtherCitizenId
            };
            otherSetting.Id = TestOtherSettingId;

            await dbContext.Settings.AddAsync(otherSetting);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            IEnumerable<SettingViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (SettingViewModel viewModel in viewModels)
            {
                Assert.IsType<SettingViewModel>(viewModel);
                Assert.Equal(TestName, viewModel.Name);
                Assert.Equal(TestValue, viewModel.Value);
            }
        }

        [Fact]
        public async Task GetCountFromMemberReturnsNullIfNoInCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Setting setting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            setting.Id = TestSettingId;

            await dbContext.Settings.AddAsync(setting);

            Setting sameUserSetting = new Setting()
            {
                Name = TestName,
                Value = TestValue,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSetting.Id = TestSameUserSettingId;

            await dbContext.Settings.AddAsync(sameUserSetting);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            IEnumerable<SettingViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SettingsService service = new SettingsService(this.mapper, settingsRepository, citizensRepository);
            IEnumerable<SettingViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}