using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using AOMMembers.Data;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Data.Repositories;
using AOMMembers.Services.Data.Services;
using AOMMembers.Web.ViewModels.Administration.ApplicationUsers;

namespace AOMMembers.Services.Data.Tests
{
    public class ApplicationUsersServiceTests
    {
        private const string TestUserId = "TestUserId";
        private const string TestOtherUserId = "TestOtherUserId";       
        private const string TestMemberId = "TestMemberId";        
        private const string TestOtherMemberId = "TestOtherMemberId";
        private const string TestFullName = "Test Full Name";
        private const string TestEmail = "TestEmail@abv.bg";
        private const string TestOtherEmail = "TestOtherEmail@abv.bg";
        private const string TestPhoneNumber = "TestPhoneNumber";
        private const string TestPictureUrl = "TestPictureUrl";

        [Fact]
        public async Task GetApplicationUserByIdReturnsExpectedUser()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<ApplicationUser> applicationUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            ApplicationUser applicationUser = new ApplicationUser()
            {
                Email = TestEmail,
                MemberId = TestMemberId
            };
            applicationUser.Id = TestUserId;

            await applicationUsersRepository.AddAsync(applicationUser);

            ApplicationUser otherApplicationUser = new ApplicationUser()
            {
                Email = TestOtherEmail,
                MemberId = TestOtherMemberId
            };
            otherApplicationUser.Id = TestOtherUserId;

            await applicationUsersRepository.AddAsync(otherApplicationUser);

            await applicationUsersRepository.SaveChangesAsync();

            ApplicationUsersService service = new ApplicationUsersService(applicationUsersRepository, membersRepository, citizensRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, relationshipsRepository, partyPositionsRepository, partyMembershipsRepository, materialStatesRepository, assetsRepository, publicImagesRepository, mediaMaterialsRepository, lawStatesRepository, lawProblemsRepository, societyHelpsRepository, societyActivitiesRepository, worldviewsRepository, interestsRepository, settingsRepository);
            ApplicationUser dbApplicationUser = await service.GetApplicationUserById(TestUserId);

            Assert.NotNull(dbApplicationUser);
            Assert.Equal(TestUserId, dbApplicationUser.Id);
            Assert.Equal(TestEmail, dbApplicationUser.Email);
            Assert.Equal(TestMemberId, dbApplicationUser.MemberId);
        }

        [Fact]
        public async Task GetUsersReturnsAllUsersInApplicationUserViewModels()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<ApplicationUser> applicationUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            ApplicationUser applicationUser = new ApplicationUser()
            {
                Email = TestEmail,
                MemberId = TestMemberId
            };
            applicationUser.Id = TestUserId;

            await applicationUsersRepository.AddAsync(applicationUser);

            ApplicationUser otherApplicationUser = new ApplicationUser()
            {
                Email = TestOtherEmail,
                MemberId = TestOtherMemberId
            };
            otherApplicationUser.Id = TestOtherUserId;

            await applicationUsersRepository.AddAsync(otherApplicationUser);

            await applicationUsersRepository.SaveChangesAsync();

            ApplicationUsersService service = new ApplicationUsersService(applicationUsersRepository, membersRepository, citizensRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, relationshipsRepository, partyPositionsRepository, partyMembershipsRepository, materialStatesRepository, assetsRepository, publicImagesRepository, mediaMaterialsRepository, lawStatesRepository, lawProblemsRepository, societyHelpsRepository, societyActivitiesRepository, worldviewsRepository, interestsRepository, settingsRepository);
            IEnumerable<ApplicationUserViewModel> applicationUserViewModels = await service.GetUsers();

            Assert.NotNull(applicationUserViewModels);

