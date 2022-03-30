using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.PartyPositions;

namespace AOMMembers.Services.Data.Services
{
    public class PartyPositionsService : IPartyPositionsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<PartyPosition> partyPositionsRespository;

        public PartyPositionsService(IMapper mapper, IDeletableEntityRepository<PartyPosition> partyPositionsRespository)
        {
            this.mapper = mapper;
            this.partyPositionsRespository = partyPositionsRespository;
        }       

        public async Task<string> CreateAsync(PartyPositionInputModel inputModel, string memberId)
        {
            PartyPosition partyPosition = new PartyPosition
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                IsCurrent = inputModel.IsCurrent,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                MemberId = memberId,
                CreatedOn = DateTime.UtcNow
            };

            await this.partyPositionsRespository.AddAsync(partyPosition);
            await this.partyPositionsRespository.SaveChangesAsync();

            return partyPosition.Id;
        }

        public async Task<PartyPositionDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            PartyPosition partyPosition = await this.partyPositionsRespository.GetByIdAsync(id);
            PartyPositionDetailsViewModel detailsViewModel = this.mapper.Map<PartyPositionDetailsViewModel>(partyPosition);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            PartyPosition partyPosition = await this.partyPositionsRespository.GetByIdAsync(id);

            return partyPosition.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, PartyPositionEditModel editModel)
        {
            PartyPosition partyPosition = this.partyPositionsRespository.All().FirstOrDefault(pp => pp.Id == id);
            if (partyPosition == null)
            {
                return false;
            }

            partyPosition.Name = editModel.Name;
            partyPosition.Description = editModel.Description;
            partyPosition.IsCurrent = editModel.IsCurrent;
            partyPosition.StartDate = editModel.StartDate;
            partyPosition.EndDate = editModel.EndDate;
            partyPosition.ModifiedOn = DateTime.UtcNow;

            await this.partyPositionsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            PartyPosition partyPosition = this.partyPositionsRespository.All().FirstOrDefault(pp => pp.Id == id);
            if (partyPosition == null)
            {
                return false;
            }

            this.partyPositionsRespository.Delete(partyPosition);
            await this.partyPositionsRespository.SaveChangesAsync();

            return partyPosition.IsDeleted;
        }

        public int GetCountFromMember(string memberId)
        {
            return this.partyPositionsRespository.All().Where(pp => pp.MemberId == memberId).Count();
        }

        public IEnumerable<PartyPositionViewModel> GetAllFromMember(string memberId)
        {
            List<PartyPositionViewModel> partyPositions = this.partyPositionsRespository.AllAsNoTracking()
                .Where(pp => pp.MemberId == memberId)
                .OrderByDescending(pp => pp.CreatedOn)
                .To<PartyPositionViewModel>().ToList();

            return partyPositions;
        }
    }
}