using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Educations;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class EducationsService : IEducationsService
    {
        private readonly IDeletableEntityRepository<Education> educationsRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;

        public EducationsService(IDeletableEntityRepository<Education> educationsRespository, IDeletableEntityRepository<Citizen> citizensRespository)
        {
            this.educationsRespository = educationsRespository;
            this.citizensRespository = citizensRespository;
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

        public async Task<EducationDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Education education = await this.educationsRespository.GetByIdAsync(id);
            EducationDetailsViewModel detailsViewModel = new EducationDetailsViewModel
            {
                Id = education.Id,
                Description = education.Description,
                CVLink = education.CVLink,
                CitizenId = education.CitizenId,
                CreatedOn = education.CreatedOn,
                ModifiedOn = education.ModifiedOn,                
                QualificationsCount = education.Qualifications.Count
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Education education = await this.educationsRespository.GetByIdAsync(id);

            return education.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, EducationEditModel editModel)
        {
            Education education = this.educationsRespository.All().FirstOrDefault(e => e.Id == id);
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

        public async Task<bool> DeleteAsync(string id)
        {
            Education education = this.educationsRespository.All().FirstOrDefault(e => e.Id == id);
            if (education == null)
            {
                return false;
            }

            this.educationsRespository.Delete(education);
            await this.educationsRespository.SaveChangesAsync();

            return education.IsDeleted;
        }
    }
}
