using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Worldviews;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class WorldviewsService : IWorldviewsService
    {
        private readonly IDeletableEntityRepository<Worldview> worldviewsRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;

        public WorldviewsService(IDeletableEntityRepository<Worldview> worldviewsRespository, IDeletableEntityRepository<Citizen> citizensRespository)
        {
            this.worldviewsRespository = worldviewsRespository;
            this.citizensRespository = citizensRespository;
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

        public async Task<bool> EditAsync(string id, WorldviewEditModel editModel)
        {
            Worldview worldview = this.worldviewsRespository.All().FirstOrDefault(w => w.Id == id);
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

        public async Task<bool> DeleteAsync(string id)
        {
            Worldview worldview = this.worldviewsRespository.All().FirstOrDefault(w => w.Id == id);
            if (worldview == null)
            {
                return false;
            }

            this.worldviewsRespository.Delete(worldview);
            await this.worldviewsRespository.SaveChangesAsync();

            return worldview.IsDeleted;
        }
    }
}