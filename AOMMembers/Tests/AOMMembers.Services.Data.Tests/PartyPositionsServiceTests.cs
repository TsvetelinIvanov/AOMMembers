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
using AOMMembers.Web.ViewModels.PartyPositions;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class PartyPositionsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestPartyPositionId = "TestPartyPositionId";
        private const string TestInexistantPartyPositionId = "TestInexistantPartyPositionId";
        private const string TestSameUserPartyPositionId = "TestSameUserPartyPositionId";
        private const string TestOtherPartyPositionId = "TestOtherPartyPositionId";
        private const string TestName = "TestName";
        private const string TestEditedName = "TestEditedName";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const bool TestIsCurrent = true;
        private const bool TestEditedIsCurrent = false;
        private readonly DateTime TestStartDate = new DateTime(2019, 12, 1);
        private readonly DateTime TestEditedStartDate = new DateTime(2022, 4, 12);
        private readonly DateTime TestEndDate = new DateTime(2023, 11, 8);
        private readonly DateTime TestEditedEndDate = new DateTime(2022, 4, 17);
        private const string TestMemberId = "TestMemberId";
        private const string TestOtherMemberId = "TestOtherMemberId";

        private readonly IMapper mapper;

        public PartyPositionsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);            
            await dbContext.SaveChangesAsync();

            PartyPositionInputModel inputModel = new PartyPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.PartyPositions.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPositionInputModel inputModel = new PartyPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            await service.CreateAsync(inputModel, TestUserId);

            PartyPosition partyPosition = dbContext.PartyPositions.FirstOrDefault();

            Assert.NotNull(partyPosition);
            Assert.IsType<PartyPosition>(partyPosition);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPositionInputModel inputModel = new PartyPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            await service.CreateAsync(inputModel, TestUserId);

            PartyPosition partyPosition = dbContext.PartyPositions.First();

            Assert.Equal(TestName, partyPosition.Name);
            Assert.Equal(TestDescription, partyPosition.Description);
            Assert.Equal(TestIsCurrent, partyPosition.IsCurrent);
            Assert.Equal(TestStartDate, partyPosition.StartDate);
            Assert.Equal(TestEndDate, partyPosition.EndDate);
            Assert.Equal(TestMemberId, partyPosition.MemberId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPositionInputModel inputModel = new PartyPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            PartyPosition partyPosition = dbContext.PartyPositions.First();

            Assert.Equal(partyPosition.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionInputModel inputModel = new PartyPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(PartyPositionCreateWithoutMemberBadResult, badResult);
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
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPositionInputModel inputModel = new PartyPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(PartyPositionCreateWithoutMemberBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isAbsent = await service.IsAbsent(TestPartyPositionId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isAbsent = await service.IsAbsent(TestPartyPositionId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isAbsent = await service.IsAbsent(TestInexistantPartyPositionId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            PartyPositionViewModel viewModel = await service.GetViewModelByIdAsync(TestPartyPositionId);

            Assert.NotNull(viewModel);
            Assert.IsType<PartyPositionViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            PartyPositionViewModel viewModel = await service.GetViewModelByIdAsync(TestPartyPositionId);

            Assert.Equal(TestName, viewModel.Name);
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
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            PartyPositionDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestPartyPositionId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<PartyPositionDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            PartyPositionDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestPartyPositionId);

            Assert.Equal(TestName, detailsViewModel.Name);
            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestIsCurrent, detailsViewModel.IsCurrent);
            Assert.Equal(TestStartDate, detailsViewModel.StartDate);
            Assert.Equal(TestEndDate, detailsViewModel.EndDate);
            Assert.Equal(TestMemberId, detailsViewModel.MemberId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isFromMember = await service.IsFromMember(TestPartyPositionId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isFromMember = await service.IsFromMember(TestPartyPositionId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isFromMember = await service.IsFromMember(TestInexistantPartyPositionId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isFromMember = await service.IsFromMember(TestPartyPositionId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isFromMember = await service.IsFromMember(TestInexistantPartyPositionId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            PartyPositionEditModel editModel = await service.GetEditModelByIdAsync(TestPartyPositionId);

            Assert.NotNull(editModel);
            Assert.IsType<PartyPositionEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            PartyPositionEditModel editModel = await service.GetEditModelByIdAsync(TestPartyPositionId);

            Assert.Equal(TestName, editModel.Name);
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
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionEditModel editModel = new PartyPositionEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isEdited = await service.EditAsync(TestPartyPositionId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionEditModel editModel = new PartyPositionEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isEdited = await service.EditAsync(TestPartyPositionId, editModel);

            PartyPosition editedPartyPosition = dbContext.PartyPositions.First();

            Assert.Equal(TestEditedName, editedPartyPosition.Name);
            Assert.Equal(TestEditedDescription, editedPartyPosition.Description);
            Assert.Equal(TestEditedIsCurrent, editedPartyPosition.IsCurrent);
            Assert.Equal(TestEditedStartDate, editedPartyPosition.StartDate);
            Assert.Equal(TestEditedEndDate, editedPartyPosition.EndDate);
            Assert.Equal(TestMemberId, editedPartyPosition.MemberId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionEditModel editModel = new PartyPositionEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isEdited = await service.EditAsync(TestInexistantPartyPositionId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionEditModel editModel = new PartyPositionEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isEdited = await service.EditAsync(TestPartyPositionId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,                
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            PartyPositionDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestPartyPositionId);

            Assert.NotNull(deleteModel);
            Assert.IsType<PartyPositionDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            PartyPositionDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestPartyPositionId);

            Assert.Equal(TestName, deleteModel.Name);
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
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isDeleted = await service.DeleteAsync(TestPartyPositionId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isDeleted = await service.DeleteAsync(TestPartyPositionId);

            PartyPosition deleted = dbContext.PartyPositions.FirstOrDefault(pp => pp.IsDeleted == false);

            Assert.Null(deleted);

            PartyPosition deletedSoft = dbContext.PartyPositions.FirstOrDefault(pp => pp.IsDeleted == true);
            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await partyPositionsRepository.AddAsync(partyPosition);
            await partyPositionsRepository.SaveChangesAsync();

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantPartyPositionId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            bool isDeleted = await service.DeleteAsync(TestPartyPositionId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task GetCountFromMemberReturnsCountCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await dbContext.PartyPositions.AddAsync(partyPosition);

            PartyPosition sameUserPartyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            sameUserPartyPosition.Id = TestSameUserPartyPositionId;

            await dbContext.PartyPositions.AddAsync(sameUserPartyPosition);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            otherMember.Id = TestOtherMemberId;
            await dbContext.Members.AddAsync(otherMember);
            await dbContext.SaveChangesAsync();

            PartyPosition otherPartyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = otherMember,
                MemberId = TestOtherMemberId
            };
            otherPartyPosition.Id = TestOtherPartyPositionId;

            await dbContext.PartyPositions.AddAsync(otherPartyPosition);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
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
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await dbContext.PartyPositions.AddAsync(partyPosition);

            PartyPosition sameUserPartyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            sameUserPartyPosition.Id = TestSameUserPartyPositionId;

            await dbContext.PartyPositions.AddAsync(sameUserPartyPosition);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
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
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await dbContext.PartyPositions.AddAsync(partyPosition);

            PartyPosition sameUserPartyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            sameUserPartyPosition.Id = TestSameUserPartyPositionId;

            await dbContext.PartyPositions.AddAsync(sameUserPartyPosition);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            otherMember.Id = TestOtherMemberId;
            await dbContext.Members.AddAsync(otherMember);
            await dbContext.SaveChangesAsync();

            PartyPosition otherPartyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = otherMember,
                MemberId = TestOtherMemberId
            };
            otherPartyPosition.Id = TestOtherPartyPositionId;

            await dbContext.PartyPositions.AddAsync(otherPartyPosition);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            IEnumerable<PartyPositionViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (PartyPositionViewModel viewModel in viewModels)
            {
                Assert.IsType<PartyPositionViewModel>(viewModel);
                Assert.Equal(TestName, viewModel.Name);
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
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PartyPosition partyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            partyPosition.Id = TestPartyPositionId;

            await dbContext.PartyPositions.AddAsync(partyPosition);

            PartyPosition sameUserPartyPosition = new PartyPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Member = member,
                MemberId = TestMemberId
            };
            sameUserPartyPosition.Id = TestSameUserPartyPositionId;

            await dbContext.PartyPositions.AddAsync(sameUserPartyPosition);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            IEnumerable<PartyPositionViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);

            PartyPositionsService service = new PartyPositionsService(this.mapper, partyPositionsRepository, membersrepository);
            IEnumerable<PartyPositionViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}