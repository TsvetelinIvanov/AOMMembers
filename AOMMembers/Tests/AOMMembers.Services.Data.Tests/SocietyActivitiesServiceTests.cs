using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Xunit;
using Moq;
using AOMMembers.Data;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Data.Repositories;
using AOMMembers.Services.Data.Services;
using AOMMembers.Web.ViewModels.SocietyActivities;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class SocietyActivitiesServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestSocietyActivityId = "TestSocietyActivityId";
        private const string TestInexistantSocietyActivityId = "TestInexistantSocietyActivityId";
        private const string TestSameUserSocietyActivityId = "TestSameUserSocietyActivityId";
        private const string TestOtherSocietyActivityId = "TestOtherSocietyActivityId";
        private const string TestName = "TestName";
        private const string TestEditedName = "TestEditedName";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const string TestResult = "TestResult";
        private const string TestEditedResult = "TestEditedResult";
        private const string TestEventLink = "TestEventLink";
        private const string TestEditedEventLink = "TestEditedEventLink";
        private const string TestCitizenId = "TestCitizenId";
        private const string TestOtherCitizenId = "TestOtherCitizenId";

        private readonly IMapper mapper;

        public SocietyActivitiesServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivityInputModel inputModel = new SocietyActivityInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.SocietyActivities.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivityInputModel inputModel = new SocietyActivityInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            SocietyActivity societyActivity = dbContext.SocietyActivities.FirstOrDefault();

            Assert.NotNull(societyActivity);
            Assert.IsType<SocietyActivity>(societyActivity);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivityInputModel inputModel = new SocietyActivityInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            SocietyActivity societyActivity = dbContext.SocietyActivities.First();

            Assert.Equal(TestName, societyActivity.Name);
            Assert.Equal(TestDescription, societyActivity.Description);
            Assert.Equal(TestResult, societyActivity.Result);
            Assert.Equal(TestEventLink, societyActivity.EventLink);
            Assert.Equal(TestCitizenId, societyActivity.CitizenId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivityInputModel inputModel = new SocietyActivityInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            SocietyActivity societyActivity = dbContext.SocietyActivities.First();

            Assert.Equal(societyActivity.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivityInputModel inputModel = new SocietyActivityInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(SocietyActivityCreateWithoutCitizenBadResult, badResult);
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
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivityInputModel inputModel = new SocietyActivityInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(SocietyActivityCreateWithoutCitizenBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestSocietyActivityId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestSocietyActivityId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantSocietyActivityId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            SocietyActivityViewModel viewModel = await service.GetViewModelByIdAsync(TestSocietyActivityId);

            Assert.NotNull(viewModel);
            Assert.IsType<SocietyActivityViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            SocietyActivityViewModel viewModel = await service.GetViewModelByIdAsync(TestSocietyActivityId);

            Assert.Equal(TestName, viewModel.Name);
            Assert.Equal(TestDescription, viewModel.Description);
            Assert.Equal(TestResult, viewModel.Result);
            Assert.Equal(TestEventLink, viewModel.EventLink);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            SocietyActivityDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestSocietyActivityId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<SocietyActivityDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            SocietyActivityDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestSocietyActivityId);

            Assert.Equal(TestName, detailsViewModel.Name);
            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestResult, detailsViewModel.Result);
            Assert.Equal(TestEventLink, detailsViewModel.EventLink);
            Assert.Equal(TestCitizenId, detailsViewModel.CitizenId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestSocietyActivityId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestSocietyActivityId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantSocietyActivityId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestSocietyActivityId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantSocietyActivityId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            SocietyActivityEditModel editModel = await service.GetEditModelByIdAsync(TestSocietyActivityId);

            Assert.NotNull(editModel);
            Assert.IsType<SocietyActivityEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            SocietyActivityEditModel editModel = await service.GetEditModelByIdAsync(TestSocietyActivityId);

            Assert.Equal(TestName, editModel.Name);
            Assert.Equal(TestDescription, editModel.Description);
            Assert.Equal(TestResult, editModel.Result);
            Assert.Equal(TestEventLink, editModel.EventLink);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivityEditModel editModel = new SocietyActivityEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Result = TestEditedResult,
                EventLink = TestEditedEventLink
            };

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestSocietyActivityId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivityEditModel editModel = new SocietyActivityEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Result = TestEditedResult,
                EventLink = TestEditedEventLink
            };

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestSocietyActivityId, editModel);

            SocietyActivity editedSocietyActivity = dbContext.SocietyActivities.First();

            Assert.Equal(TestEditedName, editedSocietyActivity.Name);
            Assert.Equal(TestEditedDescription, editedSocietyActivity.Description);
            Assert.Equal(TestEditedResult, editedSocietyActivity.Result);
            Assert.Equal(TestEditedEventLink, editedSocietyActivity.EventLink);
            Assert.Equal(TestCitizenId, editedSocietyActivity.CitizenId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivityEditModel editModel = new SocietyActivityEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Result = TestEditedResult,
                EventLink = TestEditedEventLink
            };

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestInexistantSocietyActivityId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivityEditModel editModel = new SocietyActivityEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Result = TestEditedResult,
                EventLink = TestEditedEventLink
            };

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestSocietyActivityId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            SocietyActivityDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestSocietyActivityId);

            Assert.NotNull(deleteModel);
            Assert.IsType<SocietyActivityDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            SocietyActivityDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestSocietyActivityId);

            Assert.Equal(TestName, deleteModel.Name);
            Assert.Equal(TestDescription, deleteModel.Description);
            Assert.Equal(TestResult, deleteModel.Result);
            Assert.Equal(TestEventLink, deleteModel.EventLink);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestSocietyActivityId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestSocietyActivityId);

            SocietyActivity deleted = dbContext.SocietyActivities.FirstOrDefault(sa => sa.IsDeleted == false);

            Assert.Null(deleted);

            SocietyActivity deletedSoft = dbContext.SocietyActivities.FirstOrDefault(sa => sa.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await societyActivitiesRepository.AddAsync(societyActivity);
            await societyActivitiesRepository.SaveChangesAsync();

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantSocietyActivityId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestSocietyActivityId);

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

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await dbContext.SocietyActivities.AddAsync(societyActivity);

            SocietyActivity sameUserSocietyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSocietyActivity.Id = TestSameUserSocietyActivityId;

            await dbContext.SocietyActivities.AddAsync(sameUserSocietyActivity);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            otherCitizen.Id = TestOtherCitizenId;
            await dbContext.Citizens.AddAsync(otherCitizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity otherSocietyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = otherCitizen,
                CitizenId = TestOtherCitizenId
            };
            otherSocietyActivity.Id = TestOtherSocietyActivityId;

            await dbContext.SocietyActivities.AddAsync(otherSocietyActivity);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
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

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await dbContext.SocietyActivities.AddAsync(societyActivity);

            SocietyActivity sameUserSocietyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSocietyActivity.Id = TestSameUserSocietyActivityId;

            await dbContext.SocietyActivities.AddAsync(sameUserSocietyActivity);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
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

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await dbContext.SocietyActivities.AddAsync(societyActivity);

            SocietyActivity sameUserSocietyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSocietyActivity.Id = TestSameUserSocietyActivityId;

            await dbContext.SocietyActivities.AddAsync(sameUserSocietyActivity);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            otherCitizen.Id = TestOtherCitizenId;
            await dbContext.Citizens.AddAsync(otherCitizen);
            await dbContext.SaveChangesAsync();

            SocietyActivity otherSocietyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = otherCitizen,
                CitizenId = TestOtherCitizenId
            };
            otherSocietyActivity.Id = TestOtherSocietyActivityId;

            await dbContext.SocietyActivities.AddAsync(otherSocietyActivity);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            IEnumerable<SocietyActivityViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (SocietyActivityViewModel viewModel in viewModels)
            {
                Assert.IsType<SocietyActivityViewModel>(viewModel);
                Assert.Equal(TestName, viewModel.Name);
                Assert.Equal(TestDescription, viewModel.Description);
                Assert.Equal(TestResult, viewModel.Result);
                Assert.Equal(TestEventLink, viewModel.EventLink);
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

            SocietyActivity societyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyActivity.Id = TestSocietyActivityId;

            await dbContext.SocietyActivities.AddAsync(societyActivity);

            SocietyActivity sameUserSocietyActivity = new SocietyActivity()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSocietyActivity.Id = TestSameUserSocietyActivityId;

            await dbContext.SocietyActivities.AddAsync(sameUserSocietyActivity);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            IEnumerable<SocietyActivityViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyActivitiesService service = new SocietyActivitiesService(this.mapper, societyActivitiesRepository, citizensRepository);
            IEnumerable<SocietyActivityViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}