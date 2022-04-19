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
using AOMMembers.Web.ViewModels.Careers;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class CareersServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";        
        private const string TestCareerId = "TestCareerId";
        private const string TestInexistantCareerId = "TestInexistantCareerId";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const string TestCVLink = "TestCVLink";
        private const string TestEditedCVLink = "TestEditedCVLink";
        private const string TestCitizenId = "TestCitizenId";

        private readonly IMapper mapper;

        public CareersServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task IsCreatedReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext); 
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {                
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isCreated = service.IsCreated(TestUserId);

            Assert.True(isCreated);
        }

        [Fact]
        public async Task IsCreatedReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isCreated = service.IsCreated(TestInexistantUserId);

            Assert.False(isCreated);
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            CareerInputModel inputModel = new CareerInputModel()
            {                
                Description = TestDescription,
                CVLink = TestCVLink
            };

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.Careers.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            CareerInputModel inputModel = new CareerInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Career career = dbContext.Careers.FirstOrDefault();

            Assert.NotNull(career);
            Assert.IsType<Career>(career);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            CareerInputModel inputModel = new CareerInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Career career = dbContext.Careers.First();
            
            Assert.Equal(TestDescription, career.Description);
            Assert.Equal(TestCVLink, career.CVLink);
            Assert.Equal(TestCitizenId, career.CitizenId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            CareerInputModel inputModel = new CareerInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            Career career = dbContext.Careers.First();

            Assert.Equal(career.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            CareerInputModel inputModel = new CareerInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(CareerCreateWithoutCitizenBadResult, badResult);
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
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            CareerInputModel inputModel = new CareerInputModel()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(CareerCreateWithoutCitizenBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Career career = new Career()
            {                
                Description = TestDescription,
                CVLink = TestCVLink
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isAbsent = await service.IsAbsent(TestCareerId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantCareerId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            CareerViewModel viewModel = await service.GetViewModelByIdAsync(TestCareerId);

            Assert.NotNull(viewModel);
            Assert.IsType<CareerViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            CareerViewModel viewModel = await service.GetViewModelByIdAsync(TestCareerId);
                        
            Assert.Equal(TestDescription, viewModel.Description);
            Assert.Equal(TestCVLink, viewModel.CVLink);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            CareerDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestCareerId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<CareerDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            CareerDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestCareerId);
            
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
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {                
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isFromMember = await service.IsFromMember(TestCareerId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantCareerId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isFromMember = await service.IsFromMember(TestCareerId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantCareerId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            CareerEditModel editModel = await service.GetEditModelByIdAsync(TestCareerId);

            Assert.NotNull(editModel);
            Assert.IsType<CareerEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            CareerEditModel editModel = await service.GetEditModelByIdAsync(TestCareerId);
            
            Assert.Equal(TestDescription, editModel.Description);
            Assert.Equal(TestCVLink, editModel.CVLink);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareerEditModel editModel = new CareerEditModel()
            {                
                Description = TestEditedDescription,
                CVLink = TestEditedCVLink
            };

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isEdited = await service.EditAsync(TestCareerId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareerEditModel editModel = new CareerEditModel()
            {
                Description = TestEditedDescription,
                CVLink = TestEditedCVLink
            };

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isEdited = await service.EditAsync(TestCareerId, editModel);

            Career editedCareer = dbContext.Careers.First();
            
            Assert.Equal(TestEditedDescription, editedCareer.Description);
            Assert.Equal(TestEditedCVLink, editedCareer.CVLink);
            Assert.Equal(TestCitizenId, editedCareer.CitizenId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareerEditModel editModel = new CareerEditModel()
            {
                Description = TestEditedDescription,
                CVLink = TestEditedCVLink
            };

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isEdited = await service.EditAsync(TestInexistantCareerId, editModel);            

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            CareerDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestCareerId);

            Assert.NotNull(deleteModel);
            Assert.IsType<CareerDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            CareerDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestCareerId);
            
            Assert.Equal(TestDescription, deleteModel.Description);
            Assert.Equal(TestCVLink, deleteModel.CVLink);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isDeleted = await service.DeleteAsync(TestCareerId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isDeleted = await service.DeleteAsync(TestCareerId);

            Career deleted = dbContext.Careers.FirstOrDefault(c => c.IsDeleted == false);

            Assert.Null(deleted);

            Career deletedSoft = dbContext.Careers.FirstOrDefault(c => c.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            Career career = new Career()
            {
                Description = TestDescription,
                CVLink = TestCVLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            career.Id = TestCareerId;

            await careersRepository.AddAsync(career);
            await careersRepository.SaveChangesAsync();

            CareersService service = new CareersService(this.mapper, careersRepository, citizensRepository, workPositionsRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantCareerId);

            Assert.False(isDeleted);
        }
    }
}