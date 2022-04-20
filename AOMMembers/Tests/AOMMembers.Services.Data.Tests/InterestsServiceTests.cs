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
using AOMMembers.Web.ViewModels.Interests;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Tests
{
    public class InterestsServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestInexistantUserId = "TestInexistantUserId";
        private const string TestOtherUserId = "TestOtherUserId";
        private const string TestInterestId = "TestInterestId";
        private const string TestInexistantInterestId = "TestInexistantInterestId";
        private const string TestSameUserInterestId = "TestSameUserInterestId";
        private const string TestOtherInterestId = "TestOtherInterestId";
        private const string TestDescription = "TestDescription";
        private const string TestEditedDescription = "TestEditedDescription";
        private const string TestWorldviewId = "TestWorldviewId";
        private const string TestOtherWorldviewId = "TestOtherWorldviewId";

        private readonly IMapper mapper;

        public InterestsServiceTests(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Fact]
        public async Task CreateAsyncAddCorrectlyToRepositiry()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            InterestInputModel inputModel = new InterestInputModel()
            {
                Description = TestDescription
            };

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(1, dbContext.Interests.Count());
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            InterestInputModel inputModel = new InterestInputModel()
            {
                Description = TestDescription
            };

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Interest interest = dbContext.Interests.FirstOrDefault();

            Assert.NotNull(interest);
            Assert.IsType<Interest>(interest);
        }

        [Fact]
        public async Task CreateAsyncCretesCorrectlyWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            InterestInputModel inputModel = new InterestInputModel()
            {
                Description = TestDescription
            };

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            await service.CreateAsync(inputModel, TestUserId);

            Interest interest = dbContext.Interests.First();
            
            Assert.Equal(TestDescription, interest.Description);
            Assert.Equal(TestWorldviewId, interest.WorldviewId);
        }

        [Fact]
        public async Task CreateAsyncReturnsCretedId()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            InterestInputModel inputModel = new InterestInputModel()
            {
                Description = TestDescription
            };

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            string id = await service.CreateAsync(inputModel, TestUserId);

            Interest interest = dbContext.Interests.First();

            Assert.Equal(interest.Id, id);
        }

        [Fact]
        public async Task CreateAsyncReturnsBadResultIfNoParrent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestInputModel inputModel = new InterestInputModel()
            {
                Description = TestDescription
            };

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            string badResult = await service.CreateAsync(inputModel, TestUserId);

            Assert.Equal(InterestCreateWithoutWorldviewBadResult, badResult);
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
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            InterestInputModel inputModel = new InterestInputModel()
            {
                Description = TestDescription
            };

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            string badResult = await service.CreateAsync(inputModel, inexistantUserId);

            Assert.Equal(InterestCreateWithoutWorldviewBadResult, badResult);
        }

        [Fact]
        public async Task IsAbsentReturnsFalseIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Interest interest = new Interest()
            {
                Description = TestDescription,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isAbsent = await service.IsAbsent(TestInterestId);

            Assert.False(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isAbsent = await service.IsAbsent(TestInterestId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task IsAbsentReturnsTrueIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Interest interest = new Interest()
            {
                Description = TestDescription,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isAbsent = await service.IsAbsent(TestInexistantInterestId);

            Assert.True(isAbsent);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Interest interest = new Interest()
            {
                Description = TestDescription,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            InterestViewModel viewModel = await service.GetViewModelByIdAsync(TestInterestId);

            Assert.NotNull(viewModel);
            Assert.IsType<InterestViewModel>(viewModel);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncReturnsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Interest interest = new Interest()
            {
                Description = TestDescription,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            InterestViewModel viewModel = await service.GetViewModelByIdAsync(TestInterestId);
            
            Assert.Equal(TestDescription, viewModel.Description);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Interest interest = new Interest()
            {
                Description = TestDescription,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            InterestDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestInterestId);

            Assert.NotNull(detailsViewModel);
            Assert.IsType<InterestDetailsViewModel>(detailsViewModel);
        }

        [Fact]
        public async Task GetDetailsByIdAsyncReturnsDetailsViewModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Interest interest = new Interest()
            {
                Description = TestDescription,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            InterestDetailsViewModel detailsViewModel = await service.GetDetailsByIdAsync(TestInterestId);
            
            Assert.Equal(TestDescription, detailsViewModel.Description);
            Assert.Equal(TestWorldviewId, detailsViewModel.WorldviewId);
        }

        [Fact]
        public async Task IsFromMemberReturnsTrueIfExistentAndIsFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isFromMember = await service.IsFromMember(TestInterestId, TestUserId);

            Assert.True(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isFromMember = await service.IsFromMember(TestInterestId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantInterestId, TestUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isFromMember = await service.IsFromMember(TestInterestId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task IsFromMemberReturnsFalseIfInexistantAndIsNotFromMember()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isFromMember = await service.IsFromMember(TestInexistantInterestId, TestInexistantUserId);

            Assert.False(isFromMember);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Interest interest = new Interest()
            {
                Description = TestDescription,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            InterestEditModel editModel = await service.GetEditModelByIdAsync(TestInterestId);

            Assert.NotNull(editModel);
            Assert.IsType<InterestEditModel>(editModel);
        }

        [Fact]
        public async Task GetEditModelByIdAsyncReturnsEditModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Interest interest = new Interest()
            {
                Description = TestDescription,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            InterestEditModel editModel = await service.GetEditModelByIdAsync(TestInterestId);
            
            Assert.Equal(TestDescription, editModel.Description);
        }

        [Fact]
        public async Task EditAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestEditModel editModel = new InterestEditModel()
            {
                Description = TestEditedDescription
            };

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isEdited = await service.EditAsync(TestInterestId, editModel);

            Assert.True(isEdited);
        }

        [Fact]
        public async Task EditAsyncEditCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestEditModel editModel = new InterestEditModel()
            {
                Description = TestEditedDescription
            };

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isEdited = await service.EditAsync(TestInterestId, editModel);

            Interest editedInterest = dbContext.Interests.First();
            
            Assert.Equal(TestEditedDescription, editedInterest.Description);
            Assert.Equal(TestWorldviewId, editedInterest.WorldviewId);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestEditModel editModel = new InterestEditModel()
            {
                Description = TestEditedDescription
            };

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isEdited = await service.EditAsync(TestInexistantInterestId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task EditAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestEditModel editModel = new InterestEditModel()
            {
                Description = TestEditedDescription
            };

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isEdited = await service.EditAsync(TestInterestId, editModel);

            Assert.False(isEdited);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModel()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Interest interest = new Interest()
            {
                Description = TestDescription,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            InterestDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestInterestId);

            Assert.NotNull(deleteModel);
            Assert.IsType<InterestDeleteModel>(deleteModel);
        }

        [Fact]
        public async Task GetDeleteModelByIdAsyncReturnsDeleteModelWithGivenParameters()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Interest interest = new Interest()
            {
                Description = TestDescription,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            InterestDeleteModel deleteModel = await service.GetDeleteModelByIdAsync(TestInterestId);
            
            Assert.Equal(TestDescription, deleteModel.Description);
        }

        [Fact]
        public async Task DeleteAsyncReturnsTrueIfExistent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isDeleted = await service.DeleteAsync(TestInterestId);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncDeleteCorrectly()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isDeleted = await service.DeleteAsync(TestInterestId);

            Interest deleted = dbContext.Interests.FirstOrDefault(i => i.IsDeleted == false);

            Assert.Null(deleted);

            Interest deletedSoft = dbContext.Interests.FirstOrDefault(i => i.IsDeleted == true);
            Assert.NotNull(deletedSoft);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfInexistant()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            Member member = new Member() { ApplicationUserId = TestUserId };
            await dbContext.Members.AddAsync(member);
            Citizen citizen = new Citizen() { Member = member };
            await dbContext.Citizens.AddAsync(citizen);
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await interestsRepository.AddAsync(interest);
            await interestsRepository.SaveChangesAsync();

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isDeleted = await service.DeleteAsync(TestInexistantInterestId);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncReturnsFalseIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            bool isDeleted = await service.DeleteAsync(TestInterestId);

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
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await dbContext.Interests.AddAsync(interest);

            Interest sameUserInterest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            sameUserInterest.Id = TestSameUserInterestId;

            await dbContext.Interests.AddAsync(sameUserInterest);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            await dbContext.Citizens.AddAsync(otherCitizen);
            Worldview otherWorldview = new Worldview() { Citizen = otherCitizen };
            otherWorldview.Id = TestOtherWorldviewId;
            await dbContext.Worldviews.AddAsync(otherWorldview);
            await dbContext.SaveChangesAsync();

            Interest otherInterest = new Interest()
            {
                Description = TestDescription,
                Worldview = otherWorldview,
                WorldviewId = TestOtherWorldviewId
            };
            otherInterest.Id = TestOtherInterestId;

            await dbContext.Interests.AddAsync(otherInterest);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
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
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await dbContext.Interests.AddAsync(interest);

            Interest sameUserInterest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            sameUserInterest.Id = TestSameUserInterestId;

            await dbContext.Interests.AddAsync(sameUserInterest);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            int count = service.GetCountFromMember(TestOtherUserId);

            Assert.Equal(0, count);
        }

        [Fact]
        public void GetCountFromMemberReturns0IfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
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
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await dbContext.Interests.AddAsync(interest);

            Interest sameUserInterest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            sameUserInterest.Id = TestSameUserInterestId;

            await dbContext.Interests.AddAsync(sameUserInterest);

            Member otherMember = new Member() { ApplicationUserId = TestOtherUserId };
            await dbContext.Members.AddAsync(otherMember);
            Citizen otherCitizen = new Citizen() { Member = otherMember };
            await dbContext.Citizens.AddAsync(otherCitizen);
            Worldview otherWorldview = new Worldview() { Citizen = otherCitizen };
            otherWorldview.Id = TestOtherWorldviewId;
            await dbContext.Worldviews.AddAsync(otherWorldview);
            await dbContext.SaveChangesAsync();

            Interest otherInterest = new Interest()
            {
                Description = TestDescription,
                Worldview = otherWorldview,
                WorldviewId = TestOtherWorldviewId
            };
            otherInterest.Id = TestOtherInterestId;

            await dbContext.Interests.AddAsync(otherInterest);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            IEnumerable<InterestViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Equal(2, viewModels.Count());
            foreach (InterestViewModel viewModel in viewModels)
            {
                Assert.IsType<InterestViewModel>(viewModel);
                Assert.Equal(TestDescription, viewModel.Description);
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
            Worldview worldview = new Worldview() { Citizen = citizen };
            worldview.Id = TestWorldviewId;
            await dbContext.Worldviews.AddAsync(worldview);
            await dbContext.SaveChangesAsync();

            Interest interest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            interest.Id = TestInterestId;

            await dbContext.Interests.AddAsync(interest);

            Interest sameUserInterest = new Interest()
            {
                Description = TestDescription,
                Worldview = worldview,
                WorldviewId = TestWorldviewId
            };
            sameUserInterest.Id = TestSameUserInterestId;

            await dbContext.Interests.AddAsync(sameUserInterest);

            await dbContext.SaveChangesAsync();

            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            IEnumerable<InterestViewModel> viewModels = service.GetAllFromMember(TestOtherUserId);

            Assert.Null(viewModels);
        }

        [Fact]
        public void GetCountFromMemberReturnsNullIfEmptyCollection()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);

            InterestsService service = new InterestsService(this.mapper, interestsRepository, worldviewsRepository);
            IEnumerable<InterestViewModel> viewModels = service.GetAllFromMember(TestUserId);

            Assert.Null(viewModels);
        }
    }
}