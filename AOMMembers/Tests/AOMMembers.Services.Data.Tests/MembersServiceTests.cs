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
using AOMMembers.Web.ViewModels.Members;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class MembersServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestMemberId = "TestMemberId";
        private const string TestInexistantMemberId = "TestInexistantMemberId";
        private const string TestOtherMemberId = "TestOtherMemberId";
        private const string TestFullName = "TestFullName";
        private const string TestEditedFullName = "TestEditedFullName";
        private const string TestEmail = "TestEmail";
        private const string TestEditedEmail = "TestEditedEmail";
        private const string TestPhoneNumber = "TestPhoneNumber";
        private const string TestEditedPhoneNumber = "TestEditedPhoneNumber";
        private const string TestPictureUrl = "TestPictureUrl";
        private const string TestEditedPictureUrl = "TestEditedPictureUrl";

        private readonly IMapper mapper;

        public MembersServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task IsCreatedReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);            
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,                
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isCreated = service.IsCreated(TestUserId);

            Assert.True(isCreated);
        }

        [Fact]
        public async Task IsCreatedReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isCreated = service.IsCreated(TestInexistantUserId);

            Assert.False(isCreated);
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            MemberInputModel inputModel = new MemberInputModel()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl
            };

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.Members.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            MemberInputModel inputModel = new MemberInputModel()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl
            };

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Member member = dbContext.Members.FirstOrDefault();

            Assert.NotNull(member);
            Assert.IsType<Member>(member);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            MemberInputModel inputModel = new MemberInputModel()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl
            };

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Member member = dbContext.Members.First();

            Assert.Equal(TestFullName, member.FullName);
            Assert.Equal(TestEmail, member.Email);
            Assert.Equal(TestPhoneNumber, member.PhoneNumber);
            Assert.Equal(TestPictureUrl, member.PictureUrl);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            MemberInputModel inputModel = new MemberInputModel()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl
            };

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            Member member = dbContext.Members.First();

            Assert.Equal(member.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            MemberInputModel inputModel = new MemberInputModel()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl
            };

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(MemberCreateWithoutApplicationUserBadResult, badResult);
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
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            MemberInputModel inputModel = new MemberInputModel()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl
            };

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(MemberCreateWithoutApplicationUserBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isAbsent = await service.IsAbsent(TestMemberId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantMemberId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelsReturnsViewModels()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);

            Member otherMember = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestOtherUserId
            };
            member.Id = TestOtherMemberId;

            await membersRepository.AddAsync(otherMember);

            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            IEnumerable<MemberViewModel> viewModels = service.GetViewModels();

            Assert.Equal(2, viewModels.Count());
            foreach (MemberViewModel viewModel in viewModels)
            {
                Assert.IsType<MemberViewModel>(viewModel);
            }
        }

        [Fact]
        public async Task GetViewModelsReturnsViewModelsWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);

            Member otherMember = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestOtherUserId
            };
            member.Id = TestOtherMemberId;

            await membersRepository.AddAsync(otherMember);

            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            IEnumerable<MemberViewModel> viewModels = service.GetViewModels();
            
            foreach (MemberViewModel viewModel in viewModels)
            {
                Assert.Equal(TestFullName, viewModel.FullName);
                Assert.Equal(TestEmail, viewModel.Email);
                Assert.Equal(TestPhoneNumber, viewModel.PhoneNumber);
                Assert.Equal(TestPictureUrl, viewModel.PictureUrl);                
            }
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            MemberDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestMemberId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<MemberDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            MemberDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestMemberId);

            Assert.Equal(TestFullName, detailsViewModel.FullName);
            Assert.Equal(TestEmail, detailsViewModel.Email);
            Assert.Equal(TestPhoneNumber, detailsViewModel.PhoneNumber);
            Assert.Equal(TestPictureUrl, detailsViewModel.PictureUrl);            
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isFromMember = await service.IsFromMember(TestMemberId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantMemberId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isFromMember = await service.IsFromMember(TestMemberId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantMemberId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            MemberEditModel editModel = await service.GetEditModelByIdAsync(TestMemberId);

            Assert.NotNull(editModel);
            Assert.IsType<MemberEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            MemberEditModel editModel = await service.GetEditModelByIdAsync(TestMemberId);

            Assert.Equal(TestFullName, editModel.FullName);
            Assert.Equal(TestEmail, editModel.Email);
            Assert.Equal(TestPhoneNumber, editModel.PhoneNumber);
            Assert.Equal(TestPictureUrl, editModel.PictureUrl);            
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MemberEditModel editModel = new MemberEditModel()
            {
                FullName = TestEditedFullName,
                Email = TestEditedEmail,
                PhoneNumber = TestEditedPhoneNumber,
                PictureUrl = TestEditedPictureUrl                
            };

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isEdited = await service.EditAsync(TestMemberId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MemberEditModel editModel = new MemberEditModel()
            {
                FullName = TestEditedFullName,
                Email = TestEditedEmail,
                PhoneNumber = TestEditedPhoneNumber,
                PictureUrl = TestEditedPictureUrl
            };

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isEdited = await service.EditAsync(TestMemberId, editModel);

            Member editedMember = dbContext.Members.First();

            Assert.Equal(TestEditedFullName, editedMember.FullName);
            Assert.Equal(TestEditedEmail, editedMember.Email);
            Assert.Equal(TestEditedPhoneNumber, editedMember.PhoneNumber);
            Assert.Equal(TestPictureUrl, editedMember.PictureUrl);            
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MemberEditModel editModel = new MemberEditModel()
            {
                FullName = TestEditedFullName,
                Email = TestEditedEmail,
                PhoneNumber = TestEditedPhoneNumber,
                PictureUrl = TestEditedPictureUrl
            };

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isEdited = await service.EditAsync(TestInexistantMemberId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            MemberDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestMemberId);

            Assert.NotNull(deleteModel);
            Assert.IsType<MemberDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {

            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            MemberDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestMemberId);

            Assert.Equal(TestFullName, deleteModel.FullName);
            Assert.Equal(TestEmail, deleteModel.Email);
            Assert.Equal(TestPhoneNumber, deleteModel.PhoneNumber);
            Assert.Equal(TestPictureUrl, deleteModel.PictureUrl);            
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isDeleted = await service.DeleteAsync(TestMemberId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isDeleted = await service.DeleteAsync(TestMemberId);

            Member deleted = dbContext.Members.FirstOrDefault(c => c.IsDeleted == false);

            Assert.Null(deleted);

            Member deletedSoft = dbContext.Members.FirstOrDefault(c => c.IsDeleted == true);

            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);

            Member member = new Member()
            {
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                ApplicationUserId = TestUserId
            };
            member.Id = TestMemberId;

            await membersRepository.AddAsync(member);
            await membersRepository.SaveChangesAsync();

            MembersService service = new MembersService(this.mapper, membersRepository, publicImagesRepository, mediaMaterialRepository, relationshipsRepository, partyPositionsRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantUserId);

            Assert.False(isDeleted);
        }
    }
}