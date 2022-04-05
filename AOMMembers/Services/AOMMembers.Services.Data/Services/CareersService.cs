using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Careers;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class CareersService : ICareersService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Career> careersRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;
        private readonly IDeletableEntityRepository<WorkPosition> workPositionsRespository;

        public CareersService(IMapper mapper, IDeletableEntityRepository<Career> careersRespository, IDeletableEntityRepository<Citizen> citizensRespository, IDeletableEntityRepository<WorkPosition> workPositionsRespository)
        {
            this.mapper = mapper;
            this.careersRespository = careersRespository;
            this.citizensRespository = citizensRespository;
            this.workPositionsRespository = workPositionsRespository;
        }

        public bool IsCreated(string userId)
        {
            return this.careersRespository.AllAsNoTracking().Any(c => c.Citizen.Member.ApplicationUserId == userId);
        }

        public async Task<string> CreateAsync(CareerInputModel inputModel, string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return CareerCreateWithoutCitizenBadResult;
            }
            
            Career career = new Career
            {
                Description = inputModel.Description,
                CVLink = inputModel.CVLink,
                CitizenId = citizen.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.careersRespository.AddAsync(career);
            await this.careersRespository.SaveChangesAsync();

            return career.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            Career career = await this.careersRespository.GetByIdAsync(id);

            return career == null;
        }

        public async Task<CareerViewModel> GetViewModelByIdAsync(string id)
        {            
            Career career = await this.careersRespository.GetByIdAsync(id);

            WorkPosition workPosition = career.WorkPositions.FirstOrDefault(wp => wp.IsCurrent);
            string workPositionName = workPosition != null ? workPosition.Name : "---";

            CareerViewModel viewModel = new CareerViewModel
            {
                Id = career.Id,
                Description = career.Description,
                CVLink = career.CVLink,                
                CurrentWorkPosition = workPositionName,
                WorkPositionsCount = career.WorkPositions.Count
            };

            return viewModel;
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

        public async Task<CareerEditModel> GetEditModelByIdAsync(string id)
        {
            Career career = await this.careersRespository.GetByIdAsync(id);
            CareerEditModel editModel = this.mapper.Map<CareerEditModel>(career);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, CareerEditModel editModel)
        {
            Career career = await this.careersRespository.GetByIdAsync(id);
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

        public async Task<CareerDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            Career career = await this.careersRespository.GetByIdAsync(id);

            WorkPosition workPosition = career.WorkPositions.FirstOrDefault(wp => wp.IsCurrent);
            string workPositionName = workPosition != null ? workPosition.Name : "---";

            CareerDeleteModel deleteModel = new CareerDeleteModel
            {
                Id = career.Id,
                Description = career.Description,
                CVLink = career.CVLink,                
                CreatedOn = career.CreatedOn,
                ModifiedOn = career.ModifiedOn,
                CurrentWorkPosition = workPositionName,
                WorkPositionsCount = career.WorkPositions.Count
            };

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Career career = await this.careersRespository.GetByIdAsync(id);
            if (career == null)
            {
                return false;
            }

            foreach (WorkPosition workPosition in career.WorkPositions)
            {
                this.workPositionsRespository.Delete(workPosition);
            }

            await this.workPositionsRespository.SaveChangesAsync();

            this.careersRespository.Delete(career);
            await this.careersRespository.SaveChangesAsync();

            return career.IsDeleted;
        }
    }
}