using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.Qualifications;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class QualificationsService : IQualificationsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Qualification> qualificationsRespository;
        private readonly IDeletableEntityRepository<Education> educationsRespository;

        public QualificationsService(IMapper mapper, IDeletableEntityRepository<Qualification> qualificationsRespository, IDeletableEntityRepository<Education> educationsRespository)
        {
            this.mapper = mapper;
            this.qualificationsRespository = qualificationsRespository;
            this.educationsRespository = educationsRespository;
        }        

        public async Task<string> CreateAsync(QualificationInputModel inputModel, string userId)
        {
            Education education = this.educationsRespository.AllAsNoTracking().FirstOrDefault(e => e.Citizen.Member.ApplicationUserId == userId);
            if (education == null)
            {
                return QualificationCreateWithoutEducationBadResult;
            }

            Qualification qualification = new Qualification
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                EducationId = education.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.qualificationsRespository.AddAsync(qualification);
            await this.qualificationsRespository.SaveChangesAsync();

            return qualification.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            Qualification qualification = await this.qualificationsRespository.GetByIdAsync(id);

            return qualification == null;
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

        public async Task<QualificationEditModel> GetEditModelByIdAsync(string id)
        {
            Qualification qualification = await this.qualificationsRespository.GetByIdAsync(id);
            QualificationEditModel editModel = this.mapper.Map<QualificationEditModel>(qualification);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, QualificationEditModel editModel)
        {
            Qualification qualification = await this.qualificationsRespository.GetByIdAsync(id);
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

        public async Task<QualificationDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            Qualification qualification = await this.qualificationsRespository.GetByIdAsync(id);
            QualificationDeleteModel deleteModel = this.mapper.Map<QualificationDeleteModel>(qualification);

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Qualification qualification = await this.qualificationsRespository.GetByIdAsync(id);
            if (qualification == null)
            {
                return false;
            }

            this.qualificationsRespository.Delete(qualification);
            await this.qualificationsRespository.SaveChangesAsync();

            return qualification.IsDeleted;
        }

        public int GetCountFromMember(string userId)
        {
            Education education = this.educationsRespository.AllAsNoTracking().FirstOrDefault(e => e.Citizen.Member.ApplicationUserId == userId);
            if (education == null)
            {
                return 0;
            }

            return this.qualificationsRespository.All().Where(q => q.EducationId == education.Id).Count();
        }

        public IEnumerable<QualificationViewModel> GetAllFromMember(string userId)
        {
            Education education = this.educationsRespository.AllAsNoTracking().FirstOrDefault(e => e.Citizen.Member.ApplicationUserId == userId);
            if (education == null)
            {
                return null;
            }

            List<QualificationViewModel> qualifications = this.qualificationsRespository.AllAsNoTracking()
                .Where(q => q.EducationId == education.Id)
                .OrderByDescending(q => q.CreatedOn)
                .To<QualificationViewModel>().ToList();

            return qualifications;
        }
    }
}