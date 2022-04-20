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
using AOMMembers.Web.ViewModels.WorkPositions;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class WorkPositionsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestWorkPositionId = "TestWorkPositionId";
        private const string TestInexistantWorkPositionId = "TestInexistantWorkPositionId";
        private const string TestSameUserWorkPositionId = "TestSameUserWorkPositionId";
        private const string TestOtherWorkPositionId = "TestOtherWorkPositionId";
        private const string TestName = "TestName";
        private const string TestEditedName = "TestEditedName";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const bool TestIsCurrent = true;
        private const bool TestEditedIsCurrent = false;
        private readonly DateTime TestStartDate = new DateTime(2007, 1, 1);
        private readonly DateTime TestEditedStartDate = new DateTime(2011, 1, 1);
        private readonly DateTime TestEndDate = new DateTime(2025, 1, 1);
        private readonly DateTime TestEditedEndDate = new DateTime(2017, 1, 1);
        private const string TestCareerId = "TestEducationId";
        private const string TestOtherCareerId = "TestOtherCareerId";

        private readonly IMapper mapper;

        public WorkPositionsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPositionInputModel inputModel = new WorkPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.WorkPositions.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPositionInputModel inputModel = new WorkPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            await service.CreateAsync(inputModel, TestUserId);

            WorkPosition workPosition = dbContext.WorkPositions.FirstOrDefault();

            Assert.NotNull(workPosition);
            Assert.IsType<WorkPosition>(workPosition);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPositionInputModel inputModel = new WorkPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            await service.CreateAsync(inputModel, TestUserId);

            WorkPosition workPosition = dbContext.WorkPositions.First();

            Assert.Equal(TestName, workPosition.Name);
            Assert.Equal(TestDescription, workPosition.Description);
            Assert.Equal(TestIsCurrent, workPosition.IsCurrent);
            Assert.Equal(TestStartDate, workPosition.StartDate);
            Assert.Equal(TestEndDate, workPosition.EndDate);
            Assert.Equal(TestCareerId, workPosition.CareerId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPositionInputModel inputModel = new WorkPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            WorkPosition workPosition = dbContext.WorkPositions.First();

            Assert.Equal(workPosition.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionInputModel inputModel = new WorkPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(WorkPositionCreateWithoutCareerBadResult, badResult);
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
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPositionInputModel inputModel = new WorkPositionInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate
            };

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(WorkPositionCreateWithoutCareerBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isAbsent = await service.IsAbsent(TestWorkPositionId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isAbsent = await service.IsAbsent(TestWorkPositionId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantWorkPositionId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            WorkPositionViewModel viewModel = await service.GetViewModelByIdAsync(TestWorkPositionId);

            Assert.NotNull(viewModel);
            Assert.IsType<WorkPositionViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            WorkPositionViewModel viewModel = await service.GetViewModelByIdAsync(TestWorkPositionId);

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
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            WorkPositionDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestWorkPositionId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<WorkPositionDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            WorkPositionDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestWorkPositionId);

            Assert.Equal(TestName, detailsViewModel.Name);
            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestIsCurrent, detailsViewModel.IsCurrent);
            Assert.Equal(TestStartDate, detailsViewModel.StartDate);
            Assert.Equal(TestEndDate, detailsViewModel.EndDate);
            Assert.Equal(TestCareerId, detailsViewModel.CareerId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isFromMember = await service.IsFromMember(TestWorkPositionId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isFromMember = await service.IsFromMember(TestWorkPositionId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantWorkPositionId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isFromMember = await service.IsFromMember(TestWorkPositionId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantWorkPositionId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            WorkPositionEditModel editModel = await service.GetEditModelByIdAsync(TestWorkPositionId);

            Assert.NotNull(editModel);
            Assert.IsType<WorkPositionEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            WorkPositionEditModel editModel = await service.GetEditModelByIdAsync(TestWorkPositionId);

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
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionEditModel editModel = new WorkPositionEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                IsCurrent = TestEditedIsCurrent,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isEdited = await service.EditAsync(TestWorkPositionId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionEditModel editModel = new WorkPositionEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                IsCurrent = TestEditedIsCurrent,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isEdited = await service.EditAsync(TestWorkPositionId, editModel);

            WorkPosition editedWorkPosition = dbContext.WorkPositions.First();

            Assert.Equal(TestEditedName, editedWorkPosition.Name);
            Assert.Equal(TestEditedDescription, editedWorkPosition.Description);
            Assert.Equal(TestEditedIsCurrent, editedWorkPosition.IsCurrent);
            Assert.Equal(TestEditedStartDate, editedWorkPosition.StartDate);
            Assert.Equal(TestEditedEndDate, editedWorkPosition.EndDate);
            Assert.Equal(TestCareerId, editedWorkPosition.CareerId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionEditModel editModel = new WorkPositionEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                IsCurrent = TestEditedIsCurrent,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isEdited = await service.EditAsync(TestInexistantWorkPositionId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionEditModel editModel = new WorkPositionEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                IsCurrent = TestEditedIsCurrent,
                StartDate = TestEditedStartDate,
                EndDate = TestEditedEndDate
            };

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isEdited = await service.EditAsync(TestWorkPositionId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            WorkPositionDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestWorkPositionId);

            Assert.NotNull(deleteModel);
            Assert.IsType<WorkPositionDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            WorkPositionDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestWorkPositionId);

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
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isDeleted = await service.DeleteAsync(TestWorkPositionId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isDeleted = await service.DeleteAsync(TestWorkPositionId);

            WorkPosition deleted = dbContext.WorkPositions.FirstOrDefault(wp => wp.IsDeleted == false);

            Assert.Null(deleted);

            WorkPosition deletedSoft = dbContext.WorkPositions.FirstOrDefault(wp => wp.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await workPositionsRepository.AddAsync(workPosition);
            await workPositionsRepository.SaveChangesAsync();

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantWorkPositionId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            bool isDeleted = await service.DeleteAsync(TestWorkPositionId);

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
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await dbContext.WorkPositions.AddAsync(workPosition);

            WorkPosition sameUserWorkPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            sameUserWorkPosition.Id = TestSameUserWorkPositionId;

            await dbContext.WorkPositions.AddAsync(sameUserWorkPosition);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            await dbContext.Citizens.AddAsync(otherCitizen);
            Career otherCareer = new Career() { Citizen = otherCitizen };
            otherCareer.Id = TestOtherCareerId;
            await dbContext.Careers.AddAsync(otherCareer);
            await dbContext.SaveChangesAsync();

            WorkPosition otherWorkPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = otherCareer,
                CareerId = TestOtherCareerId
            };
            otherWorkPosition.Id = TestOtherWorkPositionId;

            await dbContext.WorkPositions.AddAsync(otherWorkPosition);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
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
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await dbContext.WorkPositions.AddAsync(workPosition);

            WorkPosition sameUserWorkPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            sameUserWorkPosition.Id = TestSameUserWorkPositionId;

            await dbContext.WorkPositions.AddAsync(sameUserWorkPosition);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
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
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await dbContext.WorkPositions.AddAsync(workPosition);

            WorkPosition sameUserWorkPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            sameUserWorkPosition.Id = TestSameUserWorkPositionId;

            await dbContext.WorkPositions.AddAsync(sameUserWorkPosition);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            await dbContext.Citizens.AddAsync(otherCitizen);
            Career otherCareer = new Career() { Citizen = otherCitizen };
            otherCareer.Id = TestOtherCareerId;
            await dbContext.Careers.AddAsync(otherCareer);
            await dbContext.SaveChangesAsync();

            WorkPosition otherWorkPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = otherCareer,
                CareerId = TestOtherCareerId
            };
            otherWorkPosition.Id = TestOtherWorkPositionId;

            await dbContext.WorkPositions.AddAsync(otherWorkPosition);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            IEnumerable<WorkPositionViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (WorkPositionViewModel viewModel in viewModels)
            {
                Assert.IsType<WorkPositionViewModel>(viewModel);
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
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Career career = new Career() { Citizen = citizen };
            career.Id = TestCareerId;
            await dbContext.Careers.AddAsync(career);
            await dbContext.SaveChangesAsync();

            WorkPosition workPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            workPosition.Id = TestWorkPositionId;

            await dbContext.WorkPositions.AddAsync(workPosition);

            WorkPosition sameUserWorkPosition = new WorkPosition()
            {
                Name = TestName,
                Description = TestDescription,
                IsCurrent = TestIsCurrent,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                Career = career,
                CareerId = TestCareerId
            };
            sameUserWorkPosition.Id = TestSameUserWorkPositionId;

            await dbContext.WorkPositions.AddAsync(sameUserWorkPosition);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            IEnumerable<WorkPositionViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);

            WorkPositionsService service = new WorkPositionsService(this.mapper, workPositionsRepository, careersRepository);
            IEnumerable<WorkPositionViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}