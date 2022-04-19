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
using AOMMembers.Web.ViewModels.Relationships;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class RelationshipsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestRelationshipId = "TestRelationshipId";
        private const string TestInexistantRelationshipId = "TestInexistantRelationshipId";
        private const string TestSameUserRelationshipId = "TestSameUserRelationshipId";
        private const string TestOtherRelationshipId = "TestOtherRelationshipId";
        private const string TestKind = "TestKind";
        private const string TestEditedKind = "TestEditedKind";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const string TestMemberId = "TestMemberId";
        private const string TestOtherMemberId = "TestOtherMemberId";
        private const string TestCitizenId = "TestCitizenId";
        private const string TestOtherCitizenId = "TestOtherCitizenId";
        private const string TestCitizenFirstName = "TestCitizenFirstName";
        private const string TestOtherCitizenFirstName = "TestOtherCitizenFirstName";
        private const string TestCitizenSecondName = "TestCitizenSecondName";
        private const string TestCitizenLastName = "TestCitizenLastName";
        private const string TestCitizenGender = "TestCitizenGender";
        private const int TestCitizenAge = 42;
        private readonly DateTime TestCitizenBirthDate = new DateTime(1980, 4, 7);

        private readonly IMapper mapper;

        public RelationshipsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            

            Citizen citizen = new Citizen()
            {
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Gender = TestCitizenGender,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);

            await dbContext.SaveChangesAsync();

            RelationshipInputModel inputModel = new RelationshipInputModel()
            {
                Kind = TestKind,
                Description = TestDescription,
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.Relationships.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);


            Citizen citizen = new Citizen()
            {
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Gender = TestCitizenGender,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);

            await dbContext.SaveChangesAsync();

            RelationshipInputModel inputModel = new RelationshipInputModel()
            {
                Kind = TestKind,
                Description = TestDescription,
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            await service.CreateAsync(inputModel, TestUserId);

            Relationship relationship = dbContext.Relationships.FirstOrDefault();

            Assert.NotNull(relationship);
            Assert.IsType<Relationship>(relationship);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);


            Citizen citizen = new Citizen()
            {
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Gender = TestCitizenGender,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);

            await dbContext.SaveChangesAsync();

            RelationshipInputModel inputModel = new RelationshipInputModel()
            {
                Kind = TestKind,
                Description = TestDescription,
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            await service.CreateAsync(inputModel, TestUserId);

            Relationship relationship = dbContext.Relationships.First();

            Assert.Equal(TestKind, relationship.Kind);
            Assert.Equal(TestDescription, relationship.Description);
            Assert.Equal(TestMemberId, relationship.MemberId);
            Assert.Equal(TestCitizenId, relationship.CitizenId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);


            Citizen citizen = new Citizen()
            {
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Gender = TestCitizenGender,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);

            await dbContext.SaveChangesAsync();

            RelationshipInputModel inputModel = new RelationshipInputModel()
            {
                Kind = TestKind,
                Description = TestDescription,
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            Relationship relationship = dbContext.Relationships.First();

            Assert.Equal(relationship.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Citizen citizen = new Citizen()
            {
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Gender = TestCitizenGender,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);

            await dbContext.SaveChangesAsync();

            RelationshipInputModel inputModel = new RelationshipInputModel()
            {
                Kind = TestKind,
                Description = TestDescription,
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);            
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(RelationshipCreateWithoutMemberBadResult, badResult);
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
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);


            Citizen citizen = new Citizen()
            {
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Gender = TestCitizenGender,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);

            await dbContext.SaveChangesAsync();

            RelationshipInputModel inputModel = new RelationshipInputModel()
            {
                Kind = TestKind,
                Description = TestDescription,
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(RelationshipCreateWithoutMemberBadResult, badResult);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoCitizen()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);

            Citizen citizen = new Citizen()
            {
                FirstName = TestOtherCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Gender = TestCitizenGender,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);

            await dbContext.SaveChangesAsync();

            RelationshipInputModel inputModel = new RelationshipInputModel()
            {
                Kind = TestKind,
                Description = TestDescription,
                FirstName = TestCitizenFirstName,
                SecondName = TestCitizenSecondName,
                LastName = TestCitizenLastName,
                Age = TestCitizenAge,
                BirthDate = TestCitizenBirthDate
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(RelationshipCreateWithInexistantCitizenBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isAbsent = await service.IsAbsent(TestRelationshipId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isAbsent = await service.IsAbsent(TestRelationshipId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isAbsent = await service.IsAbsent(TestInexistantRelationshipId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            RelationshipViewModel viewModel = await service.GetViewModelByIdAsync(TestRelationshipId);

            Assert.NotNull(viewModel);
            Assert.IsType<RelationshipViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            RelationshipViewModel viewModel = await service.GetViewModelByIdAsync(TestRelationshipId);

            Assert.Equal(TestKind, viewModel.Kind);
            Assert.Equal(TestDescription, viewModel.Description);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            RelationshipDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestRelationshipId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<RelationshipDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            RelationshipDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestRelationshipId);

            Assert.Equal(TestKind, detailsViewModel.Kind);
            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestMemberId, detailsViewModel.MemberId);
            Assert.Equal(TestCitizenId, detailsViewModel.CitizenId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isFromMember = await service.IsFromMember(TestRelationshipId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isFromMember = await service.IsFromMember(TestRelationshipId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isFromMember = await service.IsFromMember(TestInexistantRelationshipId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isFromMember = await service.IsFromMember(TestRelationshipId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isFromMember = await service.IsFromMember(TestInexistantRelationshipId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,                
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            RelationshipEditModel editModel = await service.GetEditModelByIdAsync(TestRelationshipId);

            Assert.NotNull(editModel);
            Assert.IsType<RelationshipEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            RelationshipEditModel editModel = await service.GetEditModelByIdAsync(TestRelationshipId);

            Assert.Equal(TestKind, editModel.Kind);
            Assert.Equal(TestDescription, editModel.Description);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipEditModel editModel = new RelationshipEditModel()
            {
                Kind = TestEditedKind,
                Description = TestEditedDescription
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isEdited = await service.EditAsync(TestRelationshipId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipEditModel editModel = new RelationshipEditModel()
            {
                Kind = TestEditedKind,
                Description = TestEditedDescription
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isEdited = await service.EditAsync(TestRelationshipId, editModel);

            Relationship editedRelationship = dbContext.Relationships.First();

            Assert.Equal(TestEditedKind, editedRelationship.Kind);
            Assert.Equal(TestEditedDescription, editedRelationship.Description);
            Assert.Equal(TestMemberId, editedRelationship.MemberId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipEditModel editModel = new RelationshipEditModel()
            {
                Kind = TestEditedKind,
                Description = TestEditedDescription
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isEdited = await service.EditAsync(TestInexistantRelationshipId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            RelationshipEditModel editModel = new RelationshipEditModel()
            {
                Kind = TestEditedKind,
                Description = TestEditedDescription
            };

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isEdited = await service.EditAsync(TestInexistantRelationshipId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,                
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            RelationshipDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestRelationshipId);

            Assert.NotNull(deleteModel);
            Assert.IsType<RelationshipDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            RelationshipDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestRelationshipId);

            Assert.Equal(TestKind, deleteModel.Kind);
            Assert.Equal(TestDescription, deleteModel.Description);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isDeleted = await service.DeleteAsync(TestRelationshipId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isDeleted = await service.DeleteAsync(TestRelationshipId);

            Relationship deleted = dbContext.Relationships.FirstOrDefault(r => r.IsDeleted == false);

            Assert.Null(deleted);

            Relationship deletedSoft = dbContext.Relationships.FirstOrDefault(r => r.IsDeleted == true);
            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await relationshipsRepository.AddAsync(relationship);
            await relationshipsRepository.SaveChangesAsync();

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantRelationshipId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            bool isDeleted = await service.DeleteAsync(TestRelationshipId);

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

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await dbContext.Relationships.AddAsync(relationship);

            Relationship sameUserRelationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            sameUserRelationship.Id = TestSameUserRelationshipId;

            await dbContext.Relationships.AddAsync(sameUserRelationship);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            otherMember.Id = TestOtherMemberId;
            await dbContext.Members.AddAsync(otherMember);
            await dbContext.SaveChangesAsync();

            Relationship otherRelationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = otherMember,
                MemberId = TestOtherMemberId,
                CitizenId = TestCitizenId
            };
            otherRelationship.Id = TestOtherRelationshipId;

            await dbContext.Relationships.AddAsync(otherRelationship);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
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

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await dbContext.Relationships.AddAsync(relationship);

            Relationship sameUserRelationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            sameUserRelationship.Id = TestSameUserRelationshipId;

            await dbContext.Relationships.AddAsync(sameUserRelationship);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
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

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await dbContext.Relationships.AddAsync(relationship);

            Relationship sameUserRelationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            sameUserRelationship.Id = TestSameUserRelationshipId;

            await dbContext.Relationships.AddAsync(sameUserRelationship);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            otherMember.Id = TestOtherMemberId;
            await dbContext.Members.AddAsync(otherMember);
            await dbContext.SaveChangesAsync();

            Relationship otherRelationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = otherMember,
                MemberId = TestOtherMemberId,
                CitizenId = TestCitizenId
            };
            otherRelationship.Id = TestOtherRelationshipId;

            await dbContext.Relationships.AddAsync(otherRelationship);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            IEnumerable<RelationshipViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (RelationshipViewModel viewModel in viewModels)
            {
                Assert.IsType<RelationshipViewModel>(viewModel);
                Assert.Equal(TestKind, viewModel.Kind);
                Assert.Equal(TestDescription, viewModel.Description);
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

            Relationship relationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            relationship.Id = TestRelationshipId;

            await dbContext.Relationships.AddAsync(relationship);

            Relationship sameUserRelationship = new Relationship()
            {
                Kind = TestKind,
                Description = TestDescription,
                Member = member,
                MemberId = TestMemberId,
                CitizenId = TestCitizenId
            };
            sameUserRelationship.Id = TestSameUserRelationshipId;

            await dbContext.Relationships.AddAsync(sameUserRelationship);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            IEnumerable<RelationshipViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<Member> membersrepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensrepository = new EfDeletableEntityRepository<Citizen>(dbContext);

            RelationshipsService service = new RelationshipsService(this.mapper, relationshipsRepository, membersrepository, citizensrepository);
            IEnumerable<RelationshipViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}