using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Educations;
using AOMMembers.Web.ViewModels.Qualifications;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class EducationsService : IEducationsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Education> educationsRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;
        private readonly IDeletableEntityRepository<Qualification> qualificationsRespository;

        public EducationsService(IMapper mapper, IDeletableEntityRepository<Education> educationsRespository, IDeletableEntityRepository<Citizen> citizensRespository, IDeletableEntityRepository<Qualification> qualificationsRespository)
        {
            this.mapper = mapper;
            this.educationsRespository = educationsRespository;
            this.citizensRespository = citizensRespository;
            this.qualificationsRespository = qualificationsRespository;
        }

        public bool IsCreated(string userId)
        {
            return this.educationsRespository.AllAsNoTracking().Any(e => e.Citizen.Member.ApplicationUserId == userId);
        }

        public async Task<string> CreateAsync(EducationInputModel inputModel, string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return EducationCreateWithoutCitizenBadResult;
            }

            Education education = new Education
            {
                Description = inputModel.Description,
                CVLink = inputModel.CVLink,
                CitizenId = citizen.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.educationsRespository.AddAsync(education);
            await this.educationsRespository.SaveChangesAsync();

            return education.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            Education education = await this.educationsRespository.GetByIdAsync(id);

            return education == null;
        }

        public async Task<EducationViewModel> GetViewModelByIdAsync(string id)
        {
            Education education = await this.educationsRespository.GetByIdAsync(id);
            EducationViewModel viewModel = new EducationViewModel
            {
                Id = education.Id,
                Description = education.Description,
                CVLink = education.CVLink,                
                QualificationsCount = education.Qualifications.Count
            };

            return viewModel;
        }

        public async Task<EducationDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Education education = await this.educationsRespository.GetByIdAsync(id);

            HashSet<QualificationViewModel> qualifications = new HashSet<QualificationViewModel>();
            foreach (Qualification qualification in education.Qualifications)
            {
                QualificationViewModel qualificationViewModel = this.mapper.Map<QualificationViewModel>(qualification);
                qualifications.Add(qualificationViewModel);
            }

            EducationDetailsViewModel detailsViewModel = new EducationDetailsViewModel
            {
                Id = education.Id,
                Description = education.Description,
                CVLink = education.CVLink,
                CitizenId = education.CitizenId,
                CreatedOn = education.CreatedOn,
                ModifiedOn = education.ModifiedOn,
                QualificationsCount = education.Qualifications.Count,
                Qualifications = qualifications
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Education education = await this.educationsRespository.GetByIdAsync(id);

            return education.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<EducationEditModel> GetEditModelByIdAsync(string id)
        {
            Education education = await this.educationsRespository.GetByIdAsync(id);
            EducationEditModel editModel = this.mapper.Map<EducationEditModel>(education);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, EducationEditModel editModel)
        {
            Education education = await this.educationsRespository.GetByIdAsync(id);
            if (education == null)
            {
                return false;
            }

            education.Description = editModel.Description;
            education.CVLink = editModel.CVLink;
            education.ModifiedOn = DateTime.UtcNow;

            await this.educationsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<EducationDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            Education education = await this.educationsRespository.GetByIdAsync(id);
            EducationDeleteModel deleteModel = new EducationDeleteModel
            {
                Id = education.Id,
                Description = education.Description,
                CVLink = education.CVLink,                
                CreatedOn = education.CreatedOn,
                ModifiedOn = education.ModifiedOn,
                QualificationsCount = education.Qualifications.Count
            };

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Education education = await this.educationsRespository.GetByIdAsync(id);
            if (education == null)
            {
                return false;
            }

            foreach (Qualification qualification in education.Qualifications)
            {
                this.qualificationsRespository.Delete(qualification);
            }

            await this.qualificationsRespository.SaveChangesAsync();

            this.educationsRespository.Delete(education);
            await this.educationsRespository.SaveChangesAsync();

            return education.IsDeleted;
        }
    }
}
