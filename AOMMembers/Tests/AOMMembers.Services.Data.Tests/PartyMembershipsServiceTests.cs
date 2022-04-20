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
using AOMMembers.Web.ViewModels.PartyMemberships;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class PartyMembershipsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestPartyMembershipId = "TestPartyMembershipId";
        private const string TestInexistantPartyMembershipId = "TestInexistantPartyMembershipId";
        private const string TestSameUserPartyMembershipId = "TestSameUserPartyMembershipId";
        private const string TestOtherPartyMembershipId = "TestOtherPartyMembershipId";
        private const string TestPartyName = "TestPartyName";
        private const string TestEditedPartyName = "TestEditedPartyName";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const bool TestIsCurrent = true;
        private const bool TestEditedIsCurrent = false;
        private readonly DateTime TestStartDate = new DateTime(2019, 12, 1);
        private readonly DateTime TestEditedStartDate = new DateTime(2020, 1, 1);
        private readonly DateTime TestEndDate = new DateTime(2068, 12, 31);
        private readonly DateTime TestEditedEndDate = new DateTime(2021, 1, 31);
        private const string TestCitizenId = "TestCitizenId";
        private const string TestOtherCitizenId = "TestOtherCitizenId";

        private readonly IMapper mapper;

        public PartyMembershipsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembershipInputModel inputModel = new PartyMembershipInputModel()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.PartyMemberships.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembershipInputModel inputModel = new PartyMembershipInputModel()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            PartyMembership partyMembership = dbContext.PartyMemberships.FirstOrDefault();

            Assert.NotNull(partyMembership);
            Assert.IsType<PartyMembership>(partyMembership);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembershipInputModel inputModel = new PartyMembershipInputModel()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            await service.CreateAsync(inputModel, TestUserId);

            PartyMembership partyMembership = dbContext.PartyMemberships.First();

            Assert.Equal(TestPartyName, partyMembership.PartyName);
            Assert.Equal(TestDescription, partyMembership.Description);
            Assert.Equal(TestIsCurrent, partyMembership.IsCurrent);
            Assert.Equal(TestStartDate, partyMembership.StartDate);
            Assert.Equal(TestEndDate, partyMembership.EndDate);
            Assert.Equal(TestCitizenId, partyMembership.CitizenId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembershipInputModel inputModel = new PartyMembershipInputModel()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            PartyMembership partyMembership = dbContext.PartyMemberships.First();

            Assert.Equal(partyMembership.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipInputModel inputModel = new PartyMembershipInputModel()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(PartyMembershipCreateWithoutCitizenBadResult, badResult);
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
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembershipInputModel inputModel = new PartyMembershipInputModel()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(PartyMembershipCreateWithoutCitizenBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestPartyMembershipId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestPartyMembershipId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantPartyMembershipId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            PartyMembershipViewModel viewModel = await service.GetViewModelByIdAsync(TestPartyMembershipId);

            Assert.NotNull(viewModel);
            Assert.IsType<PartyMembershipViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            PartyMembershipViewModel viewModel = await service.GetViewModelByIdAsync(TestPartyMembershipId);

            Assert.Equal(TestPartyName, viewModel.PartyName);
            Assert.Equal(TestDescription, viewModel.Description);
            Assert.Equal(TestIsCurrent, viewModel.IsCurrent);
            Assert.Equal(TestStartDate, viewModel.StartDate);
            Assert.Equal(TestEndDate, viewModel.EndDate);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            PartyMembershipDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestPartyMembershipId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<PartyMembershipDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            PartyMembershipDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestPartyMembershipId);

            Assert.Equal(TestPartyName, detailsViewModel.PartyName);
            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestIsCurrent, detailsViewModel.IsCurrent);
            Assert.Equal(TestStartDate, detailsViewModel.StartDate);
            Assert.Equal(TestEndDate, detailsViewModel.EndDate);
            Assert.Equal(TestCitizenId, detailsViewModel.CitizenId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestPartyMembershipId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestPartyMembershipId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantPartyMembershipId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestPartyMembershipId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantPartyMembershipId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            PartyMembershipEditModel editModel = await service.GetEditModelByIdAsync(TestPartyMembershipId);

            Assert.NotNull(editModel);
            Assert.IsType<PartyMembershipEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            PartyMembershipEditModel editModel = await service.GetEditModelByIdAsync(TestPartyMembershipId);

            Assert.Equal(TestPartyName, editModel.PartyName);
            Assert.Equal(TestDescription, editModel.Description);
            Assert.Equal(TestIsCurrent, editModel.IsCurrent);
            Assert.Equal(TestStartDate, editModel.StartDate);
            Assert.Equal(TestEndDate, editModel.EndDate);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipEditModel editModel = new PartyMembershipEditModel()
            {
                PartyName = TestEditedPartyName,
                Description = TestEditedDescription,
                IsCurrent = TestEditedIsCurrent,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestPartyMembershipId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipEditModel editModel = new PartyMembershipEditModel()
            {
                PartyName = TestEditedPartyName,
                Description = TestEditedDescription,
                IsCurrent = TestEditedIsCurrent,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestPartyMembershipId, editModel);

            PartyMembership editedPartyMembership = dbContext.PartyMemberships.First();

            Assert.Equal(TestEditedPartyName, editedPartyMembership.PartyName);
            Assert.Equal(TestEditedDescription, editedPartyMembership.Description);
            Assert.Equal(TestEditedIsCurrent, editedPartyMembership.IsCurrent);
            Assert.Equal(TestEditedStartDate, editedPartyMembership.StartDate);
            Assert.Equal(TestEditedEndDate, editedPartyMembership.EndDate);
            Assert.Equal(TestCitizenId, editedPartyMembership.CitizenId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipEditModel editModel = new PartyMembershipEditModel()
            {
                PartyName = TestEditedPartyName,
                Description = TestEditedDescription,
                IsCurrent = TestEditedIsCurrent,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestInexistantPartyMembershipId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipEditModel editModel = new PartyMembershipEditModel()
            {
                PartyName = TestEditedPartyName,
                Description = TestEditedDescription,
                IsCurrent = TestEditedIsCurrent,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isEdited = await service.EditAsync(TestPartyMembershipId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            PartyMembershipDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestPartyMembershipId);

            Assert.NotNull(deleteModel);
            Assert.IsType<PartyMembershipDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            PartyMembershipDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestPartyMembershipId);

            Assert.Equal(TestPartyName, deleteModel.PartyName);
            Assert.Equal(TestDescription, deleteModel.Description);
            Assert.Equal(TestIsCurrent, deleteModel.IsCurrent);
            Assert.Equal(TestStartDate, deleteModel.StartDate);
            Assert.Equal(TestEndDate, deleteModel.EndDate);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestPartyMembershipId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestPartyMembershipId);

            PartyMembership deleted = dbContext.PartyMemberships.FirstOrDefault(pm => pm.IsDeleted == false);

            Assert.Null(deleted);

            PartyMembership deletedSoft = dbContext.PartyMemberships.FirstOrDefault(pm => pm.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await partyMembershipsRepository.AddAsync(partyMembership);
            await partyMembershipsRepository.SaveChangesAsync();

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantPartyMembershipId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            bool isDeleted = await service.DeleteAsync(TestPartyMembershipId);

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

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await dbContext.PartyMemberships.AddAsync(partyMembership);

            PartyMembership sameUserPartyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserPartyMembership.Id = TestSameUserPartyMembershipId;

            await dbContext.PartyMemberships.AddAsync(sameUserPartyMembership);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            otherCitizen.Id = TestOtherCitizenId;
            await dbContext.Citizens.AddAsync(otherCitizen);
            await dbContext.SaveChangesAsync();

            PartyMembership otherPartyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = otherCitizen,
                CitizenId = TestOtherCitizenId
            };
            otherPartyMembership.Id = TestOtherPartyMembershipId;

            await dbContext.PartyMemberships.AddAsync(otherPartyMembership);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
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

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await dbContext.PartyMemberships.AddAsync(partyMembership);

            PartyMembership sameUserPartyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserPartyMembership.Id = TestSameUserPartyMembershipId;

            await dbContext.PartyMemberships.AddAsync(sameUserPartyMembership);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
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

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await dbContext.PartyMemberships.AddAsync(partyMembership);

            PartyMembership sameUserPartyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserPartyMembership.Id = TestSameUserPartyMembershipId;

            await dbContext.PartyMemberships.AddAsync(sameUserPartyMembership);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            otherCitizen.Id = TestOtherCitizenId;
            await dbContext.Citizens.AddAsync(otherCitizen);
            await dbContext.SaveChangesAsync();

            PartyMembership otherPartyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = otherCitizen,
                CitizenId = TestOtherCitizenId
            };
            otherPartyMembership.Id = TestOtherPartyMembershipId;

            await dbContext.PartyMemberships.AddAsync(otherPartyMembership);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            IEnumerable<PartyMembershipViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (PartyMembershipViewModel viewModel in viewModels)
            {
                Assert.IsType<PartyMembershipViewModel>(viewModel);
                Assert.Equal(TestPartyName, viewModel.PartyName);
                Assert.Equal(TestDescription, viewModel.Description);
                Assert.Equal(TestIsCurrent, viewModel.IsCurrent);
                Assert.Equal(TestStartDate, viewModel.StartDate);
                Assert.Equal(TestEndDate, viewModel.EndDate);
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

            PartyMembership partyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            partyMembership.Id = TestPartyMembershipId;

            await dbContext.PartyMemberships.AddAsync(partyMembership);

            PartyMembership sameUserPartyMembership = new PartyMembership()
            {
                PartyName = TestPartyName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            sameUserPartyMembership.Id = TestSameUserPartyMembershipId;

            await dbContext.PartyMemberships.AddAsync(sameUserPartyMembership);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            IEnumerable<PartyMembershipViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            PartyMembershipsService service = new PartyMembershipsService(this.mapper, partyMembershipsRepository, citizensRepository);
            IEnumerable<PartyMembershipViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}