using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.SocietyHelps;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class SocietyHelpsService : ISocietyHelpsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<SocietyHelp> societyHelpsRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;

        public SocietyHelpsService(IMapper mapper, IDeletableEntityRepository<SocietyHelp> societyHelpsRespository, IDeletableEntityRepository<Citizen> citizensRespository)
        {
            this.mapper = mapper;
            this.societyHelpsRespository = societyHelpsRespository;
            this.citizensRespository = citizensRespository;
        }

        public async Task<string> CreateAsync(SocietyHelpInputModel inputModel, string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return SocietyHelpCreateWithoutCitizenBadResult;
            }

            SocietyHelp societyHelp = new SocietyHelp
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                Result = inputModel.Result,
                EventLink = inputModel.EventLink,
                CitizenId = citizen.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.societyHelpsRespository.AddAsync(societyHelp);
            await this.societyHelpsRespository.SaveChangesAsync();

            return societyHelp.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            SocietyHelp societyHelp = await this.societyHelpsRespository.GetByIdAsync(id);

            return societyHelp == null;
        }

        public async Task<SocietyHelpViewModel> GetViewModelByIdAsync(string id)
        {
            SocietyHelp societyHelp = await this.societyHelpsRespository.GetByIdAsync(id);
            SocietyHelpViewModel viewModel = this.mapper.Map<SocietyHelpViewModel>(societyHelp);

            return viewModel;
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

        public async Task<SocietyHelpEditModel> GetEditModelByIdAsync(string id)
        {
            SocietyHelp societyHelp = await this.societyHelpsRespository.GetByIdAsync(id);
            SocietyHelpEditModel editModel = this.mapper.Map<SocietyHelpEditModel>(societyHelp);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, SocietyHelpEditModel editModel)
        {
            SocietyHelp societyHelp = await this.societyHelpsRespository.GetByIdAsync(id);
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

        public async Task<SocietyHelpDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            SocietyHelp societyHelp = await this.societyHelpsRespository.GetByIdAsync(id);
            SocietyHelpDeleteModel deleteModel = this.mapper.Map<SocietyHelpDeleteModel>(societyHelp);

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            SocietyHelp societyHelp = await this.societyHelpsRespository.GetByIdAsync(id);
            if (societyHelp == null)
            {
                return false;
            }

            this.societyHelpsRespository.Delete(societyHelp);
            await this.societyHelpsRespository.SaveChangesAsync();

            return societyHelp.IsDeleted;
        }

        public int GetCountFromMember(string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return 0;
            }

            return this.societyHelpsRespository.All().Where(sh => sh.CitizenId == citizen.Id).Count();
        }

        public IEnumerable<SocietyHelpViewModel> GetAllFromMember(string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return null;
            }

            List<SocietyHelpViewModel> societyHelps = this.societyHelpsRespository.AllAsNoTracking()
                .Where(sh => sh.CitizenId == citizen.Id)
                .OrderByDescending(sh => sh.CreatedOn)
                .To<SocietyHelpViewModel>().ToList();

            return societyHelps;
        }
    }
}