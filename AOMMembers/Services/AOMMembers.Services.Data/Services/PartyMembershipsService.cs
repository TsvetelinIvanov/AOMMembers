using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.PartyMemberships;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class PartyMembershipsService : IPartyMembershipsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<PartyMembership> partyMembershipsRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;

        public PartyMembershipsService(IMapper mapper, IDeletableEntityRepository<PartyMembership> partyMembershipsRespository, IDeletableEntityRepository<Citizen> citizensRespository)
        {
            this.mapper = mapper;
            this.partyMembershipsRespository = partyMembershipsRespository;
            this.citizensRespository = citizensRespository;
        }

        public async Task<string> CreateAsync(PartyMembershipInputModel inputModel, string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return PartyMembershipCreateWithoutCitizenBadResult;
            }

            PartyMembership partyMembership = new PartyMembership
            {
                PartyName = inputModel.PartyName,
                Description = inputModel.Description,
                IsCurrent = inputModel.IsCurrent,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                CitizenId = citizen.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.partyMembershipsRespository.AddAsync(partyMembership);
            await this.partyMembershipsRespository.SaveChangesAsync();

            return partyMembership.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            PartyMembership partyMembership = await this.partyMembershipsRespository.GetByIdAsync(id);

            return partyMembership == null;
        }

        public async Task<PartyMembershipViewModel> GetViewModelByIdAsync(string id)
        {
            PartyMembership partyMembership = await this.partyMembershipsRespository.GetByIdAsync(id);
            PartyMembershipViewModel viewModel = this.mapper.Map<PartyMembershipViewModel>(partyMembership);

            return viewModel;
        }

        public async Task<PartyMembershipDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            PartyMembership partyMembership = await this.partyMembershipsRespository.GetByIdAsync(id);
            PartyMembershipDetailsViewModel detailsViewModel = this.mapper.Map<PartyMembershipDetailsViewModel>(partyMembership);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            PartyMembership partyMembership = await this.partyMembershipsRespository.GetByIdAsync(id);

            return partyMembership.Citizen.Member.ApplicationUserId == userId;
        }

        public async Task<PartyMembershipEditModel> GetEditModelByIdAsync(string id)
        {
            PartyMembership partyMembership = await this.partyMembershipsRespository.GetByIdAsync(id);
            PartyMembershipEditModel editModel = this.mapper.Map<PartyMembershipEditModel>(partyMembership);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, PartyMembershipEditModel editModel)
        {
            PartyMembership partyMembership = await this.partyMembershipsRespository.GetByIdAsync(id);
            if (partyMembership == null)
            {
                return false;
            }

            partyMembership.PartyName = editModel.PartyName;
            partyMembership.Description = editModel.Description;
            partyMembership.IsCurrent = editModel.IsCurrent;
            partyMembership.StartDate = editModel.StartDate;
            partyMembership.EndDate = editModel.EndDate;
            partyMembership.ModifiedOn = DateTime.UtcNow;

            await this.partyMembershipsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<PartyMembershipDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            PartyMembership partyMembership = await this.partyMembershipsRespository.GetByIdAsync(id);
            PartyMembershipDeleteModel deleteModel = this.mapper.Map<PartyMembershipDeleteModel>(partyMembership);

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            PartyMembership partyMembership = await this.partyMembershipsRespository.GetByIdAsync(id);
            if (partyMembership == null)
            {
                return false;
            }

            this.partyMembershipsRespository.Delete(partyMembership);
            await this.partyMembershipsRespository.SaveChangesAsync();

            return partyMembership.IsDeleted;
        }

        public int GetCountFromMember(string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return 0;
            }

            return this.partyMembershipsRespository.All().Where(pm => pm.Citizen.Id == citizen.Id).Count();
        }

        public IEnumerable<PartyMembershipViewModel> GetAllFromMember(string userId)
        {
            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.Member.ApplicationUserId == userId);
            if (citizen == null)
            {
                return null;
            }

            List<PartyMembershipViewModel> partyMemberships = this.partyMembershipsRespository.All()
                .Where(pm => pm.CitizenId == citizen.Id)
                .OrderByDescending(pm => pm.CreatedOn)
                .To<PartyMembershipViewModel>().ToList();

            return partyMemberships;
        }
    }
}