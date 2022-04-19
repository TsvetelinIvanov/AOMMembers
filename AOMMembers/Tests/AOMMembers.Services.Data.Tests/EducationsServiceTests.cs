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
using AOMMembers.Web.ViewModels.Educations;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class EducationsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestEducationId = "TestEducationId";
        private const string TestInexistantEducationId = "TestInexistantEducationId";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const string TestCVLink = "TestCVLink";
        private const string TestEditedCVLink = "TestEditedCVLink";
        private const string TestCitizenId = "TestCitizenId";

        private readonly IMapper mapper;

        public EducationsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task IsCreatedReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isCreated = service.IsCreated(TestUserId);

            Assert.True(isCreated);
        }

        [Fact]
        public async Task IsCreatedReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isCreated = service.IsCreated(TestInexistantUserId);

            Assert.False(isCreated);
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            EducationInputModel inputModel = new EducationInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.Educations.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            EducationInputModel inputModel = new EducationInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Education education = dbContext.Educations.FirstOrDefault();

            Assert.NotNull(education);
            Assert.IsType<Education>(education);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            EducationInputModel inputModel = new EducationInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Education education = dbContext.Educations.First();

            Assert.Equal(TestDescription, education.Description);
            Assert.Equal(TestCVLink, education.CVLink);
            Assert.Equal(TestCitizenId, education.CitizenId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            EducationInputModel inputModel = new EducationInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            Education education = dbContext.Educations.First();

            Assert.Equal(education.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            EducationInputModel inputModel = new EducationInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(EducationCreateWithoutCitizenBadResult, badResult);
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
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            EducationInputModel inputModel = new EducationInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(EducationCreateWithoutCitizenBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isAbsent = await service.IsAbsent(TestEducationId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantEducationId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            EducationViewModel viewModel = await service.GetViewModelByIdAsync(TestEducationId);

            Assert.NotNull(viewModel);
            Assert.IsType<EducationViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            EducationViewModel viewModel = await service.GetViewModelByIdAsync(TestEducationId);

            Assert.Equal(TestDescription, viewModel.Description);
            Assert.Equal(TestCVLink, viewModel.CVLink);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            EducationDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestEducationId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<EducationDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            EducationDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestEducationId);

            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestCVLink, detailsViewModel.CVLink);
            Assert.Equal(TestCitizenId, detailsViewModel.CitizenId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isFromMember = await service.IsFromMember(TestEducationId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantEducationId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isFromMember = await service.IsFromMember(TestEducationId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantEducationId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            EducationEditModel editModel = await service.GetEditModelByIdAsync(TestEducationId);

            Assert.NotNull(editModel);
            Assert.IsType<EducationEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            EducationEditModel editModel = await service.GetEditModelByIdAsync(TestEducationId);

            Assert.Equal(TestDescription, editModel.Description);
            Assert.Equal(TestCVLink, editModel.CVLink);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationEditModel editModel = new EducationEditModel()
            {
                Description = TestEditedDescription,
                CVLink = TestEditedCVLink
            };

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isEdited = await service.EditAsync(TestEducationId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationEditModel editModel = new EducationEditModel()
            {
                Description = TestEditedDescription,
                CVLink = TestEditedCVLink
            };

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isEdited = await service.EditAsync(TestEducationId, editModel);

            Education editedEducation = dbContext.Educations.First();

            Assert.Equal(TestEditedDescription, editedEducation.Description);
            Assert.Equal(TestEditedCVLink, editedEducation.CVLink);
            Assert.Equal(TestCitizenId, editedEducation.CitizenId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationEditModel editModel = new EducationEditModel()
            {
                Description = TestEditedDescription,
                CVLink = TestEditedCVLink
            };

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isEdited = await service.EditAsync(TestInexistantEducationId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,                
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            EducationDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestEducationId);

            Assert.NotNull(deleteModel);
            Assert.IsType<EducationDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            EducationDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestEducationId);

            Assert.Equal(TestDescription, deleteModel.Description);
            Assert.Equal(TestCVLink, deleteModel.CVLink);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isDeleted = await service.DeleteAsync(TestEducationId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isDeleted = await service.DeleteAsync(TestEducationId);

            Education deleted = dbContext.Educations.FirstOrDefault(e => e.IsDeleted == false);

            Assert.Null(deleted);

            Education deletedSoft = dbContext.Educations.FirstOrDefault(e => e.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Education education = new Education()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            education.Id = TestEducationId;

            await educationsRepository.AddAsync(education);
            await educationsRepository.SaveChangesAsync();

            EducationsService service = new EducationsService(this.mapper, educationsRepository, citizensRepository, qualificationsRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantEducationId);

            Assert.False(isDeleted);
        }
    }
}