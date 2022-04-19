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
using AOMMembers.Web.ViewModels.LawStates;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class LawStatesServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestLawStateId = "TestLawStateId";
        private const string TestInexistantLawStateId = "TestInexistantLawStateId";
        private const string TestCondition = "TestCondition";
        private const string TestEditedCondition = "TestEditedCondition";
        private const string TestCitizenId = "TestCitizenId";

        private readonly IMapper mapper;

        public LawStatesServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task IsCreatedReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isCreated = service.IsCreated(TestUserId);

            Assert.True(isCreated);
        }

        [Fact]
        public async Task IsCreatedReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isCreated = service.IsCreated(TestInexistantUserId);

            Assert.False(isCreated);
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawStateInputModel inputModel = new LawStateInputModel()
            {
                Condition = TestCondition
            };

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.LawStates.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawStateInputModel inputModel = new LawStateInputModel()
            {
                Condition = TestCondition
            };

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            LawState lawState = dbContext.LawStates.FirstOrDefault();

            Assert.NotNull(lawState);
            Assert.IsType<LawState>(lawState);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawStateInputModel inputModel = new LawStateInputModel()
            {
                Condition = TestCondition
            };

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            LawState lawState = dbContext.LawStates.First();

            Assert.Equal(TestCondition, lawState.Condition);
            Assert.Equal(TestCitizenId, lawState.CitizenId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawStateInputModel inputModel = new LawStateInputModel()
            {
                Condition = TestCondition
            };

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            LawState lawState = dbContext.LawStates.First();

            Assert.Equal(lawState.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawStateInputModel inputModel = new LawStateInputModel()
            {
                Condition = TestCondition
            };

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(LawStateCreateWithoutCitizenBadResult, badResult);
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
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawStateInputModel inputModel = new LawStateInputModel()
            {
                Condition = TestCondition
            };

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(LawStateCreateWithoutCitizenBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawState lawState = new LawState()
            {
                Condition = TestCondition
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isAbsent = await service.IsAbsent(TestLawStateId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawState lawState = new LawState()
            {
                Condition = TestCondition
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantLawStateId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawState lawState = new LawState()
            {
                Condition = TestCondition
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            LawStateViewModel viewModel = await service.GetViewModelByIdAsync(TestLawStateId);

            Assert.NotNull(viewModel);
            Assert.IsType<LawStateViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawState lawState = new LawState()
            {
                Condition = TestCondition
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            LawStateViewModel viewModel = await service.GetViewModelByIdAsync(TestLawStateId);

            Assert.Equal(TestCondition, viewModel.Condition);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            LawStateDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestLawStateId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<LawStateDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            LawStateDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestLawStateId);

            Assert.Equal(TestCondition, detailsViewModel.Condition);
            Assert.Equal(TestCitizenId, detailsViewModel.CitizenId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isFromMember = await service.IsFromMember(TestLawStateId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantLawStateId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isFromMember = await service.IsFromMember(TestLawStateId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantLawStateId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            LawStateEditModel editModel = await service.GetEditModelByIdAsync(TestLawStateId);

            Assert.NotNull(editModel);
            Assert.IsType<LawStateEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            LawStateEditModel editModel = await service.GetEditModelByIdAsync(TestLawStateId);

            Assert.Equal(TestCondition, editModel.Condition);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStateEditModel editModel = new LawStateEditModel()
            {
                Condition = TestEditedCondition
            };

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isEdited = await service.EditAsync(TestLawStateId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStateEditModel editModel = new LawStateEditModel()
            {
                Condition = TestEditedCondition
            };

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isEdited = await service.EditAsync(TestLawStateId, editModel);

            LawState editedLawState = dbContext.LawStates.First();

            Assert.Equal(TestEditedCondition, editedLawState.Condition);
            Assert.Equal(TestCitizenId, editedLawState.CitizenId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStateEditModel editModel = new LawStateEditModel()
            {
                Condition = TestEditedCondition
            };

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isEdited = await service.EditAsync(TestInexistantLawStateId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawState lawState = new LawState()
            {
                Condition = TestCondition,               
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            LawStateDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestLawStateId);

            Assert.NotNull(deleteModel);
            Assert.IsType<LawStateDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            LawStateDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestLawStateId);

            Assert.Equal(TestCondition, deleteModel.Condition);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isDeleted = await service.DeleteAsync(TestLawStateId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isDeleted = await service.DeleteAsync(TestLawStateId);

            LawState deleted = dbContext.LawStates.FirstOrDefault(ls => ls.IsDeleted == false);

            Assert.Null(deleted);

            LawState deletedSoft = dbContext.LawStates.FirstOrDefault(ls => ls.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            LawState lawState = new LawState()
            {
                Condition = TestCondition,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            lawState.Id = TestLawStateId;

            await lawStatesRepository.AddAsync(lawState);
            await lawStatesRepository.SaveChangesAsync();

            LawStatesService service = new LawStatesService(this.mapper, lawStatesRepository, citizensRepository, lawProblemsRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantLawStateId);

            Assert.False(isDeleted);
        }
    }
}