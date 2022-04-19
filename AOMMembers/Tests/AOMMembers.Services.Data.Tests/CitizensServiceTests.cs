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
using AOMMembers.Web.ViewModels.Citizens;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class CitizensServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestCitizenId = "TestCitizenId";
        private const string TestInexistantCitizenId = "TestInexistantCitizenId";
        private const string TestFirstName = "TestFirstName";
        private const string TestEditedFirstName = "TestEditedFirstName";
        private const string TestSecondName = "TestSecondName";
        private const string TestEditedSecondName = "TestEditedSecondName";
        private const string TestLastName = "TestLastName";
        private const string TestEditedLastName = "TestEditedLastName";
        private const string TestGender = "TestGender";
        private const string TestEditedGender = "TestEditedGender";
        private const int TestAge = 42;
        private const int TestEditedAge = 31;
        private readonly DateTime TestBirthDate = new DateTime(1980, 4, 7);
        private readonly DateTime TestEditedBirthDate = new DateTime(1991, 1, 10);        
        private const string TestMemberId = "TestMemberId";

        private readonly IMapper mapper;

        public CitizensServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task IsCreatedReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            
            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);            
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isCreated = service.IsCreated(TestUserId);

            Assert.True(isCreated);
        }

        [Fact]
        public async Task IsCreatedReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isCreated = service.IsCreated(TestInexistantUserId);

            Assert.False(isCreated);
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            CitizenInputModel inputModel = new CitizenInputModel()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate
            };

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.Citizens.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            CitizenInputModel inputModel = new CitizenInputModel()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate
            };

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Citizen citizen = dbContext.Citizens.FirstOrDefault();

            Assert.NotNull(citizen);
            Assert.IsType<Citizen>(citizen);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            CitizenInputModel inputModel = new CitizenInputModel()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate
            };

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Citizen citizen = dbContext.Citizens.First();

            Assert.Equal(TestFirstName, citizen.FirstName);
            Assert.Equal(TestSecondName, citizen.SecondName);
            Assert.Equal(TestLastName, citizen.LastName);
            Assert.Equal(TestGender, citizen.Gender);
            Assert.Equal(TestAge, citizen.Age);
            Assert.Equal(TestBirthDate, citizen.BirthDate);            
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            CitizenInputModel inputModel = new CitizenInputModel()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate
            };

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            Citizen citizen = dbContext.Citizens.First();

            Assert.Equal(citizen.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);
            
            CitizenInputModel inputModel = new CitizenInputModel()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate
            };

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(CitizenCreateWithoutMemberBadResult, badResult);
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
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            CitizenInputModel inputModel = new CitizenInputModel()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate
            };

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(CitizenCreateWithoutMemberBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);            

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate                
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isAbsent = await service.IsAbsent(TestCitizenId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantCitizenId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            CitizenViewModel viewModel = await service.GetViewModelByIdAsync(TestCitizenId);

            Assert.NotNull(viewModel);
            Assert.IsType<CitizenViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            CitizenViewModel viewModel = await service.GetViewModelByIdAsync(TestCitizenId);

            Assert.Equal(TestFirstName, viewModel.FirstName);
            Assert.Equal(TestSecondName, viewModel.SecondName);
            Assert.Equal(TestLastName, viewModel.LastName);
            Assert.Equal(TestGender, viewModel.Gender);
            Assert.Equal(TestAge, viewModel.Age);
            Assert.Equal(TestBirthDate, viewModel.BirthDate);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            CitizenDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestCitizenId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<CitizenDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            CitizenDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestCitizenId);

            Assert.Equal(TestFirstName, detailsViewModel.FirstName);
            Assert.Equal(TestSecondName, detailsViewModel.SecondName);
            Assert.Equal(TestLastName, detailsViewModel.LastName);
            Assert.Equal(TestGender, detailsViewModel.Gender);
            Assert.Equal(TestBirthDate, detailsViewModel.BirthDate);
            Assert.Equal(TestBirthDate, detailsViewModel.BirthDate);
            Assert.Equal(TestMemberId, detailsViewModel.MemberId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isFromMember = await service.IsFromMember(TestCitizenId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantCitizenId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isFromMember = await service.IsFromMember(TestCitizenId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantCitizenId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            CitizenEditModel editModel = await service.GetEditModelByIdAsync(TestCitizenId);

            Assert.NotNull(editModel);
            Assert.IsType<CitizenEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            CitizenEditModel editModel = await service.GetEditModelByIdAsync(TestCitizenId);

            Assert.Equal(TestFirstName, editModel.FirstName);
            Assert.Equal(TestSecondName, editModel.SecondName);
            Assert.Equal(TestLastName, editModel.LastName);
            Assert.Equal(TestGender, editModel.Gender);
            Assert.Equal(TestAge, editModel.Age);
            Assert.Equal(TestBirthDate, editModel.BirthDate);            
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizenEditModel editModel = new CitizenEditModel()
            {
                FirstName = TestEditedFirstName,
                SecondName = TestEditedSecondName,
                LastName = TestEditedLastName,
                Gender = TestEditedGender,
                Age = TestEditedAge,                
                BirthDate = TestEditedBirthDate
            };

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isEdited = await service.EditAsync(TestCitizenId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizenEditModel editModel = new CitizenEditModel()
            {
                FirstName = TestEditedFirstName,
                SecondName = TestEditedSecondName,
                LastName = TestEditedLastName,
                Gender = TestEditedGender,
                Age = TestEditedAge,                
                BirthDate = TestEditedBirthDate
            };

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isEdited = await service.EditAsync(TestCitizenId, editModel);

            Citizen editedCitizen = dbContext.Citizens.First();

            Assert.Equal(TestEditedFirstName, editedCitizen.FirstName);
            Assert.Equal(TestEditedSecondName, editedCitizen.SecondName);
            Assert.Equal(TestEditedLastName, editedCitizen.LastName);
            Assert.Equal(TestEditedGender, editedCitizen.Gender);
            Assert.Equal(TestEditedAge, editedCitizen.Age);            
            Assert.Equal(TestEditedBirthDate, editedCitizen.BirthDate);
            Assert.Equal(TestMemberId, editedCitizen.MemberId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizenEditModel editModel = new CitizenEditModel()
            {
                FirstName = TestEditedFirstName,
                SecondName = TestEditedSecondName,
                LastName = TestEditedLastName,
                Gender = TestEditedGender,
                Age = TestEditedAge,
                BirthDate = TestEditedBirthDate
            };

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isEdited = await service.EditAsync(TestInexistantCitizenId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            CitizenDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestCitizenId);

            Assert.NotNull(deleteModel);
            Assert.IsType<CitizenDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();

            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            CitizenDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestCitizenId);

            Assert.Equal(TestFirstName, deleteModel.FirstName);
            Assert.Equal(TestSecondName, deleteModel.SecondName);
            Assert.Equal(TestLastName, deleteModel.LastName);
            Assert.Equal(TestGender, deleteModel.Gender);
            Assert.Equal(TestAge, deleteModel.Age);
            Assert.Equal(TestBirthDate, deleteModel.BirthDate);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();


            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isDeleted = await service.DeleteAsync(TestCitizenId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();


            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isDeleted = await service.DeleteAsync(TestCitizenId);

            Citizen deleted = dbContext.Citizens.FirstOrDefault(c => c.IsDeleted == false);

            Assert.Null(deleted);

            Citizen deletedSoft = dbContext.Citizens.FirstOrDefault(c => c.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            Citizen citizen = new Citizen()
            {
                FirstName = TestFirstName,
                SecondName = TestSecondName,
                LastName = TestLastName,
                Gender = TestGender,
                Age = TestAge,
                BirthDate = TestBirthDate,
                Member = member,
                MemberId = TestMemberId
            };
            citizen.Id = TestCitizenId;

            await citizensRepository.AddAsync(citizen);
            await citizensRepository.SaveChangesAsync();


            CitizensService service = new CitizensService(this.mapper, citizensRepository, membersRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, materialStatesRepository, assetsRepository, lawStatesRepository, lawProblemsRepository, worldviewsRepository, interestsRepository, partyMembershipsRepository, societyHelpsRepository, societyActivitiesRepository, settingsRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantCitizenId);

            Assert.False(isDeleted);
        }
    }
}