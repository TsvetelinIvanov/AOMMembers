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
using AOMMembers.Web.ViewModels.MaterialStates;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class MaterialStatesServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestMaterialStateId = "TestMaterialStateId";
        private const string TestInexistantMaterialStateId = "TestInexistantMaterialStateId";
        private const decimal TestRiches = 10000;
        private const decimal TestEditedRiches = 10001;
        private const decimal TestMoney = 1000;
        private const decimal TestEditedMoney = 1001;
        private const decimal TestMonthIncome = 1100;
        private const decimal TestEditedMonthIncome = 1101;
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const string TestTaxDeclarationLink = "TestTaxDeclarationLink";
        private const string TestEditedTaxDeclarationLink = "TestEditedTaxDeclarationLink";
        private const string TestCitizenId = "TestCitizenId";

        private readonly IMapper mapper;

        public MaterialStatesServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task IsCreatedReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isCreated = service.IsCreated(TestUserId);

            Assert.True(isCreated);
        }

        [Fact]
        public async Task IsCreatedReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isCreated = service.IsCreated(TestInexistantUserId);

            Assert.False(isCreated);
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialStateInputModel inputModel = new MaterialStateInputModel()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink
            };

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.MaterialStates.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialStateInputModel inputModel = new MaterialStateInputModel()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink
            };

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            MaterialState materialState = dbContext.MaterialStates.FirstOrDefault();

            Assert.NotNull(materialState);
            Assert.IsType<MaterialState>(materialState);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialStateInputModel inputModel = new MaterialStateInputModel()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink
            };

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            MaterialState materialState = dbContext.MaterialStates.First();

            Assert.Equal(TestRiches, materialState.Riches);
            Assert.Equal(TestMoney, materialState.Money);
            Assert.Equal(TestMonthIncome, materialState.MonthIncome);
            Assert.Equal(TestDescription, materialState.Description);
            Assert.Equal(TestTaxDeclarationLink, materialState.TaxDeclarationLink);
            Assert.Equal(TestCitizenId, materialState.CitizenId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialStateInputModel inputModel = new MaterialStateInputModel()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink
            };

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            MaterialState materialState = dbContext.MaterialStates.First();

            Assert.Equal(materialState.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialStateInputModel inputModel = new MaterialStateInputModel()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink
            };

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(MaterialStateCreateWithoutCitizenBadResult, badResult);
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
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialStateInputModel inputModel = new MaterialStateInputModel()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink
            };

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(MaterialStateCreateWithoutCitizenBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isAbsent = await service.IsAbsent(TestMaterialStateId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantMaterialStateId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            MaterialStateViewModel viewModel = await service.GetViewModelByIdAsync(TestMaterialStateId);

            Assert.NotNull(viewModel);
            Assert.IsType<MaterialStateViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            MaterialStateViewModel viewModel = await service.GetViewModelByIdAsync(TestMaterialStateId);

            Assert.Equal(TestRiches, viewModel.Riches);
            Assert.Equal(TestMoney, viewModel.Money);
            Assert.Equal(TestMonthIncome, viewModel.MonthIncome);
            Assert.Equal(TestDescription, viewModel.Description);
            Assert.Equal(TestTaxDeclarationLink, viewModel.TaxDeclarationLink);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            MaterialStateDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestMaterialStateId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<MaterialStateDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            MaterialStateDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestMaterialStateId);

            Assert.Equal(TestRiches, detailsViewModel.Riches);
            Assert.Equal(TestMoney, detailsViewModel.Money);
            Assert.Equal(TestMonthIncome, detailsViewModel.MonthIncome);
            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestTaxDeclarationLink, detailsViewModel.TaxDeclarationLink);
            Assert.Equal(TestCitizenId, detailsViewModel.CitizenId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isFromMember = await service.IsFromMember(TestMaterialStateId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantMaterialStateId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isFromMember = await service.IsFromMember(TestMaterialStateId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantMaterialStateId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,                
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            MaterialStateEditModel editModel = await service.GetEditModelByIdAsync(TestMaterialStateId);

            Assert.NotNull(editModel);
            Assert.IsType<MaterialStateEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            MaterialStateEditModel editModel = await service.GetEditModelByIdAsync(TestMaterialStateId);

            Assert.Equal(TestRiches, editModel.Riches);
            Assert.Equal(TestMoney, editModel.Money);
            Assert.Equal(TestMonthIncome, editModel.MonthIncome);
            Assert.Equal(TestDescription, editModel.Description);
            Assert.Equal(TestTaxDeclarationLink, editModel.TaxDeclarationLink);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStateEditModel editModel = new MaterialStateEditModel()
            {
                Riches = TestEditedRiches,
                Money = TestEditedMoney,
                MonthIncome = TestEditedMonthIncome,
                Description = TestEditedDescription,
                TaxDeclarationLink = TestEditedTaxDeclarationLink,
            };

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isEdited = await service.EditAsync(TestMaterialStateId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStateEditModel editModel = new MaterialStateEditModel()
            {
                Riches = TestEditedRiches,
                Money = TestEditedMoney,
                MonthIncome = TestEditedMonthIncome,
                Description = TestEditedDescription,
                TaxDeclarationLink = TestEditedTaxDeclarationLink,
            };

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isEdited = await service.EditAsync(TestMaterialStateId, editModel);

            MaterialState editedMaterialState = dbContext.MaterialStates.First();

            Assert.Equal(TestEditedRiches, editedMaterialState.Riches);
            Assert.Equal(TestEditedMoney, editedMaterialState.Money);
            Assert.Equal(TestEditedMonthIncome, editedMaterialState.MonthIncome);
            Assert.Equal(TestEditedDescription, editedMaterialState.Description);
            Assert.Equal(TestEditedTaxDeclarationLink, editedMaterialState.TaxDeclarationLink);
            Assert.Equal(TestCitizenId, editedMaterialState.CitizenId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStateEditModel editModel = new MaterialStateEditModel()
            {
                Riches = TestEditedRiches,
                Money = TestEditedMoney,
                MonthIncome = TestEditedMonthIncome,
                Description = TestEditedDescription,
                TaxDeclarationLink = TestEditedTaxDeclarationLink,
            };

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isEdited = await service.EditAsync(TestInexistantMaterialStateId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            MaterialStateDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestMaterialStateId);

            Assert.NotNull(deleteModel);
            Assert.IsType<MaterialStateDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            MaterialStateDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestMaterialStateId);

            Assert.Equal(TestRiches, deleteModel.Riches);
            Assert.Equal(TestMoney, deleteModel.Money);
            Assert.Equal(TestMonthIncome, deleteModel.MonthIncome);
            Assert.Equal(TestDescription, deleteModel.Description);
            Assert.Equal(TestTaxDeclarationLink, deleteModel.TaxDeclarationLink);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isDeleted = await service.DeleteAsync(TestMaterialStateId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isDeleted = await service.DeleteAsync(TestMaterialStateId);

            MaterialState deleted = dbContext.MaterialStates.FirstOrDefault(ms => ms.IsDeleted == false);

            Assert.Null(deleted);

            MaterialState deletedSoft = dbContext.MaterialStates.FirstOrDefault(ms => ms.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            citizen.Id = TestCitizenId;
            await dbContext.Citizens.AddAsync(citizen);
            await dbContext.SaveChangesAsync();

            MaterialState materialState = new MaterialState()
            {
                Riches = TestRiches,
                Money = TestMoney,
                MonthIncome = TestMonthIncome,
                Description = TestDescription,
                TaxDeclarationLink = TestTaxDeclarationLink,
                Citizen = citizen,
                CitizenId = TestCitizenId
            };
            materialState.Id = TestMaterialStateId;

            await materialStatesRepository.AddAsync(materialState);
            await materialStatesRepository.SaveChangesAsync();

            MaterialStatesService service = new MaterialStatesService(this.mapper, materialStatesRepository, citizensRepository, assetsRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantMaterialStateId);

            Assert.False(isDeleted);
        }
    }
}