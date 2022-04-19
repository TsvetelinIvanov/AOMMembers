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
using AOMMembers.Web.ViewModels.PublicImages;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class PublicImagesServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestPublicImageId = "TestPublicImageId";
        private const string TestInexistantPublicImageId = "TestInexistantPublicImageId";
        private const int TestRating = 10;
        private const int TestEditedRating = 9;
        private const string TestMemberId = "TestMemberId";

        private readonly IMapper mapper;

        public PublicImagesServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task IsCreatedReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);            

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,                
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isCreated = service.IsCreated(TestUserId);

            Assert.True(isCreated);
        }

        [Fact]
        public async Task IsCreatedReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isCreated = service.IsCreated(TestInexistantUserId);

            Assert.False(isCreated);
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImageInputModel inputModel = new PublicImageInputModel()
            {
                Rating = TestRating
            };

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.PublicImages.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImageInputModel inputModel = new PublicImageInputModel()
            {
                Rating = TestRating
            };

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            PublicImage publicImage = dbContext.PublicImages.FirstOrDefault();

            Assert.NotNull(publicImage);
            Assert.IsType<PublicImage>(publicImage);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImageInputModel inputModel = new PublicImageInputModel()
            {
                Rating = TestRating
            };

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            PublicImage publicImage = dbContext.PublicImages.FirstOrDefault();

            Assert.Equal(TestRating, publicImage.Rating);            
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImageInputModel inputModel = new PublicImageInputModel()
            {
                Rating = TestRating
            };

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            PublicImage publicImage = dbContext.PublicImages.First();

            Assert.Equal(publicImage.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImageInputModel inputModel = new PublicImageInputModel()
            {
                Rating = TestRating
            };

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(PublicImageCreateWithoutMemberBadResult, badResult);
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
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImageInputModel inputModel = new PublicImageInputModel()
            {
                Rating = TestRating
            };

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(PublicImageCreateWithoutMemberBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isAbsent = await service.IsAbsent(TestPublicImageId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantPublicImageId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            PublicImageViewModel viewModel = await service.GetViewModelByIdAsync(TestPublicImageId);

            Assert.NotNull(viewModel);
            Assert.IsType<PublicImageViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            PublicImageViewModel viewModel = await service.GetViewModelByIdAsync(TestPublicImageId);

            Assert.Equal(TestRating, viewModel.Rating);            
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            PublicImageDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestPublicImageId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<PublicImageDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            PublicImageDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestPublicImageId);

            Assert.Equal(TestRating, detailsViewModel.Rating);            
            Assert.Equal(TestMemberId, detailsViewModel.MemberId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isFromMember = await service.IsFromMember(TestPublicImageId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantPublicImageId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isFromMember = await service.IsFromMember(TestPublicImageId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantPublicImageId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            PublicImageEditModel editModel = await service.GetEditModelByIdAsync(TestPublicImageId);

            Assert.NotNull(editModel);
            Assert.IsType<PublicImageEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            PublicImageEditModel editModel = await service.GetEditModelByIdAsync(TestPublicImageId);

            Assert.Equal(TestRating, editModel.Rating);            
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImageEditModel editModel = new PublicImageEditModel()
            {
                Rating = TestRating
            };

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isEdited = await service.EditAsync(TestPublicImageId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImageEditModel editModel = new PublicImageEditModel()
            {
                Rating = TestRating
            };

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isEdited = await service.EditAsync(TestPublicImageId, editModel);

            PublicImage editedPublicImage = dbContext.PublicImages.First();

            Assert.Equal(TestEditedRating, editedPublicImage.Rating);
            Assert.Equal(TestMemberId, editedPublicImage.MemberId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImageEditModel editModel = new PublicImageEditModel()
            {
                Rating = TestRating
            };

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isEdited = await service.EditAsync(TestInexistantPublicImageId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            PublicImageDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestPublicImageId);

            Assert.NotNull(deleteModel);
            Assert.IsType<PublicImageDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();

            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            PublicImageDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestPublicImageId);

            Assert.Equal(TestRating, deleteModel.Rating);            
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();


            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isDeleted = await service.DeleteAsync(TestPublicImageId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();


            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isDeleted = await service.DeleteAsync(TestPublicImageId);

            PublicImage deleted = dbContext.PublicImages.FirstOrDefault(pi => pi.IsDeleted == false);

            Assert.Null(deleted);

            PublicImage deletedSoft = dbContext.PublicImages.FirstOrDefault(pi => pi.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            member.Id = TestMemberId;
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();

            PublicImage publicImage = new PublicImage()
            {
                Rating = TestRating,
                Member = member,
                MemberId = TestMemberId
            };
            publicImage.Id = TestPublicImageId;

            await publicImagesRepository.AddAsync(publicImage);
            await publicImagesRepository.SaveChangesAsync();


            PublicImagesService service = new PublicImagesService(this.mapper, publicImagesRepository, membersRepository, mediaMaterialsRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantPublicImageId);

            Assert.False(isDeleted);
        }
    }
}