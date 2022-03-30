using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Careers;

namespace AOMMembers.Services.Data.Services
{
    public class CareersService : ICareersService
    {
        private readonly IDeletableEntityRepository<Career> careersRespository;

        public CareersService(IDeletableEntityRepository<Career> careersRespository)
        {            
            this.careersRespository = careersRespository;
        }        

        public async Task<string> CreateAsync(CareerInputModel inputModel, string citizenId)
        {
            Career career = new Career
            {                
                Description = inputModel.Description,
                CVLink = inputModel.CVLink,
                CitizenId = citizenId,
                CreatedOn = DateTime.UtcNow
            };

            await this.careersRespository.AddAsync(career);
            await this.careersRespository.SaveChangesAsync();

            return career.Id;
        }

        public async Task<CareerDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Career career = await this.careersRespository.GetByIdAsync(id);

            WorkPosition workPosition = career.WorkPositions.FirstOrDefault(wp => wp.IsCurrent);
            string workPositionName = workPosition != null ? workPosition.Name : "---";

            CareerDetailsViewModel detailsViewModel = new CareerDetailsViewModel
            {
                Id = career.Id,
                Description = career.Description,
                CVLink = career.CVLink,
                CitizenId = career.CitizenId,
                CreatedOn = career.CreatedOn,
                ModifiedOn = career.ModifiedOn,
                CurrentWorkPosition = workPositionName,
                WorkPositionsCount = career.WorkPositions.Count
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Career career = await this.careersRespository.GetByIdAsync(id);

            return career.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, CareerEditModel editModel)
        {
            Career career = this.careersRespository.All().FirstOrDefault(c => c.Id == id);
            if (career == null)
            {
                return false;
            }
            
            career.Description = editModel.Description;
            career.CVLink = editModel.CVLink;
            career.ModifiedOn = DateTime.UtcNow;

            await this.careersRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Career career = this.careersRespository.All().FirstOrDefault(c => c.Id == id);
            if (career == null)
            {
                return false;
            }

            this.careersRespository.Delete(career);
            await this.careersRespository.SaveChangesAsync();

            return career.IsDeleted;
        }
    }
}