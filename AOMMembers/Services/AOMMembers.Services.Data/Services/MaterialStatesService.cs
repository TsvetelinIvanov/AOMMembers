using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.MaterialStates;

namespace AOMMembers.Services.Data.Services
{
    public class MaterialStatesService : IMaterialStatesService
    {
        private readonly IDeletableEntityRepository<MaterialState> materialStatesRespository;

        public MaterialStatesService(IDeletableEntityRepository<MaterialState> materialStatesRespository)
        {
            this.materialStatesRespository = materialStatesRespository;
        }        

        public async Task<string> CreateAsync(MaterialStateInputModel inputModel, string citizenId)
        {
            MaterialState materialState = new MaterialState
            {
                Riches = inputModel.Riches,
                Money = inputModel.Money,
                MonthIncome = inputModel.MonthIncome,
                Description = inputModel.Description,
                TaxDeclarationLink = inputModel.TaxDeclarationLink,
                CitizenId = citizenId,
                CreatedOn = DateTime.UtcNow
            };

            await this.materialStatesRespository.AddAsync(materialState);
            await this.materialStatesRespository.SaveChangesAsync();

            return materialState.Id;
        }

        public async Task<MaterialStateDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(id);
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
                AssetsCount = materialState.Assets.Count
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            MaterialState materialState = await this.materialStatesRespository.GetByIdAsync(id);

            return materialState.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, MaterialStateEditModel editModel)
        {
            MaterialState materialState = this.materialStatesRespository.All().FirstOrDefault(ms => ms.Id == id);
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

        public async Task<bool> DeleteAsync(string id)
        {
            MaterialState materialState = this.materialStatesRespository.All().FirstOrDefault(ms => ms.Id == id);
            if (materialState == null)
            {
                return false;
            }

            this.materialStatesRespository.Delete(materialState);
            await this.materialStatesRespository.SaveChangesAsync();

            return materialState.IsDeleted;
        }
    }
}