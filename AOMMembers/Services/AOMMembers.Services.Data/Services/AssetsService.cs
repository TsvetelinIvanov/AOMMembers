using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.Assets;

namespace AOMMembers.Services.Data.Services
{
    public class AssetsService : IAssetsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Asset> assetsRespository;

        public AssetsService(IMapper mapper, IDeletableEntityRepository<Asset> assetsRespository)
        {
            this.mapper = mapper;
            this.assetsRespository = assetsRespository;
        }        

        public async Task<string> CreateAsync(AssetInputModel inputModel, string materialStateId)
        {
            Asset asset = new Asset
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                Worthiness = inputModel.Worthiness,
                MaterialStateId = materialStateId,
                CreatedOn = DateTime.UtcNow
            };

            await this.assetsRespository.AddAsync(asset);
            await this.assetsRespository.SaveChangesAsync();

            return asset.Id;
        }

        public async Task<AssetDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Asset asset = await this.assetsRespository.GetByIdAsync(id);
            AssetDetailsViewModel detailsViewModel = this.mapper.Map<AssetDetailsViewModel>(asset);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Asset asset = await this.assetsRespository.GetByIdAsync(id);

            return asset.MaterialState.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, AssetEditModel editModel)
        {
            Asset asset = this.assetsRespository.All().FirstOrDefault(a => a.Id == id);
            if (asset == null)
            {
                return false;
            }

            asset.Name = editModel.Name;
            asset.Description = editModel.Description;
            asset.Worthiness = editModel.Worthiness;
            asset.ModifiedOn = DateTime.UtcNow;

            await this.assetsRespository.SaveChangesAsync();

            return true;
        }        

        public async Task<bool> DeleteAsync(string id)
        {
            Asset asset = this.assetsRespository.All().FirstOrDefault(a => a.Id == id);            
            if (asset == null)
            {
                return false;
            }

            this.assetsRespository.Delete(asset);
            await this.assetsRespository.SaveChangesAsync();

            return asset.IsDeleted;
        }

        public int GetCountFromMember(string materialStateId)
        {
            return this.assetsRespository.All().Where(a => a.MaterialStateId == materialStateId).Count();
        }

        public IEnumerable<AssetViewModel> GetAllFromMember(string materialStateId)
        {
            List<AssetViewModel> assets = this.assetsRespository.AllAsNoTracking()
                .Where(a => a.MaterialStateId == materialStateId)
                .OrderByDescending(a => a.CreatedOn)                
                .To<AssetViewModel>().ToList();

            return assets;
        }
    }
}