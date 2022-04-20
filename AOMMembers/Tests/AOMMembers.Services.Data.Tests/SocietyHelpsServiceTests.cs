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
using AOMMembers.Web.ViewModels.SocietyHelps;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class SocietyHelpsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestSocietyHelpId = "TestSocietyHelpId";
        private const string TestInexistantSocietyHelpId = "TestInexistantSocietyHelpId";
        private const string TestSameUserSocietyHelpId = "TestSameUserSocietyHelpId";
        private const string TestOtherSocietyHelpId = "TestOtherSocietyHelpId";
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

        public SocietyHelpsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelpInputModel inputModel = new SocietyHelpInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.SocietyHelps.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelpInputModel inputModel = new SocietyHelpInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            SocietyHelp societyHelp = dbContext.SocietyHelps.FirstOrDefault();

            Assert.NotNull(societyHelp);
            Assert.IsType<SocietyHelp>(societyHelp);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelpInputModel inputModel = new SocietyHelpInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            SocietyHelp societyHelp = dbContext.SocietyHelps.First();

            Assert.Equal(TestName, societyHelp.Name);
            Assert.Equal(TestDescription, societyHelp.Description);
            Assert.Equal(TestResult, societyHelp.Result);
            Assert.Equal(TestEventLink, societyHelp.EventLink);
            Assert.Equal(TestCitizenId, societyHelp.CitizenId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelpInputModel inputModel = new SocietyHelpInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            SocietyHelp societyHelp = dbContext.SocietyHelps.First();

            Assert.Equal(societyHelp.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpInputModel inputModel = new SocietyHelpInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(SocietyHelpCreateWithoutCitizenBadResult, badResult);
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
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelpInputModel inputModel = new SocietyHelpInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink
            };

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(SocietyHelpCreateWithoutCitizenBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestSocietyHelpId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestSocietyHelpId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantSocietyHelpId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            SocietyHelpViewModel viewModel = await service.GetViewModelByIdAsync(TestSocietyHelpId);

            Assert.NotNull(viewModel);
            Assert.IsType<SocietyHelpViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            SocietyHelpViewModel viewModel = await service.GetViewModelByIdAsync(TestSocietyHelpId);

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
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            SocietyHelpDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestSocietyHelpId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<SocietyHelpDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            SocietyHelpDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestSocietyHelpId);

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
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestSocietyHelpId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestSocietyHelpId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantSocietyHelpId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestSocietyHelpId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantSocietyHelpId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            SocietyHelpEditModel editModel = await service.GetEditModelByIdAsync(TestSocietyHelpId);

            Assert.NotNull(editModel);
            Assert.IsType<SocietyHelpEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            SocietyHelpEditModel editModel = await service.GetEditModelByIdAsync(TestSocietyHelpId);

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
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpEditModel editModel = new SocietyHelpEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Result = TestEditedResult,
                EventLink = TestEditedEventLink
            };

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestSocietyHelpId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpEditModel editModel = new SocietyHelpEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Result = TestEditedResult,
                EventLink = TestEditedEventLink
            };

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestSocietyHelpId, editModel);

            SocietyHelp editedSocietyHelp = dbContext.SocietyHelps.First();

            Assert.Equal(TestEditedName, editedSocietyHelp.Name);
            Assert.Equal(TestEditedDescription, editedSocietyHelp.Description);
            Assert.Equal(TestEditedResult, editedSocietyHelp.Result);
            Assert.Equal(TestEditedEventLink, editedSocietyHelp.EventLink);
            Assert.Equal(TestCitizenId, editedSocietyHelp.CitizenId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpEditModel editModel = new SocietyHelpEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Result = TestEditedResult,
                EventLink = TestEditedEventLink
            };

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestInexistantSocietyHelpId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpEditModel editModel = new SocietyHelpEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Result = TestEditedResult,
                EventLink = TestEditedEventLink
            };

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestSocietyHelpId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            SocietyHelpDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestSocietyHelpId);

            Assert.NotNull(deleteModel);
            Assert.IsType<SocietyHelpDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            SocietyHelpDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestSocietyHelpId);

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
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestSocietyHelpId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestSocietyHelpId);

            SocietyHelp deleted = dbContext.SocietyHelps.FirstOrDefault(sh => sh.IsDeleted == false);

            Assert.Null(deleted);

            SocietyHelp deletedSoft = dbContext.SocietyHelps.FirstOrDefault(sh => sh.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await societyHelpsRepository.AddAsync(societyHelp);
            await societyHelpsRepository.SaveChangesAsync();

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantSocietyHelpId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestSocietyHelpId);

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

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await dbContext.SocietyHelps.AddAsync(societyHelp);

            SocietyHelp sameUserSocietyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSocietyHelp.Id = TestSameUserSocietyHelpId;

            await dbContext.SocietyHelps.AddAsync(sameUserSocietyHelp);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            otherCitizen.Id = TestOtherCitizenId;
            await dbContext.Citizens.AddAsync(otherCitizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp otherSocietyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = otherCitizen,
                CitizenId = TestOtherCitizenId
            };
            otherSocietyHelp.Id = TestOtherSocietyHelpId;

            await dbContext.SocietyHelps.AddAsync(otherSocietyHelp);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
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

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await dbContext.SocietyHelps.AddAsync(societyHelp);

            SocietyHelp sameUserSocietyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSocietyHelp.Id = TestSameUserSocietyHelpId;

            await dbContext.SocietyHelps.AddAsync(sameUserSocietyHelp);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
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

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await dbContext.SocietyHelps.AddAsync(societyHelp);

            SocietyHelp sameUserSocietyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSocietyHelp.Id = TestSameUserSocietyHelpId;

            await dbContext.SocietyHelps.AddAsync(sameUserSocietyHelp);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            otherCitizen.Id = TestOtherCitizenId;
            await dbContext.Citizens.AddAsync(otherCitizen);
            await dbContext.SaveChangesAsync();

            SocietyHelp otherSocietyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = otherCitizen,
                CitizenId = TestOtherCitizenId
            };
            otherSocietyHelp.Id = TestOtherSocietyHelpId;

            await dbContext.SocietyHelps.AddAsync(otherSocietyHelp);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            IEnumerable<SocietyHelpViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (SocietyHelpViewModel viewModel in viewModels)
            {
                Assert.IsType<SocietyHelpViewModel>(viewModel);
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

            SocietyHelp societyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            societyHelp.Id = TestSocietyHelpId;

            await dbContext.SocietyHelps.AddAsync(societyHelp);

            SocietyHelp sameUserSocietyHelp = new SocietyHelp()
            {
                Name = TestName,
                Description = TestDescription,
                Result = TestResult,
                EventLink = TestEventLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserSocietyHelp.Id = TestSameUserSocietyHelpId;

            await dbContext.SocietyHelps.AddAsync(sameUserSocietyHelp);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            IEnumerable<SocietyHelpViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            SocietyHelpsService service = new SocietyHelpsService(this.mapper, societyHelpsRepository, citizensRepository);
            IEnumerable<SocietyHelpViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}