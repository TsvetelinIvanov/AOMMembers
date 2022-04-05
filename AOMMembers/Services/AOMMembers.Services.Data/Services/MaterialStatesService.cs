using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.MaterialStates;
using AOMMembers.Web.ViewModels.Assets;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class MaterialStatesService : IMaterialStatesService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<MaterialState> materialStatesRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;
        private readonly IDeletableEntityRepository<Asset> assetsRespository;

        public MaterialStatesService(IMapper mapper, IDeletableEntityRepository<MaterialState> materialStatesRespository, IDeletableEntityRepository<Citizen> citizensRespository, IDeletableEntityRepository<Asset> assetsRespository)
        {
            this.mapper = mapper;
            this.materialStatesRespository = materialStatesRespository;
            this.citizensRespository = citizensRespository;
            this.assetsRespository = assetsRespository;
        }

        public bool IsCreated(string userId)
        {
            return this.materialStatesRespository.AllAsNoTracking().Any(ms => ms.Citizen.Member.ApplicationUserId == userId);
        }

        public async Task<string> CreateAsync(MaterialStateInputModel inputModel, string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return MaterialStateCreateWithoutCitizenBadResult;
            }

            MaterialState materialState = new MaterialState
            {
                Riches = inputModel.Riches,
                Money = inputModel.Money,
                MonthIncome = inputModel.MonthIncome,
                Description = inputModel.Description,
                TaxDeclarationLink = inputModel.TaxDeclarationLink,
                CitizenId = citizen.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.materialStatesRespository.AddAsync(materialState);
            await this.materialStatesRespository.SaveChangesAsync();

            return materialState.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(id);

            return materialState == null;
        }

        public async Task<MaterialStateViewModel> GetViewModelByIdAsync(string id)
        {
            MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(id);
            MaterialStateViewModel viewModel = new MaterialStateViewModel
            {
                Id = materialState.Id,
                Riches = materialState.Riches,
                Money = materialState.Money,
                MonthIncome = materialState.MonthIncome,
                Description = materialState.Description,
                TaxDeclarationLink = materialState.TaxDeclarationLink,                
                AssetsCount = materialState.Assets.Count
                
            };

            return viewModel;
        }

        public async Task<MaterialStateDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(id);

            HashSet<AssetViewModel> assets = new HashSet<AssetViewModel>();
            foreach (Asset asset in materialState.Assets)
            {
                AssetViewModel assetViewModel = this.mapper.Map<AssetViewModel>(asset);
                assets.Add(assetViewModel);
            }

            MaterialStateDetailsViewModel detailsViewModel = new MaterialStateDetailsViewModel
            {
                Id = materialState.Id,
                Riches = materialState.Riches,
                Money = materialState.Money,
                MonthIncome = materialState.MonthIncome,
                Description = materialState.Description,
                TaxDeclarationLink = materialState.TaxDeclarationLink,
                CitizenId = materialState.CitizenId,
                CreatedOn = materialState.CreatedOn,
                ModifiedOn = materialState.ModifiedOn,
                AssetsCount = materialState.Assets.Count,
                Assets = assets
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(id);

            return materialState.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<MaterialStateEditModel> GetEditModelByIdAsync(string id)
        {
            MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(id);
            MaterialStateEditModel editModel = this.mapper.Map<MaterialStateEditModel>(materialState);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, MaterialStateEditModel editModel)
        {
            MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(id);
            if (materialState == null)
            {
                return false;
            }

            materialState.Riches = editModel.Riches;
            materialState.Money = editModel.Money;
            materialState.MonthIncome = editModel.MonthIncome;
            materialState.Description = editModel.Description;
            materialState.TaxDeclarationLink = editModel.TaxDeclarationLink;
            materialState.ModifiedOn = DateTime.UtcNow;

            await this.materialStatesRespository.SaveChangesAsync();

            return true;
        }

        public async Task<MaterialStateDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(id);
            MaterialStateDeleteModel deleteModel = new MaterialStateDeleteModel
            {
                Id = materialState.Id,
                Riches = materialState.Riches,
                Money = materialState.Money,
                MonthIncome = materialState.MonthIncome,
                Description = materialState.Description,
                TaxDeclarationLink = materialState.TaxDeclarationLink,                
                CreatedOn = materialState.CreatedOn,
                ModifiedOn = materialState.ModifiedOn,
                AssetsCount = materialState.Assets.Count
            };

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(id);
            if (materialState == null)
            {
                return false;
            }

            foreach (Asset asset in materialState.Assets)
            {
                this.assetsRespository.Delete(asset);
            }

            await this.assetsRespository.SaveChangesAsync();

            this.materialStatesRespository.Delete(materialState);
            await this.materialStatesRespository.SaveChangesAsync();

            return materialState.IsDeleted;
        }
    }
}