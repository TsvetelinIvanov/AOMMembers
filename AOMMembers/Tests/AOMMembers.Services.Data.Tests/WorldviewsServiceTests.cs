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
using AOMMembers.Web.ViewModels.Worldviews;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class WorldviewsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestWorldviewId = "TestWorldviewId";
        private const string TestInexistantWorldviewId = "TestInexistantWorldviewId";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const string TestIdeology = "TestIdeology";
        private const string TestEditedIdeology = "TestEditedIdeology";
        private const string TestCitizenId = "TestCitizenId";

        private readonly IMapper mapper;

        public WorldviewsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task IsCreatedReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isCreated = service.IsCreated(TestUserId);

            Assert.True(isCreated);
        }

        [Fact]
        public async Task IsCreatedReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isCreated = service.IsCreated(TestInexistantUserId);

            Assert.False(isCreated);
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            WorldviewInputModel inputModel = new WorldviewInputModel()
            {
                Description = TestDescription,
                Ideology = TestIdeology
            };

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.Worldviews.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            WorldviewInputModel inputModel = new WorldviewInputModel()
            {
                Description = TestDescription,
                Ideology = TestIdeology
            };

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Worldview worldview = dbContext.Worldviews.FirstOrDefault();

            Assert.NotNull(worldview);
            Assert.IsType<Worldview>(worldview);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            WorldviewInputModel inputModel = new WorldviewInputModel()
            {
                Description = TestDescription,
                Ideology = TestIdeology
            };

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Worldview worldview = dbContext.Worldviews.First();

            Assert.Equal(TestDescription, worldview.Description);
            Assert.Equal(TestIdeology, worldview.Ideology);
            Assert.Equal(TestCitizenId, worldview.CitizenId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            WorldviewInputModel inputModel = new WorldviewInputModel()
            {
                Description = TestDescription,
                Ideology = TestIdeology
            };

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            Worldview worldview = dbContext.Worldviews.First();

            Assert.Equal(worldview.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            WorldviewInputModel inputModel = new WorldviewInputModel()
            {
                Description = TestDescription,
                Ideology = TestIdeology
            };

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(WorldviewCreateWithoutCitizenBadResult, badResult);
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
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            WorldviewInputModel inputModel = new WorldviewInputModel()
            {
                Description = TestDescription,
                Ideology = TestIdeology
            };

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(WorldviewCreateWithoutCitizenBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,                
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isAbsent = await service.IsAbsent(TestWorldviewId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantWorldviewId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            WorldviewViewModel viewModel = await service.GetViewModelByIdAsync(TestWorldviewId);

            Assert.NotNull(viewModel);
            Assert.IsType<WorldviewViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            WorldviewViewModel viewModel = await service.GetViewModelByIdAsync(TestWorldviewId);

            Assert.Equal(TestDescription, viewModel.Description);
            Assert.Equal(TestIdeology, viewModel.Ideology);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            WorldviewDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestWorldviewId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<WorldviewDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            WorldviewDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestWorldviewId);

            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestIdeology, detailsViewModel.Ideology);
            Assert.Equal(TestCitizenId, detailsViewModel.CitizenId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isFromMember = await service.IsFromMember(TestWorldviewId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantWorldviewId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isFromMember = await service.IsFromMember(TestWorldviewId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantWorldviewId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            WorldviewEditModel editModel = await service.GetEditModelByIdAsync(TestWorldviewId);

            Assert.NotNull(editModel);
            Assert.IsType<WorldviewEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            WorldviewEditModel editModel = await service.GetEditModelByIdAsync(TestWorldviewId);

            Assert.Equal(TestDescription, editModel.Description);
            Assert.Equal(TestIdeology, editModel.Ideology);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewEditModel editModel = new WorldviewEditModel()
            {
                Description = TestEditedDescription,
                Ideology = TestEditedIdeology
            };

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isEdited = await service.EditAsync(TestWorldviewId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewEditModel editModel = new WorldviewEditModel()
            {
                Description = TestEditedDescription,
                Ideology = TestEditedIdeology
            };

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isEdited = await service.EditAsync(TestWorldviewId, editModel);

            Worldview editedWorldview = dbContext.Worldviews.First();

            Assert.Equal(TestEditedDescription, editedWorldview.Description);
            Assert.Equal(TestEditedIdeology, editedWorldview.Ideology);
            Assert.Equal(TestCitizenId, editedWorldview.CitizenId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewEditModel editModel = new WorldviewEditModel()
            {
                Description = TestEditedDescription,
                Ideology = TestEditedIdeology
            };

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isEdited = await service.EditAsync(TestInexistantWorldviewId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,                
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            WorldviewDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestWorldviewId);

            Assert.NotNull(deleteModel);
            Assert.IsType<WorldviewDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            WorldviewDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestWorldviewId);

            Assert.Equal(TestDescription, deleteModel.Description);
            Assert.Equal(TestIdeology, deleteModel.Ideology);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isDeleted = await service.DeleteAsync(TestWorldviewId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isDeleted = await service.DeleteAsync(TestWorldviewId);

            Worldview deleted = dbContext.Worldviews.FirstOrDefault(w => w.IsDeleted == false);

            Assert.Null(deleted);

            Worldview deletedSoft = dbContext.Worldviews.FirstOrDefault(w => w.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Worldview worldview = new Worldview()
            {
                Description = TestDescription,
                Ideology = TestIdeology,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            worldview.Id = TestWorldviewId;

            await worldviewsRepository.AddAsync(worldview);
            await worldviewsRepository.SaveChangesAsync();

            WorldviewsService service = new WorldviewsService(this.mapper, worldviewsRepository, citizensRepository, interestsRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantWorldviewId);

            Assert.False(isDeleted);
        }
    }
}