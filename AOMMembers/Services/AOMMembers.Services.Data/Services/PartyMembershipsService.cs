using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.PartyMemberships;

namespace AOMMembers.Services.Data.Services
{
    public class PartyMembershipsService : IPartyMembershipsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<PartyMembership> partyMembershipsRespository;

        public PartyMembershipsService(IMapper mapper, IDeletableEntityRepository<PartyMembership> partyMembershipsRespository)
        {
            this.mapper = mapper;
            this.partyMembershipsRespository = partyMembershipsRespository;
        }        

        public async Task<string> CreateAsync(PartyMembershipInputModel inputModel, string citizenId)
        {
            PartyMembership partyMembership = new PartyMembership
            {
                PartyName = inputModel.PartyName,
                Description = inputModel.Description,
                IsCurrent = inputModel.IsCurrent,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                CitizenId = citizenId,
                CreatedOn = DateTime.UtcNow
            };

            await this.partyMembershipsRespository.AddAsync(partyMembership);
            await this.partyMembershipsRespository.SaveChangesAsync();

            return partyMembership.Id;
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

        public async Task<bool> EditAsync(string id, PartyMembershipEditModel editModel)
        {
            PartyMembership partyMembership = this.partyMembershipsRespository.All().FirstOrDefault(pm => pm.Id == id);
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

        public async Task<bool> DeleteAsync(string id)
        {
            PartyMembership partyMembership = this.partyMembershipsRespository.All().FirstOrDefault(pm => pm.Id == id);
            if (partyMembership == null)
            {
                return false;
            }

            this.partyMembershipsRespository.Delete(partyMembership);
            await this.partyMembershipsRespository.SaveChangesAsync();

            return partyMembership.IsDeleted;
        }

        public int GetCountFromMember(string citizenId)
        {
            return this.partyMembershipsRespository.All().Where(pm => pm.CitizenId == citizenId).Count();
        }

        public IEnumerable<PartyMembershipViewModel> GetAllFromMember(string citizenId)
        {
            List<PartyMembershipViewModel> partyMemberships = this.partyMembershipsRespository.AllAsNoTracking()
                .Where(pm => pm.CitizenId == citizenId)
                .OrderByDescending(pm => pm.CreatedOn)
                .To<PartyMembershipViewModel>().ToList();

            return partyMemberships;
        }
    }
}