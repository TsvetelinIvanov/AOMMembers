using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.Qualifications;

namespace AOMMembers.Services.Data.Services
{
    public class QualificationsService : IQualificationsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Qualification> qualificationsRespository;

        public QualificationsService(IMapper mapper, IDeletableEntityRepository<Qualification> qualificationsRespository)
        {
            this.mapper = mapper;
            this.qualificationsRespository = qualificationsRespository;
        }        

        public async Task<string> CreateAsync(QualificationInputModel inputModel, string educationId)
        {
            Qualification qualification = new Qualification
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                EducationId = educationId,
                CreatedOn = DateTime.UtcNow
            };

            await this.qualificationsRespository.AddAsync(qualification);
            await this.qualificationsRespository.SaveChangesAsync();

            return qualification.Id;
        }

        public async Task<QualificationDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Qualification qualification = await this.qualificationsRespository.GetByIdAsync(id);
            QualificationDetailsViewModel detailsViewModel = this.mapper.Map<QualificationDetailsViewModel>(qualification);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Qualification qualification = await this.qualificationsRespository.GetByIdAsync(id);

            return qualification.Education.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, QualificationEditModel editModel)
        {
            Qualification qualification = this.qualificationsRespository.All().FirstOrDefault(q => q.Id == id);
            if (qualification == null)
            {
                return false;
            }

            qualification.Name = editModel.Name;
            qualification.Description = editModel.Description;
            qualification.StartDate = editModel.StartDate;
            qualification.EndDate = editModel.EndDate;
            qualification.ModifiedOn = DateTime.UtcNow;

            await this.qualificationsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Qualification qualification = this.qualificationsRespository.All().FirstOrDefault(q => q.Id == id);
            if (qualification == null)
            {
                return false;
            }

            this.qualificationsRespository.Delete(qualification);
            await this.qualificationsRespository.SaveChangesAsync();

            return qualification.IsDeleted;
        }

        public int GetCountFromMember(string educationId)
        {
            return this.qualificationsRespository.All().Where(q => q.EducationId == educationId).Count();
        }

        public IEnumerable<QualificationViewModel> GetAllFromMember(string educationId)
        {
            List<QualificationViewModel> qualifications = this.qualificationsRespository.AllAsNoTracking()
                .Where(q => q.EducationId == educationId)
                .OrderByDescending(q => q.CreatedOn)
                .To<QualificationViewModel>().ToList();

            return qualifications;
        }
    }
}