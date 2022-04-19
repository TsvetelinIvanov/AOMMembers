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
using AOMMembers.Web.ViewModels.Administration.Dashboard;

namespace AOMMembers.Services.Data.Tests
{
    public class DashboardServiceTests
    {
        private const int EntitiesCount = 3;

        [Fact]
        public async Task GetIndexViewModelReturnsIndexViewModel()
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
            EfDeletableEntityRepository<MediaMaterial> mediaMaterialImagesRepository = new EfDeletableEntityRepository<MediaMaterial>(dbContext);
            EfDeletableEntityRepository<LawState> lawStatesRepository = new EfDeletableEntityRepository<LawState>(dbContext);
            EfDeletableEntityRepository<LawProblem> lawProblemsRepository = new EfDeletableEntityRepository<LawProblem>(dbContext);
            EfDeletableEntityRepository<SocietyHelp> societyHelpsRepository = new EfDeletableEntityRepository<SocietyHelp>(dbContext);
            EfDeletableEntityRepository<SocietyActivity> societyActivitiesRepository = new EfDeletableEntityRepository<SocietyActivity>(dbContext);
            EfDeletableEntityRepository<Worldview> worldviewsRepository = new EfDeletableEntityRepository<Worldview>(dbContext);
            EfDeletableEntityRepository<Interest> interestsRepository = new EfDeletableEntityRepository<Interest>(dbContext);
            EfDeletableEntityRepository<Setting> settingsRepository = new EfDeletableEntityRepository<Setting>(dbContext);            

            await applicationUsersRepository.AddAsync(new ApplicationUser());
            await applicationUsersRepository.AddAsync(new ApplicationUser());
            await applicationUsersRepository.AddAsync(new ApplicationUser());
            await applicationUsersRepository.SaveChangesAsync();

            await membersRepository.AddAsync(new Member());
            await membersRepository.AddAsync(new Member());
            await membersRepository.AddAsync(new Member());
            await membersRepository.SaveChangesAsync();

            await citizensRepository.AddAsync(new Citizen());
            await citizensRepository.AddAsync(new Citizen());
            await citizensRepository.AddAsync(new Citizen());
            await citizensRepository.SaveChangesAsync();

            await educationsRepository.AddAsync(new Education());
            await educationsRepository.AddAsync(new Education());
            await educationsRepository.AddAsync(new Education());
            await educationsRepository.SaveChangesAsync();

            await qualificationsRepository.AddAsync(new Qualification());
            await qualificationsRepository.AddAsync(new Qualification());
            await qualificationsRepository.AddAsync(new Qualification());
            await qualificationsRepository.SaveChangesAsync();

            await careersRepository.AddAsync(new Career());
            await careersRepository.AddAsync(new Career());
            await careersRepository.AddAsync(new Career());
            await careersRepository.SaveChangesAsync();

            await workPositionsRepository.AddAsync(new WorkPosition());
            await workPositionsRepository.AddAsync(new WorkPosition());
            await workPositionsRepository.AddAsync(new WorkPosition());
            await workPositionsRepository.SaveChangesAsync();

            await relationshipsRepository.AddAsync(new Relationship());
            await relationshipsRepository.AddAsync(new Relationship());
            await relationshipsRepository.AddAsync(new Relationship());
            await relationshipsRepository.SaveChangesAsync();

            await partyPositionsRepository.AddAsync(new PartyPosition());
            await partyPositionsRepository.AddAsync(new PartyPosition());
            await partyPositionsRepository.AddAsync(new PartyPosition());
            await partyPositionsRepository.SaveChangesAsync();

            await partyMembershipsRepository.AddAsync(new PartyMembership());
            await partyMembershipsRepository.AddAsync(new PartyMembership());
            await partyMembershipsRepository.AddAsync(new PartyMembership());
            await partyMembershipsRepository.SaveChangesAsync();

            await materialStatesRepository.AddAsync(new MaterialState());
            await materialStatesRepository.AddAsync(new MaterialState());
            await materialStatesRepository.AddAsync(new MaterialState());
            await materialStatesRepository.SaveChangesAsync();

            await assetsRepository.AddAsync(new Asset());
            await assetsRepository.AddAsync(new Asset());
            await assetsRepository.AddAsync(new Asset());
            await assetsRepository.SaveChangesAsync();

