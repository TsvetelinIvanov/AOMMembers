using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.WorkPositions;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class WorkPositionsService : IWorkPositionsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<WorkPosition> workPositionsRespository;
        private readonly IDeletableEntityRepository<Career> careersRespository;

        public WorkPositionsService(IMapper mapper, IDeletableEntityRepository<WorkPosition> workPositionsRespository, IDeletableEntityRepository<Career> careersRespository)
        {
            this.mapper = mapper;
            this.workPositionsRespository = workPositionsRespository;
            this.careersRespository = careersRespository;
        }        

        public async Task<string> CreateAsync(WorkPositionInputModel inputModel, string userId)
        {
            Career career = this.careersRespository.AllAsNoTracking().FirstOrDefault(c => c.Citizen.Member.ApplicationUserId == userId);
            if (career == null)
            {
                return WorkPositionCreateWithoutCareerBadResult;
            }

            WorkPosition workPosition = new WorkPosition
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                IsCurrent = inputModel.IsCurrent,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                CareerId = career.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.workPositionsRespository.AddAsync(workPosition);
            await this.workPositionsRespository.SaveChangesAsync();

            return workPosition.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            WorkPosition workPosition = await this.workPositionsRespository.GetByIdAsync(id);

            return workPosition == null;
        }

        public async Task<WorkPositionDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            WorkPosition workPosition = await this.workPositionsRespository.GetByIdAsync(id);
            WorkPositionDetailsViewModel detailsViewModel = this.mapper.Map<WorkPositionDetailsViewModel>(workPosition);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            WorkPosition workPosition = await this.workPositionsRespository.GetByIdAsync(id);

            return workPosition.Career.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<WorkPositionEditModel> GetEditModelByIdAsync(string id)
        {
            WorkPosition workPosition = await this.workPositionsRespository.GetByIdAsync(id);
            WorkPositionEditModel editModel = this.mapper.Map<WorkPositionEditModel>(workPosition);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, WorkPositionEditModel editModel)
        {
            WorkPosition workPosition = await this.workPositionsRespository.GetByIdAsync(id);
            if (workPosition == null)
            {
                return false;
            }

            workPosition.Name = editModel.Name;
            workPosition.Description = editModel.Description;
            workPosition.IsCurrent = editModel.IsCurrent;
            workPosition.StartDate = editModel.StartDate;
            workPosition.EndDate = editModel.EndDate;
            workPosition.ModifiedOn = DateTime.UtcNow;

            await this.workPositionsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<WorkPositionDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            WorkPosition workPosition = await this.workPositionsRespository.GetByIdAsync(id);
            WorkPositionDeleteModel deleteModel = this.mapper.Map<WorkPositionDeleteModel>(workPosition);

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            WorkPosition workPosition = await this.workPositionsRespository.GetByIdAsync(id);
            if (workPosition == null)
            {
                return false;
            }

            this.workPositionsRespository.Delete(workPosition);
            await this.workPositionsRespository.SaveChangesAsync();

            return workPosition.IsDeleted;
        }

        public int GetCountFromMember(string userId)
        {
            Career career = this.careersRespository.AllAsNoTracking().FirstOrDefault(c => c.Citizen.Member.ApplicationUserId == userId);
            if (career == null)
            {
                return 0;
            }

            return this.workPositionsRespository.All().Where(wp => wp.CareerId == career.Id).Count();
        }

        public IEnumerable<WorkPositionViewModel> GetAllFromMember(string userId)
        {
            Career career = this.careersRespository.AllAsNoTracking().FirstOrDefault(c => c.Citizen.Member.ApplicationUserId == userId);
            if (career == null)
            {
                return null;
            }

            List<WorkPositionViewModel> workPositions = this.workPositionsRespository.AllAsNoTracking()
                .Where(wp => wp.CareerId == career.Id)
                .OrderByDescending(wp => wp.CreatedOn)
                .To<WorkPositionViewModel>().ToList();

            return workPositions;
        }
    }
}