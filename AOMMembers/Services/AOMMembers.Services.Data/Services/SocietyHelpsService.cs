using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.SocietyHelps;

namespace AOMMembers.Services.Data.Services
{
    public class SocietyHelpsService : ISocietyHelpsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<SocietyHelp> societyHelpsRespository;

        public SocietyHelpsService(IMapper mapper, IDeletableEntityRepository<SocietyHelp> societyHelpsRespository)
        {
            this.mapper = mapper;
            this.societyHelpsRespository = societyHelpsRespository;
        }        

        public async Task<string> CreateAsync(SocietyHelpInputModel inputModel, string citizenId)
        {
            SocietyHelp societyHelp = new SocietyHelp
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                Result = inputModel.Result,
                EventLink = inputModel.EventLink,
                CitizenId = citizenId,
                CreatedOn = DateTime.UtcNow
            };

            await this.societyHelpsRespository.AddAsync(societyHelp);
            await this.societyHelpsRespository.SaveChangesAsync();

            return societyHelp.Id;
        }

        public async Task<SocietyHelpDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            SocietyHelp societyHelp = await this.societyHelpsRespository.GetByIdAsync(id);
            SocietyHelpDetailsViewModel detailsViewModel = this.mapper.Map<SocietyHelpDetailsViewModel>(societyHelp);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            SocietyHelp societyHelp = await this.societyHelpsRespository.GetByIdAsync(id);

            return societyHelp.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, SocietyHelpEditModel editModel)
        {
            SocietyHelp societyHelp = this.societyHelpsRespository.All().FirstOrDefault(sh => sh.Id == id);
            if (societyHelp == null)
            {
                return false;
            }

            societyHelp.Name = editModel.Name;
            societyHelp.Description = editModel.Description;
            societyHelp.Result = editModel.Result;
            societyHelp.EventLink = editModel.EventLink;
            societyHelp.ModifiedOn = DateTime.UtcNow;

            await this.societyHelpsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            SocietyHelp societyHelp = this.societyHelpsRespository.All().FirstOrDefault(sh => sh.Id == id);
            if (societyHelp == null)
            {
                return false;
            }

            this.societyHelpsRespository.Delete(societyHelp);
            await this.societyHelpsRespository.SaveChangesAsync();

            return societyHelp.IsDeleted;
        }

        public int GetCountFromMember(string citizenId)
        {
            return this.societyHelpsRespository.All().Where(sh => sh.CitizenId == citizenId).Count();
        }

        public IEnumerable<SocietyHelpViewModel> GetAllFromMember(string citizenId)
        {
            List<SocietyHelpViewModel> societyHelps = this.societyHelpsRespository.AllAsNoTracking()
                .Where(sh => sh.CitizenId == citizenId)
                .OrderByDescending(sh => sh.CreatedOn)
                .To<SocietyHelpViewModel>().ToList();

            return societyHelps;
        }
    }
}