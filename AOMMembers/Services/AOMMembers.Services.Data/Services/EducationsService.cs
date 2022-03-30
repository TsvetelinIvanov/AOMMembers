using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Educations;

namespace AOMMembers.Services.Data.Services
{
    public class EducationsService : IEducationsService
    {
        private readonly IDeletableEntityRepository<Education> educationsRespository;

        public EducationsService(IDeletableEntityRepository<Education> educationsRespository)
        {
            this.educationsRespository = educationsRespository;
        }        

        public async Task<string> CreateAsync(EducationInputModel inputModel, string citizenId)
        {
            Education education = new Education
            {
                Description = inputModel.Description,
                CVLink = inputModel.CVLink,
                CitizenId = citizenId,
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
