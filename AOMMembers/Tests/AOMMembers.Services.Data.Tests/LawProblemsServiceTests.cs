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
using AOMMembers.Web.ViewModels.LawProblems;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class LawProblemsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestLawProblemId = "TestLawProblemId";
        private const string TestInexistantLawProblemId = "TestInexistantLawProblemId";
        private const string TestSameUserLawProblemId = "TestSameUserLawProblemId";
        private const string TestOtherLawProblemId = "TestOtherLawProblemId";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const string TestLawProblemLink = "TestLawProblemLink";
        private const string TestEditedLawProblemLink = "TestEditedLawProblemLink";
        private const string TestLawStateId = "TestLawStateId";
        private const string TestOtherLawStateId = "TestOtherLawStateId";

        private readonly IMapper mapper;

        public LawProblemsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblemInputModel inputModel = new LawProblemInputModel()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink
            };

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.LawProblems.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblemInputModel inputModel = new LawProblemInputModel()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink
            };

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            await service.CreateAsync(inputModel, TestUserId);

            LawProblem lawProblem = dbContext.LawProblems.FirstOrDefault();

            Assert.NotNull(lawProblem);
            Assert.IsType<LawProblem>(lawProblem);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblemInputModel inputModel = new LawProblemInputModel()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink
            };

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            await service.CreateAsync(inputModel, TestUserId);

            LawProblem lawProblem = dbContext.LawProblems.First();

            Assert.Equal(TestDescription, lawProblem.Description);
            Assert.Equal(TestLawProblemLink, lawProblem.LawProblemLink);
            Assert.Equal(TestLawStateId, lawProblem.LawStateId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblemInputModel inputModel = new LawProblemInputModel()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink
            };

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            LawProblem lawProblem = dbContext.LawProblems.First();

            Assert.Equal(lawProblem.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemInputModel inputModel = new LawProblemInputModel()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink
            };

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(LawProblemCreateWithoutLawStateBadResult, badResult);
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
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblemInputModel inputModel = new LawProblemInputModel()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink
            };

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(LawProblemCreateWithoutLawStateBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isAbsent = await service.IsAbsent(TestLawProblemId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isAbsent = await service.IsAbsent(TestLawProblemId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantLawProblemId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            LawProblemViewModel viewModel = await service.GetViewModelByIdAsync(TestLawProblemId);

            Assert.NotNull(viewModel);
            Assert.IsType<LawProblemViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            LawProblemViewModel viewModel = await service.GetViewModelByIdAsync(TestLawProblemId);

            Assert.Equal(TestDescription, viewModel.Description);
            Assert.Equal(TestLawProblemLink, viewModel.LawProblemLink);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            LawProblemDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestLawProblemId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<LawProblemDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            LawProblemDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestLawProblemId);

            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestLawProblemLink, detailsViewModel.LawProblemLink);
            Assert.Equal(TestLawStateId, detailsViewModel.LawStateId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isFromMember = await service.IsFromMember(TestLawProblemId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isFromMember = await service.IsFromMember(TestLawProblemId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantLawProblemId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isFromMember = await service.IsFromMember(TestLawProblemId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantLawProblemId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            LawProblemEditModel editModel = await service.GetEditModelByIdAsync(TestLawProblemId);

            Assert.NotNull(editModel);
            Assert.IsType<LawProblemEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            LawProblemEditModel editModel = await service.GetEditModelByIdAsync(TestLawProblemId);

            Assert.Equal(TestDescription, editModel.Description);
            Assert.Equal(TestLawProblemLink, editModel.LawProblemLink);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemEditModel editModel = new LawProblemEditModel()
            {
                Description = TestEditedDescription,
                LawProblemLink = TestEditedLawProblemLink
            };

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isEdited = await service.EditAsync(TestLawProblemId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemEditModel editModel = new LawProblemEditModel()
            {
                Description = TestEditedDescription,
                LawProblemLink = TestEditedLawProblemLink
            };

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isEdited = await service.EditAsync(TestLawProblemId, editModel);

            LawProblem editedLawProblem = dbContext.LawProblems.First();

            Assert.Equal(TestEditedDescription, editedLawProblem.Description);
            Assert.Equal(TestEditedLawProblemLink, editedLawProblem.LawProblemLink);
            Assert.Equal(TestLawStateId, editedLawProblem.LawStateId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemEditModel editModel = new LawProblemEditModel()
            {
                Description = TestEditedDescription,
                LawProblemLink = TestEditedLawProblemLink
            };

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isEdited = await service.EditAsync(TestInexistantLawProblemId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemEditModel editModel = new LawProblemEditModel()
            {
                Description = TestEditedDescription,
                LawProblemLink = TestEditedLawProblemLink
            };

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isEdited = await service.EditAsync(TestLawProblemId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            LawProblemDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestLawProblemId);

            Assert.NotNull(deleteModel);
            Assert.IsType<LawProblemDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            LawProblemDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestLawProblemId);

            Assert.Equal(TestDescription, deleteModel.Description);
            Assert.Equal(TestLawProblemLink, deleteModel.LawProblemLink);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isDeleted = await service.DeleteAsync(TestLawProblemId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isDeleted = await service.DeleteAsync(TestLawProblemId);

            LawProblem deleted = dbContext.LawProblems.FirstOrDefault(lp => lp.IsDeleted == false);

            Assert.Null(deleted);

            LawProblem deletedSoft = dbContext.LawProblems.FirstOrDefault(lp => lp.IsDeleted == true);
            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await lawProblemsRepository.AddAsync(lawProblem);
            await lawProblemsRepository.SaveChangesAsync();

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantLawProblemId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            bool isDeleted = await service.DeleteAsync(TestLawProblemId);

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
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await dbContext.LawProblems.AddAsync(lawProblem);

            LawProblem sameUserLawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            sameUserLawProblem.Id = TestSameUserLawProblemId;

            await dbContext.LawProblems.AddAsync(sameUserLawProblem);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            await dbContext.Citizens.AddAsync(otherCitizen);
            LawState otherLawState = new LawState() { Citizen = otherCitizen };
            otherLawState.Id = TestOtherLawStateId;
            await dbContext.LawStates.AddAsync(otherLawState);
            await dbContext.SaveChangesAsync();

            LawProblem otherLawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = otherLawState,
                LawStateId = TestOtherLawStateId
            };
            otherLawProblem.Id = TestOtherLawProblemId;

            await dbContext.LawProblems.AddAsync(otherLawProblem);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
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
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await dbContext.LawProblems.AddAsync(lawProblem);

            LawProblem sameUserLawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            sameUserLawProblem.Id = TestSameUserLawProblemId;

            await dbContext.LawProblems.AddAsync(sameUserLawProblem);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
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
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await dbContext.LawProblems.AddAsync(lawProblem);

            LawProblem sameUserLawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            sameUserLawProblem.Id = TestSameUserLawProblemId;

            await dbContext.LawProblems.AddAsync(sameUserLawProblem);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            await dbContext.Citizens.AddAsync(otherCitizen);
            LawState otherLawState = new LawState() { Citizen = otherCitizen };
            otherLawState.Id = TestOtherLawStateId;
            await dbContext.LawStates.AddAsync(otherLawState);
            await dbContext.SaveChangesAsync();

            LawProblem otherLawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = otherLawState,
                LawStateId = TestOtherLawStateId
            };
            otherLawProblem.Id = TestOtherLawProblemId;

            await dbContext.LawProblems.AddAsync(otherLawProblem);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            IEnumerable<LawProblemViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (LawProblemViewModel viewModel in viewModels)
            {
                Assert.IsType<LawProblemViewModel>(viewModel);
                Assert.Equal(TestDescription, viewModel.Description);
                Assert.Equal(TestLawProblemLink, viewModel.LawProblemLink);
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
            await dbContext.Citizens.AddAsync(citizen);
            LawState lawState = new LawState() { Citizen = citizen };
            lawState.Id = TestLawStateId;
            await dbContext.LawStates.AddAsync(lawState);
            await dbContext.SaveChangesAsync();

            LawProblem lawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            lawProblem.Id = TestLawProblemId;

            await dbContext.LawProblems.AddAsync(lawProblem);

            LawProblem sameUserLawProblem = new LawProblem()
            {
                Description = TestDescription,
                LawProblemLink = TestLawProblemLink,
                LawState = lawState,
                LawStateId = TestLawStateId
            };
            sameUserLawProblem.Id = TestSameUserLawProblemId;

            await dbContext.LawProblems.AddAsync(sameUserLawProblem);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            IEnumerable<LawProblemViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);

            LawProblemsService service = new LawProblemsService(this.mapper, lawProblemsRepository, lawStatesRepository);
            IEnumerable<LawProblemViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}