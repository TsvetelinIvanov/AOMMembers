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
using AOMMembers.Web.ViewModels.Assets;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class AssetsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestAssetId = "TestAssetId";
        private const string TestInexistantAssetId = "TestInexistantAssetId";
        private const string TestSameUserAssetId = "TestSameUserAssetId";
        private const string TestOtherAssetId = "TestOtherAssetId";
        private const string TestName = "TestName";
        private const string TestEditedName = "TestEditedName";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const decimal TestWorthiness = 1000;
        private const decimal TestEditedWorthiness = 1001;
        private const string TestMaterialStateId = "TestMaterialStateId";        
        private const string TestOtherMaterialStateId = "TestOtherMaterialStateId";

        private readonly IMapper mapper;

        public AssetsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            AssetInputModel inputModel = new AssetInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness
            };

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.Assets.Count());            
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            AssetInputModel inputModel = new AssetInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness
            };

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            await service.CreateAsync(inputModel, TestUserId);            

            Asset asset = dbContext.Assets.FirstOrDefault();

            Assert.NotNull(asset);
            Assert.IsType<Asset>(asset);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            AssetInputModel inputModel = new AssetInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness
            };

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Asset asset = dbContext.Assets.First();

            Assert.Equal(TestName, asset.Name);
            Assert.Equal(TestDescription, asset.Description);
            Assert.Equal(TestWorthiness, asset.Worthiness);
            Assert.Equal(TestMaterialStateId, asset.MaterialStateId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            AssetInputModel inputModel = new AssetInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness
            };

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            Asset asset = dbContext.Assets.First();

            Assert.Equal(asset.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            
            AssetInputModel inputModel = new AssetInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness
            };

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(AssetCreateWithoutMaterialStateBadResult, badResult);
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
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            AssetInputModel inputModel = new AssetInputModel()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness                
            };

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);            

            Assert.Equal(AssetCreateWithoutMaterialStateBadResult, badResult);            
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            
            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isAbsent = await service.IsAbsent(TestAssetId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isAbsent = await service.IsAbsent(TestAssetId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            
            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantAssetId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            
            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            AssetViewModel viewModel = await service.GetViewModelByIdAsync(TestAssetId);

            Assert.NotNull(viewModel);
            Assert.IsType<AssetViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            
            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            AssetViewModel viewModel = await service.GetViewModelByIdAsync(TestAssetId);

            Assert.Equal(TestName, viewModel.Name);
            Assert.Equal(TestDescription, viewModel.Description);
            Assert.Equal(TestWorthiness, viewModel.Worthiness);            
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            
            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            AssetDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestAssetId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<AssetDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            
            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            AssetDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestAssetId);

            Assert.Equal(TestName, detailsViewModel.Name);
            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestWorthiness, detailsViewModel.Worthiness);
            Assert.Equal(TestMaterialStateId, detailsViewModel.MaterialStateId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isFromMember = await service.IsFromMember(TestAssetId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isFromMember = await service.IsFromMember(TestAssetId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantAssetId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isFromMember = await service.IsFromMember(TestAssetId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantAssetId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            
            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            AssetEditModel editModel = await service.GetEditModelByIdAsync(TestAssetId);

            Assert.NotNull(editModel);
            Assert.IsType<AssetEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            
            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            AssetEditModel editModel = await service.GetEditModelByIdAsync(TestAssetId);

            Assert.Equal(TestName, editModel.Name);
            Assert.Equal(TestDescription, editModel.Description);
            Assert.Equal(TestWorthiness, editModel.Worthiness);            
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetEditModel editModel = new AssetEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Worthiness = TestEditedWorthiness
            };

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isEdited = await service.EditAsync(TestAssetId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetEditModel editModel = new AssetEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Worthiness = TestEditedWorthiness
            };

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isEdited = await service.EditAsync(TestAssetId, editModel);

            Asset editedAsset = dbContext.Assets.First();

            Assert.Equal(TestEditedName, editedAsset.Name);
            Assert.Equal(TestEditedDescription, editedAsset.Description);
            Assert.Equal(TestEditedWorthiness, editedAsset.Worthiness);
            Assert.Equal(TestMaterialStateId, editedAsset.MaterialStateId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetEditModel editModel = new AssetEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Worthiness = TestEditedWorthiness
            };

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isEdited = await service.EditAsync(TestInexistantAssetId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            AssetEditModel editModel = new AssetEditModel()
            {
                Name = TestEditedName,
                Description = TestEditedDescription,
                Worthiness = TestEditedWorthiness
            };

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isEdited = await service.EditAsync(TestAssetId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            AssetDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestAssetId);

            Assert.NotNull(deleteModel);
            Assert.IsType<AssetDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            AssetDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestAssetId);

            Assert.Equal(TestName, deleteModel.Name);
            Assert.Equal(TestDescription, deleteModel.Description);
            Assert.Equal(TestWorthiness, deleteModel.Worthiness);            
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();            

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isDeleted = await service.DeleteAsync(TestAssetId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();            

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isDeleted = await service.DeleteAsync(TestAssetId);

            Asset deleted = dbContext.Assets.FirstOrDefault(a => a.IsDeleted == false);

            Assert.Null(deleted);

            Asset deletedSoft = dbContext.Assets.FirstOrDefault(a => a.IsDeleted == true);
            Assert.NotNull(deletedSoft);            
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await assetsRepository.AddAsync(asset);
            await assetsRepository.SaveChangesAsync();            

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantAssetId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            bool isDeleted = await service.DeleteAsync(TestAssetId);

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
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await dbContext.Assets.AddAsync(asset);

            Asset sameUserAsset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            sameUserAsset.Id = TestSameUserAssetId;

            await dbContext.Assets.AddAsync(sameUserAsset);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            await dbContext.Citizens.AddAsync(otherCitizen);
            MaterialState otherMaterialState = new MaterialState() { Citizen = otherCitizen };
            otherMaterialState.Id = TestOtherMaterialStateId;
            await dbContext.MaterialStates.AddAsync(otherMaterialState);
            await dbContext.SaveChangesAsync();

            Asset otherAsset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = otherMaterialState,
                MaterialStateId = TestOtherMaterialStateId
            };
            otherAsset.Id = TestOtherAssetId;

            await dbContext.Assets.AddAsync(otherAsset);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
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
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await dbContext.Assets.AddAsync(asset);

            Asset sameUserAsset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            sameUserAsset.Id = TestSameUserAssetId;

            await dbContext.Assets.AddAsync(sameUserAsset);            

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
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
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await dbContext.Assets.AddAsync(asset);

            Asset sameUserAsset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            sameUserAsset.Id = TestSameUserAssetId;

            await dbContext.Assets.AddAsync(sameUserAsset);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            await dbContext.Citizens.AddAsync(otherCitizen);
            MaterialState otherMaterialState = new MaterialState() { Citizen = otherCitizen };
            otherMaterialState.Id = TestOtherMaterialStateId;
            await dbContext.MaterialStates.AddAsync(otherMaterialState);
            await dbContext.SaveChangesAsync();

            Asset otherAsset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestOtherMaterialStateId
            };
            otherAsset.Id = TestOtherAssetId;

            await dbContext.Assets.AddAsync(otherAsset);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            IEnumerable <AssetViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (AssetViewModel viewModel in viewModels)
            {
                Assert.IsType<AssetViewModel>(viewModel);
                Assert.Equal(TestName, viewModel.Name);
                Assert.Equal(TestDescription, viewModel.Description);
                Assert.Equal(TestWorthiness, viewModel.Worthiness);
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
            MaterialState materialState = new MaterialState() { Citizen = citizen };
            materialState.Id = TestMaterialStateId;
            await dbContext.MaterialStates.AddAsync(materialState);
            await dbContext.SaveChangesAsync();

            Asset asset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            asset.Id = TestAssetId;

            await dbContext.Assets.AddAsync(asset);

            Asset sameUserAsset = new Asset()
            {
                Name = TestName,
                Description = TestDescription,
                Worthiness = TestWorthiness,
                MaterialState = materialState,
                MaterialStateId = TestMaterialStateId
            };
            sameUserAsset.Id = TestSameUserAssetId;

            await dbContext.Assets.AddAsync(sameUserAsset);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);
            IEnumerable<AssetViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);

            AssetsService service = new AssetsService(this.mapper, assetsRepository, materialStatesRepository);            
            IEnumerable<AssetViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}