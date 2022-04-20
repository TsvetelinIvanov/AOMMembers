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
using AOMMembers.Web.ViewModels.MediaMaterials;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class MediaMaterialsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestMediaMaterialId = "TestMediaMaterialId";
        private const string TestInexistantMediaMaterialId = "TestInexistantMediaMaterialId";
        private const string TestSameUserMediaMaterialId = "TestSameUserMediaMaterialId";
        private const string TestOtherMediaMaterialId = "TestOtherMediaMaterialId";
        private const string TestKind = "TestKind";
        private const string TestEditedKind = "TestEditedKind";
        private const string TestMediaName = "TestMediaName";
        private const string TestEditedMediaName = "TestEditedMediaName";
        private readonly DateTime TestIssueDate = new DateTime(2019, 12, 9);
        private readonly DateTime TestEditedIssueDate = new DateTime(2019, 12, 10);
        private const string TestAuthor = "TestAuthor";
        private const string TestEditedAuthor = "TestEditedAuthor";
        private const string TestHeading = "TestHeading";
        private const string TestEditedHeading = "TestEditedHeading";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const string TestMediaMaterialLink = "TestMediaMaterialLink";
        private const string TestEditedMediaMaterialLink = "TestEditedMediaMaterialLink";
        private const string TestPublicImageId = "TestPublicImageId";
        private const string TestOtherPublicImageId = "TestOtherPublicImageId";

        private readonly IMapper mapper;

        public MediaMaterialsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterialInputModel inputModel = new MediaMaterialInputModel()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink
            };

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.MediaMaterials.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterialInputModel inputModel = new MediaMaterialInputModel()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink
            };

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            await service.CreateAsync(inputModel, TestUserId);

            MediaMaterial mediaMaterial = dbContext.MediaMaterials.FirstOrDefault();

            Assert.NotNull(mediaMaterial);
            Assert.IsType<MediaMaterial>(mediaMaterial);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterialInputModel inputModel = new MediaMaterialInputModel()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink
            };

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            await service.CreateAsync(inputModel, TestUserId);

            MediaMaterial mediaMaterial = dbContext.MediaMaterials.First();

            Assert.Equal(TestKind, mediaMaterial.Kind);
            Assert.Equal(TestMediaName, mediaMaterial.MediaName);
            Assert.Equal(TestIssueDate, mediaMaterial.IssueDate);
            Assert.Equal(TestAuthor, mediaMaterial.Author);
            Assert.Equal(TestHeading, mediaMaterial.Heading);
            Assert.Equal(TestDescription, mediaMaterial.Description);
            Assert.Equal(TestMediaMaterialLink, mediaMaterial.MediaMaterialLink);
            Assert.Equal(TestPublicImageId, mediaMaterial.PublicImageId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterialInputModel inputModel = new MediaMaterialInputModel()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink
            };

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            MediaMaterial mediaMaterial = dbContext.MediaMaterials.First();

            Assert.Equal(mediaMaterial.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialInputModel inputModel = new MediaMaterialInputModel()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink
            };

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(MediaMaterialCreateWithoutPublicImageBadResult, badResult);
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
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterialInputModel inputModel = new MediaMaterialInputModel()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink
            };

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(MediaMaterialCreateWithoutPublicImageBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isAbsent = await service.IsAbsent(TestMediaMaterialId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isAbsent = await service.IsAbsent(TestMediaMaterialId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantMediaMaterialId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            MediaMaterialViewModel viewModel = await service.GetViewModelByIdAsync(TestMediaMaterialId);

            Assert.NotNull(viewModel);
            Assert.IsType<MediaMaterialViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            MediaMaterialViewModel viewModel = await service.GetViewModelByIdAsync(TestMediaMaterialId);

            Assert.Equal(TestKind, viewModel.Kind);
            Assert.Equal(TestMediaName, viewModel.MediaName);
            Assert.Equal(TestIssueDate, viewModel.IssueDate);
            Assert.Equal(TestAuthor, viewModel.Author);
            Assert.Equal(TestHeading, viewModel.Heading);
            Assert.Equal(TestDescription, viewModel.Description);
            Assert.Equal(TestMediaMaterialLink, viewModel.MediaMaterialLink);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            MediaMaterialDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestMediaMaterialId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<MediaMaterialDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            MediaMaterialDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestMediaMaterialId);

            Assert.Equal(TestKind, detailsViewModel.Kind);
            Assert.Equal(TestMediaName, detailsViewModel.MediaName);
            Assert.Equal(TestIssueDate, detailsViewModel.IssueDate);
            Assert.Equal(TestAuthor, detailsViewModel.Author);
            Assert.Equal(TestHeading, detailsViewModel.Heading);
            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestMediaMaterialLink, detailsViewModel.MediaMaterialLink);
            Assert.Equal(TestPublicImageId, detailsViewModel.PublicImageId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isFromMember = await service.IsFromMember(TestMediaMaterialId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isFromMember = await service.IsFromMember(TestMediaMaterialId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantMediaMaterialId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isFromMember = await service.IsFromMember(TestMediaMaterialId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantMediaMaterialId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            MediaMaterialEditModel editModel = await service.GetEditModelByIdAsync(TestMediaMaterialId);

            Assert.NotNull(editModel);
            Assert.IsType<MediaMaterialEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            MediaMaterialEditModel editModel = await service.GetEditModelByIdAsync(TestMediaMaterialId);

            Assert.Equal(TestKind, editModel.Kind);
            Assert.Equal(TestMediaName, editModel.MediaName);
            Assert.Equal(TestIssueDate, editModel.IssueDate);
            Assert.Equal(TestAuthor, editModel.Author);
            Assert.Equal(TestHeading, editModel.Heading);
            Assert.Equal(TestDescription, editModel.Description);
            Assert.Equal(TestMediaMaterialLink, editModel.MediaMaterialLink);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialEditModel editModel = new MediaMaterialEditModel()
            {
                Kind = TestEditedKind,
                MediaName = TestEditedMediaName,
                IssueDate = TestEditedIssueDate,
                Author = TestEditedAuthor,
                Heading = TestEditedHeading,
                Description = TestEditedDescription,
                MediaMaterialLink = TestEditedMediaMaterialLink
            };

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isEdited = await service.EditAsync(TestMediaMaterialId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialEditModel editModel = new MediaMaterialEditModel()
            {
                Kind = TestEditedKind,
                MediaName = TestEditedMediaName,
                IssueDate = TestEditedIssueDate,
                Author = TestEditedAuthor,
                Heading = TestEditedHeading,
                Description = TestEditedDescription,
                MediaMaterialLink = TestEditedMediaMaterialLink
            };

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isEdited = await service.EditAsync(TestMediaMaterialId, editModel);

            MediaMaterial editedMediaMaterial = dbContext.MediaMaterials.First();

            Assert.Equal(TestEditedKind, editedMediaMaterial.Kind);
            Assert.Equal(TestEditedMediaName, editedMediaMaterial.MediaName);
            Assert.Equal(TestEditedIssueDate, editedMediaMaterial.IssueDate);
            Assert.Equal(TestEditedAuthor, editedMediaMaterial.Author);
            Assert.Equal(TestEditedHeading, editedMediaMaterial.Heading);
            Assert.Equal(TestEditedDescription, editedMediaMaterial.Description);
            Assert.Equal(TestEditedMediaMaterialLink, editedMediaMaterial.MediaMaterialLink);
            Assert.Equal(TestPublicImageId, editedMediaMaterial.PublicImageId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialEditModel editModel = new MediaMaterialEditModel()
            {
                Kind = TestEditedKind,
                MediaName = TestEditedMediaName,
                IssueDate = TestEditedIssueDate,
                Author = TestEditedAuthor,
                Heading = TestEditedHeading,
                Description = TestEditedDescription,
                MediaMaterialLink = TestEditedMediaMaterialLink
            };

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isEdited = await service.EditAsync(TestInexistantMediaMaterialId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialEditModel editModel = new MediaMaterialEditModel()
            {
                Kind = TestEditedKind,
                MediaName = TestEditedMediaName,
                IssueDate = TestEditedIssueDate,
                Author = TestEditedAuthor,
                Heading = TestEditedHeading,
                Description = TestEditedDescription,
                MediaMaterialLink = TestEditedMediaMaterialLink
            };

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isEdited = await service.EditAsync(TestMediaMaterialId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            MediaMaterialDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestMediaMaterialId);

            Assert.NotNull(deleteModel);
            Assert.IsType<MediaMaterialDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            MediaMaterialDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestMediaMaterialId);

            Assert.Equal(TestKind, deleteModel.Kind);
            Assert.Equal(TestMediaName, deleteModel.MediaName);
            Assert.Equal(TestIssueDate, deleteModel.IssueDate);
            Assert.Equal(TestAuthor, deleteModel.Author);
            Assert.Equal(TestHeading, deleteModel.Heading);
            Assert.Equal(TestDescription, deleteModel.Description);
            Assert.Equal(TestMediaMaterialLink, deleteModel.MediaMaterialLink);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isDeleted = await service.DeleteAsync(TestMediaMaterialId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isDeleted = await service.DeleteAsync(TestMediaMaterialId);

            MediaMaterial deleted = dbContext.MediaMaterials.FirstOrDefault(mm => mm.IsDeleted == false);

            Assert.Null(deleted);

            MediaMaterial deletedSoft = dbContext.MediaMaterials.FirstOrDefault(mm => mm.IsDeleted == true);
            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await mediaMaterialsRepository.AddAsync(mediaMaterial);
            await mediaMaterialsRepository.SaveChangesAsync();

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantMediaMaterialId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            bool isDeleted = await service.DeleteAsync(TestMediaMaterialId);

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
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await dbContext.MediaMaterials.AddAsync(mediaMaterial);

            MediaMaterial sameUserMediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            sameUserMediaMaterial.Id = TestSameUserMediaMaterialId;

            await dbContext.MediaMaterials.AddAsync(sameUserMediaMaterial);

            Member otherMember = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(otherMember);
            PublicImage otherPublicImage = new PublicImage() { Member = otherMember };
            otherPublicImage.Id = TestOtherPublicImageId;
            await dbContext.PublicImages.AddAsync(otherPublicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial otherMediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = otherPublicImage,
                PublicImageId = TestOtherPublicImageId
            };
            mediaMaterial.Id = TestOtherMediaMaterialId;

            await dbContext.MediaMaterials.AddAsync(otherMediaMaterial);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
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
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await dbContext.MediaMaterials.AddAsync(mediaMaterial);

            MediaMaterial sameUserMediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            sameUserMediaMaterial.Id = TestSameUserMediaMaterialId;

            await dbContext.MediaMaterials.AddAsync(sameUserMediaMaterial);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
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
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await dbContext.MediaMaterials.AddAsync(mediaMaterial);

            MediaMaterial sameUserMediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            sameUserMediaMaterial.Id = TestSameUserMediaMaterialId;

            await dbContext.MediaMaterials.AddAsync(sameUserMediaMaterial);

            Member otherMember = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(otherMember);
            PublicImage otherPublicImage = new PublicImage() { Member = otherMember };
            otherPublicImage.Id = TestOtherPublicImageId;
            await dbContext.PublicImages.AddAsync(otherPublicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial otherMediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = otherPublicImage,
                PublicImageId = TestOtherPublicImageId
            };
            mediaMaterial.Id = TestOtherMediaMaterialId;

            await dbContext.MediaMaterials.AddAsync(otherMediaMaterial);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            IEnumerable<MediaMaterialViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (MediaMaterialViewModel viewModel in viewModels)
            {
                Assert.IsType<MediaMaterialViewModel>(viewModel);
                Assert.Equal(TestKind, viewModel.Kind);
                Assert.Equal(TestMediaName, viewModel.MediaName);
                Assert.Equal(TestIssueDate, viewModel.IssueDate);
                Assert.Equal(TestAuthor, viewModel.Author);
                Assert.Equal(TestHeading, viewModel.Heading);
                Assert.Equal(TestDescription, viewModel.Description);
                Assert.Equal(TestMediaMaterialLink, viewModel.MediaMaterialLink);
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
            PublicImage publicImage = new PublicImage() { Member = member };
            publicImage.Id = TestPublicImageId;
            await dbContext.PublicImages.AddAsync(publicImage);
            await dbContext.SaveChangesAsync();

            MediaMaterial mediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            mediaMaterial.Id = TestMediaMaterialId;

            await dbContext.MediaMaterials.AddAsync(mediaMaterial);

            MediaMaterial sameUserMediaMaterial = new MediaMaterial()
            {
                Kind = TestKind,
                MediaName = TestMediaName,
                IssueDate = TestIssueDate,
                Author = TestAuthor,
                Heading = TestHeading,
                Description = TestDescription,
                MediaMaterialLink = TestMediaMaterialLink,
                PublicImage = publicImage,
                PublicImageId = TestPublicImageId
            };
            sameUserMediaMaterial.Id = TestSameUserMediaMaterialId;

            await dbContext.MediaMaterials.AddAsync(sameUserMediaMaterial);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            IEnumerable<MediaMaterialViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImageRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);

            MediaMaterialsService service = new MediaMaterialsService(this.mapper, mediaMaterialsRepository, publicImageRepository);
            IEnumerable<MediaMaterialViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}