            int counter = 0;
            foreach (ApplicationUserViewModel applicationUserViewModel in applicationUserViewModels)
            {
                Assert.NotNull(applicationUserViewModels);
                Assert.IsType<ApplicationUserViewModel>(applicationUserViewModel);
                if (counter == 0)
                {
                    Assert.Equal(TestUserId, applicationUserViewModel.Id);
                    Assert.Equal(TestEmail, applicationUserViewModel.Email);
                }
                else if (counter == 1)
                {
                    Assert.Equal(TestOtherUserId, applicationUserViewModel.Id);
                    Assert.Equal(TestOtherEmail, applicationUserViewModel.Email);
                }

                counter++;
            }
        }

        [Fact]
        public async Task RestoreDeletedAsyncRestoresDeleted()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            EfDeletableEntityRepository<ApplicationUser> applicationUsersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            EfDeletableEntityRepository<Member> membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            EfDeletableEntityRepository<Citizen> citizensRepository = new EfDeletableEntityRepository<Citizen>(dbContext);
            EfDeletableEntityRepository<Education> educationsRepository = new EfDeletableEntityRepository<Education>(dbContext);
            EfDeletableEntityRepository<Qualification> qualificationsRepository = new EfDeletableEntityRepository<Qualification>(dbContext);
            EfDeletableEntityRepository<Career> careersRepository = new EfDeletableEntityRepository<Career>(dbContext);
            EfDeletableEntityRepository<WorkPosition> workPositionsRepository = new EfDeletableEntityRepository<WorkPosition>(dbContext);
            EfDeletableEntityRepository<Relationship> relationshipsRepository = new EfDeletableEntityRepository<Relationship>(dbContext);
            EfDeletableEntityRepository<PartyPosition> partyPositionsRepository = new EfDeletableEntityRepository<PartyPosition>(dbContext);
            EfDeletableEntityRepository<PartyMembership> partyMembershipsRepository = new EfDeletableEntityRepository<PartyMembership>(dbContext);
            EfDeletableEntityRepository<MaterialState> materialStatesRepository = new EfDeletableEntityRepository<MaterialState>(dbContext);
            EfDeletableEntityRepository<Asset> assetsRepository = new EfDeletableEntityRepository<Asset>(dbContext);
            EfDeletableEntityRepository<PublicImage> publicImagesRepository = new EfDeletableEntityRepository<PublicImage>(dbContext);
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialsRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);

            ApplicationUser applicationUser = new ApplicationUser()
            {
                Email = TestEmail,
                MemberId = TestMemberId
            };
            applicationUser.Id = TestUserId;

            await applicationUsersRepository.AddAsync(applicationUser);
            await applicationUsersRepository.SaveChangesAsync();

            Member member = new Member()
            {
                ApplicationUserId = TestUserId,
                FullName = TestFullName,
                Email = TestEmail,
                PhoneNumber = TestPhoneNumber,
                PictureUrl = TestPictureUrl,
                IsDeleted = true
            };
            member.Id = TestMemberId;

            await dbContext.Members.AddAsync(member);            
            await dbContext.SaveChangesAsync();

            ApplicationUsersService service = new ApplicationUsersService(applicationUsersRepository, membersRepository, citizensRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, relationshipsRepository, partyPositionsRepository, partyMembershipsRepository, materialStatesRepository, assetsRepository, publicImagesRepository, mediaMaterialsRepository, lawStatesRepository, lawProblemsRepository, societyHelpsRepository, societyActivitiesRepository, worldviewsRepository, interestsRepository, settingsRepository);
            await service.RestoreDeletedAsync(TestUserId);

            Member restoredMember = await membersRepository.GetByIdAsync(TestUserId);

            Assert.NotNull(restoredMember);
            Assert.False(restoredMember.IsDeleted);
            Assert.Equal(TestUserId, restoredMember.ApplicationUserId);
            Assert.Equal(TestFullName, restoredMember.FullName);
            Assert.Equal(TestEmail, restoredMember.Email);
            Assert.Equal(TestPhoneNumber, restoredMember.PhoneNumber);
            Assert.Equal(TestPictureUrl, restoredMember.PictureUrl);
        }
    }
}