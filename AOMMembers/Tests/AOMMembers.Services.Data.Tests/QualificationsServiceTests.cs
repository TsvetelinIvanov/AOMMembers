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
using AOMMembers.Web.ViewModels.Qualifications;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class QualificationsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestQualificationId = "TestQualificationId";
        private const string TestInexistantQualificationId = "TestInexistantQualificationId";
        private const string TestSameUserQualificationId = "TestSameUserQualificationId";
        private const string TestOtherQualificationId = "TestOtherQualificationId";
        private const string TestName = "TestName";
        private const string TestEditedName = "TestEditedName";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private readonly DateTime TestStartDate = new DateTime(2001, 10, 1);
        private readonly DateTime TestEditedStartDate = new DateTime(2005, 11, 1);
        private readonly DateTime TestEndDate = new DateTime(2005, 7, 11);
        private readonly DateTime TestEditedEndDate = new DateTime(2007, 7, 19);
        private const string TestEducationId = "TestEducationId";
        private const string TestOtherEducationId = "TestOtherEducationId";

        private readonly IMapper mapper;

        public QualificationsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            QualificationInputModel inputModel = new QualificationInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.Qualifications.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            QualificationInputModel inputModel = new QualificationInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Qualification qualification = dbContext.Qualifications.FirstOrDefault();

            Assert.NotNull(qualification);
            Assert.IsType<Qualification>(qualification);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            QualificationInputModel inputModel = new QualificationInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Qualification qualification = dbContext.Qualifications.First();

            Assert.Equal(TestName, qualification.Name);
            Assert.Equal(TestDescription, qualification.Description);
            Assert.Equal(TestStartDate, qualification.StartDate);
            Assert.Equal(TestEndDate, qualification.EndDate);
            Assert.Equal(TestEducationId, qualification.EducationId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            QualificationInputModel inputModel = new QualificationInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            Qualification qualification = dbContext.Qualifications.First();

            Assert.Equal(qualification.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationInputModel inputModel = new QualificationInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(QualificationCreateWithoutEducationBadResult, badResult);
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
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            QualificationInputModel inputModel = new QualificationInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(QualificationCreateWithoutEducationBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isAbsent = await service.IsAbsent(TestQualificationId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isAbsent = await service.IsAbsent(TestQualificationId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantQualificationId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            QualificationViewModel viewModel = await service.GetViewModelByIdAsync(TestQualificationId);

            Assert.NotNull(viewModel);
            Assert.IsType<QualificationViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            QualificationViewModel viewModel = await service.GetViewModelByIdAsync(TestQualificationId);

            Assert.Equal(TestName, viewModel.Name);
            Assert.Equal(TestDescription, viewModel.Description);
            Assert.Equal(TestStartDate, viewModel.StartDate);
            Assert.Equal(TestEndDate, viewModel.EndDate);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            QualificationDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestQualificationId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<QualificationDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            QualificationDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestQualificationId);

            Assert.Equal(TestName, detailsViewModel.Name);
            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestStartDate, detailsViewModel.StartDate);
            Assert.Equal(TestEndDate, detailsViewModel.EndDate);
            Assert.Equal(TestEducationId, detailsViewModel.EducationId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isFromMember = await service.IsFromMember(TestQualificationId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isFromMember = await service.IsFromMember(TestQualificationId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantQualificationId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isFromMember = await service.IsFromMember(TestQualificationId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantQualificationId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            QualificationEditModel editModel = await service.GetEditModelByIdAsync(TestQualificationId);

            Assert.NotNull(editModel);
            Assert.IsType<QualificationEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            QualificationEditModel editModel = await service.GetEditModelByIdAsync(TestQualificationId);

            Assert.Equal(TestName, editModel.Name);
            Assert.Equal(TestDescription, editModel.Description);
            Assert.Equal(TestStartDate, editModel.StartDate);
            Assert.Equal(TestEndDate, editModel.EndDate);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationEditModel editModel = new QualificationEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isEdited = await service.EditAsync(TestQualificationId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationEditModel editModel = new QualificationEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isEdited = await service.EditAsync(TestQualificationId, editModel);

            Qualification editedQualification = dbContext.Qualifications.First();

            Assert.Equal(TestEditedName, editedQualification.Name);
            Assert.Equal(TestEditedDescription, editedQualification.Description);
            Assert.Equal(TestEditedStartDate, editedQualification.StartDate);
            Assert.Equal(TestEditedEndDate, editedQualification.EndDate);
            Assert.Equal(TestEducationId, editedQualification.EducationId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationEditModel editModel = new QualificationEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isEdited = await service.EditAsync(TestInexistantQualificationId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationEditModel editModel = new QualificationEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isEdited = await service.EditAsync(TestQualificationId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            QualificationDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestQualificationId);

            Assert.NotNull(deleteModel);
            Assert.IsType<QualificationDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            QualificationDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestQualificationId);

            Assert.Equal(TestName, deleteModel.Name);
            Assert.Equal(TestDescription, deleteModel.Description);
            Assert.Equal(TestStartDate, deleteModel.StartDate);
            Assert.Equal(TestEndDate, deleteModel.EndDate);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isDeleted = await service.DeleteAsync(TestQualificationId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isDeleted = await service.DeleteAsync(TestQualificationId);

            Qualification deleted = dbContext.Qualifications.FirstOrDefault(q => q.IsDeleted == false);

            Assert.Null(deleted);

            Qualification deletedSoft = dbContext.Qualifications.FirstOrDefault(q => q.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await qualificationsRepository.AddAsync(qualification);
            await qualificationsRepository.SaveChangesAsync();

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantQualificationId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            bool isDeleted = await service.DeleteAsync(TestQualificationId);

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
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await dbContext.Qualifications.AddAsync(qualification);

            Qualification sameUserQualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            sameUserQualification.Id = TestSameUserQualificationId;

            await dbContext.Qualifications.AddAsync(sameUserQualification);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            await dbContext.Citizens.AddAsync(otherCitizen);
            Education otherEducation = new Education() { Citizen = otherCitizen };
            otherEducation.Id = TestOtherEducationId;
            await dbContext.Educations.AddAsync(otherEducation);
            await dbContext.SaveChangesAsync();

            Qualification otherQualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = otherEducation,
                EducationId = TestOtherEducationId
            };
            otherQualification.Id = TestOtherQualificationId;

            await dbContext.Qualifications.AddAsync(otherQualification);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
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
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await dbContext.Qualifications.AddAsync(qualification);

            Qualification sameUserQualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            sameUserQualification.Id = TestSameUserQualificationId;

            await dbContext.Qualifications.AddAsync(sameUserQualification);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
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
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await dbContext.Qualifications.AddAsync(qualification);

            Qualification sameUserQualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            sameUserQualification.Id = TestSameUserQualificationId;

            await dbContext.Qualifications.AddAsync(sameUserQualification);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            await dbContext.Citizens.AddAsync(otherCitizen);
            Education otherEducation = new Education() { Citizen = otherCitizen };
            otherEducation.Id = TestOtherEducationId;
            await dbContext.Educations.AddAsync(otherEducation);
            await dbContext.SaveChangesAsync();

            Qualification otherQualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = otherEducation,
                EducationId = TestOtherEducationId
            };
            otherQualification.Id = TestOtherQualificationId;

            await dbContext.Qualifications.AddAsync(otherQualification);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            IEnumerable<QualificationViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (QualificationViewModel viewModel in viewModels)
            {
                Assert.IsType<QualificationViewModel>(viewModel);
                Assert.Equal(TestName, viewModel.Name);
                Assert.Equal(TestDescription, viewModel.Description);
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
            await dbContext.Citizens.AddAsync(citizen);
            Education education = new Education() { Citizen = citizen };
            education.Id = TestEducationId;
            await dbContext.Educations.AddAsync(education);
            await dbContext.SaveChangesAsync();

            Qualification qualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            qualification.Id = TestQualificationId;

            await dbContext.Qualifications.AddAsync(qualification);

            Qualification sameUserQualification = new Qualification()
            {
                Name = TestName,
                Description = TestDescription,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Education = education,
                EducationId = TestEducationId
            };
            sameUserQualification.Id = TestSameUserQualificationId;

            await dbContext.Qualifications.AddAsync(sameUserQualification);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            IEnumerable<QualificationViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);

            QualificationsService service = new QualificationsService(this.mapper, qualificationsRepository, educationsRepository);
            IEnumerable<QualificationViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}