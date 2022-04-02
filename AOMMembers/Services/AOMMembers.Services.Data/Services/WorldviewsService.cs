using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Worldviews;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class WorldviewsService : IWorldviewsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Worldview> worldviewsRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;
        private readonly IDeletableEntityRepository<Interest> interestsRespository;

        public WorldviewsService(IMapper mapper, IDeletableEntityRepository<Worldview> worldviewsRespository, IDeletableEntityRepository<Citizen> citizensRespository, IDeletableEntityRepository<Interest> interestsRespository)
        {
            this.mapper = mapper;
            this.worldviewsRespository = worldviewsRespository;
            this.citizensRespository = citizensRespository;
            this.interestsRespository = interestsRespository;
        }

        public async Task<string> CreateAsync(WorldviewInputModel inputModel, string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return WorldviewCreateWithoutCitizenBadResult;
            }

            Worldview worldview = new Worldview
            {
                Description = inputModel.Description,
                Ideology = inputModel.Ideology,
                CitizenId = citizen.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.worldviewsRespository.AddAsync(worldview);
            await this.worldviewsRespository.SaveChangesAsync();

            return worldview.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            Worldview worldview = await this.worldviewsRespository.GetByIdAsync(id);

            return worldview == null;
        }

        public async Task<WorldviewDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Worldview worldview = await this.worldviewsRespository.GetByIdAsync(id);
            WorldviewDetailsViewModel detailsViewModel = new WorldviewDetailsViewModel
            {
                Id = worldview.Id,
                Description = worldview.Description,
                Ideology = worldview.Ideology,
                CitizenId = worldview.CitizenId,
                CreatedOn = worldview.CreatedOn,
                ModifiedOn = worldview.ModifiedOn,
                InterestsCount = worldview.Interests.Count
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Worldview worldview = await this.worldviewsRespository.GetByIdAsync(id);

            return worldview.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<WorldviewEditModel> GetEditModelByIdAsync(string id)
        {
            Worldview worldview = await this.worldviewsRespository.GetByIdAsync(id);
            WorldviewEditModel editModel = this.mapper.Map<WorldviewEditModel>(worldview);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, WorldviewEditModel editModel)
        {
            Worldview worldview = await this.worldviewsRespository.GetByIdAsync(id);
            if (worldview == null)
            {
                return false;
            }

            worldview.Description = editModel.Description;
            worldview.Ideology = editModel.Ideology;
            worldview.ModifiedOn = DateTime.UtcNow;

            await this.worldviewsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<WorldviewDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            Worldview worldview = await this.worldviewsRespository.GetByIdAsync(id);
            WorldviewDeleteModel deleteModel = new WorldviewDeleteModel
            {
                Id = worldview.Id,
                Description = worldview.Description,
                Ideology = worldview.Ideology,                
                CreatedOn = worldview.CreatedOn,
                ModifiedOn = worldview.ModifiedOn,
                InterestsCount = worldview.Interests.Count
            };

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Worldview worldview = await this.worldviewsRespository.GetByIdAsync(id);
            if (worldview == null)
            {
                return false;
            }

            foreach (Interest interest in worldview.Interests)
            {
                this.interestsRespository.Delete(interest);
            }

            await this.interestsRespository.SaveChangesAsync();

            this.worldviewsRespository.Delete(worldview);
            await this.worldviewsRespository.SaveChangesAsync();

            return worldview.IsDeleted;
        }
    }
}