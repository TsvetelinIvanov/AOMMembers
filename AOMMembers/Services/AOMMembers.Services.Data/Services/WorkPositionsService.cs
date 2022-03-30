using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.WorkPositions;

namespace AOMMembers.Services.Data.Services
{
    public class WorkPositionsService : IWorkPositionsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<WorkPosition> workPositionsRespository;

        public WorkPositionsService(IMapper mapper, IDeletableEntityRepository<WorkPosition> workPositionsRespository)
        {
            this.mapper = mapper;
            this.workPositionsRespository = workPositionsRespository;
        }        

        public async Task<string> CreateAsync(WorkPositionInputModel inputModel, string careerId)
        {
            WorkPosition workPosition = new WorkPosition
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                IsCurrent = inputModel.IsCurrent,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                CareerId = careerId,
                CreatedOn = DateTime.UtcNow
            };

            await this.workPositionsRespository.AddAsync(workPosition);
            await this.workPositionsRespository.SaveChangesAsync();

            return workPosition.Id;
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

        public async Task<bool> EditAsync(string id, WorkPositionEditModel editModel)
        {
            WorkPosition workPosition = this.workPositionsRespository.All().FirstOrDefault(wp => wp.Id == id);
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

        public async Task<bool> DeleteAsync(string id)
        {
            WorkPosition workPosition = this.workPositionsRespository.All().FirstOrDefault(wp => wp.Id == id);
            if (workPosition == null)
            {
                return false;
            }

            this.workPositionsRespository.Delete(workPosition);
            await this.workPositionsRespository.SaveChangesAsync();

            return workPosition.IsDeleted;
        }

        public int GetCountFromMember(string careerId)
        {
            return this.workPositionsRespository.All().Where(wp => wp.CareerId == careerId).Count();
        }

        public IEnumerable<WorkPositionViewModel> GetAllFromMember(string careerId)
        {
            List<WorkPositionViewModel> workPositions = this.workPositionsRespository.AllAsNoTracking()
                .Where(wp => wp.CareerId == careerId)
                .OrderByDescending(wp => wp.CreatedOn)
                .To<WorkPositionViewModel>().ToList();

            return workPositions;
        }
    }
}