            await publicImagesRepository.AddAsync(new PublicImage());
            await publicImagesRepository.AddAsync(new PublicImage());
            await publicImagesRepository.AddAsync(new PublicImage());
            await publicImagesRepository.SaveChangesAsync();

            await mediaMaterialImagesRepository.AddAsync(new MediaMaterial());
            await mediaMaterialImagesRepository.AddAsync(new MediaMaterial());
            await mediaMaterialImagesRepository.AddAsync(new MediaMaterial());
            await mediaMaterialImagesRepository.SaveChangesAsync();

            await lawStatesRepository.AddAsync(new LawState());
            await lawStatesRepository.AddAsync(new LawState());
            await lawStatesRepository.AddAsync(new LawState());
            await lawStatesRepository.SaveChangesAsync();

            await lawProblemsRepository.AddAsync(new LawProblem());
            await lawProblemsRepository.AddAsync(new LawProblem());
            await lawProblemsRepository.AddAsync(new LawProblem());
            await lawProblemsRepository.SaveChangesAsync();

            await societyHelpsRepository.AddAsync(new SocietyHelp());
            await societyHelpsRepository.AddAsync(new SocietyHelp());
            await societyHelpsRepository.AddAsync(new SocietyHelp());
            await societyHelpsRepository.SaveChangesAsync();

            await societyActivitiesRepository.AddAsync(new SocietyActivity());
            await societyActivitiesRepository.AddAsync(new SocietyActivity());
            await societyActivitiesRepository.AddAsync(new SocietyActivity());
            await societyActivitiesRepository.SaveChangesAsync();

            await worldviewsRepository.AddAsync(new Worldview());
            await worldviewsRepository.AddAsync(new Worldview());
            await worldviewsRepository.AddAsync(new Worldview());
            await worldviewsRepository.SaveChangesAsync();

            await interestsRepository.AddAsync(new Interest());
            await interestsRepository.AddAsync(new Interest());
            await interestsRepository.AddAsync(new Interest());
            await interestsRepository.SaveChangesAsync();

            await settingsRepository.AddAsync(new Setting());
            await settingsRepository.AddAsync(new Setting());
            await settingsRepository.AddAsync(new Setting());
            await settingsRepository.SaveChangesAsync();
            
            DashboardService service = new DashboardService(applicationUsersRepository, membersRepository, citizensRepository, educationsRepository, qualificationsRepository, careersRepository, workPositionsRepository, relationshipsRepository, partyPositionsRepository, partyMembershipsRepository, materialStatesRepository, assetsRepository, publicImagesRepository, mediaMaterialImagesRepository, lawStatesRepository, lawProblemsRepository, societyHelpsRepository, societyActivitiesRepository, worldviewsRepository, interestsRepository, settingsRepository);
            IndexViewModel viewModel = service.GetIndexViewModel();

            Assert.NotNull(viewModel);
            Assert.IsType<IndexViewModel>(viewModel);
            Assert.Equal(EntitiesCount, viewModel.MembersCount);
            Assert.Equal(EntitiesCount, viewModel.CitizensCount);
            Assert.Equal(EntitiesCount, viewModel.EducationsCount);
            Assert.Equal(EntitiesCount, viewModel.QualificationsCount);
            Assert.Equal(EntitiesCount, viewModel.CareersCount);
            Assert.Equal(EntitiesCount, viewModel.WorkPositionsCount);
            Assert.Equal(EntitiesCount, viewModel.RelationshipsCount);
            Assert.Equal(EntitiesCount, viewModel.PartyPositionsCount);
            Assert.Equal(EntitiesCount, viewModel.PartyMembershipsCount);
            Assert.Equal(EntitiesCount, viewModel.MaterialStatesCount);
            Assert.Equal(EntitiesCount, viewModel.AssetsCount);
            Assert.Equal(EntitiesCount, viewModel.PublicImagesCount);
            Assert.Equal(EntitiesCount, viewModel.MediaMaterialsCount);
            Assert.Equal(EntitiesCount, viewModel.LawStatesCount);
            Assert.Equal(EntitiesCount, viewModel.LawProblemsCount);
            Assert.Equal(EntitiesCount, viewModel.SocietyHelpsCount);
            Assert.Equal(EntitiesCount, viewModel.SocietyActivitiesCount);
            Assert.Equal(EntitiesCount, viewModel.WorldviewsCount);
            Assert.Equal(EntitiesCount, viewModel.InterestsCount);
            Assert.Equal(EntitiesCount, viewModel.SettingsCount);            
        }
    }
}