using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.PartyPositions;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class PartyPositionsService : IPartyPositionsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<PartyPosition> partyPositionsRespository;
        private readonly IDeletableEntityRepository<Member> membersRespository;

        public PartyPositionsService(IMapper mapper, IDeletableEntityRepository<PartyPosition> partyPositionsRespository, IDeletableEntityRepository<Member> membersRespository)
        {
            this.mapper = mapper;
            this.partyPositionsRespository = partyPositionsRespository;
            this.membersRespository = membersRespository;
        }

        public async Task<string> CreateAsync(PartyPositionInputModel inputModel, string userId)
        {
            Member member = this.membersRespository.AllAsNoTracking().FirstOrDefault(m => m.ApplicationUserId == userId);
            if (member == null)
            {
                return PartyPositionCreateWithoutMemberBadResult;
            }

            PartyPosition partyPosition = new PartyPosition
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                IsCurrent = inputModel.IsCurrent,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                MemberId = member.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.partyPositionsRespository.AddAsync(partyPosition);
            await this.partyPositionsRespository.SaveChangesAsync();

            return partyPosition.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            PartyPosition partyPosition = await this.partyPositionsRespository.GetByIdAsync(id);

            return partyPosition == null;
        }

        public async Task<PartyPositionViewModel> GetViewModelByIdAsync(string id)
        {
            PartyPosition partyPosition = await this.partyPositionsRespository.GetByIdAsync(id);
            PartyPositionViewModel viewModel = this.mapper.Map<PartyPositionViewModel>(partyPosition);

            return viewModel;
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

        public async Task<PartyPositionEditModel> GetEditModelByIdAsync(string id)
        {
            PartyPosition partyPosition = await this.partyPositionsRespository.GetByIdAsync(id);
            PartyPositionEditModel editModel = this.mapper.Map<PartyPositionEditModel>(partyPosition);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, PartyPositionEditModel editModel)
        {
            PartyPosition partyPosition = await this.partyPositionsRespository.GetByIdAsync(id);
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

        public async Task<PartyPositionDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            PartyPosition partyPosition = await this.partyPositionsRespository.GetByIdAsync(id);
            PartyPositionDeleteModel deleteModel = this.mapper.Map<PartyPositionDeleteModel>(partyPosition);

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            PartyPosition partyPosition = await this.partyPositionsRespository.GetByIdAsync(id);
            if (partyPosition == null)
            {
                return false;
            }

            this.partyPositionsRespository.Delete(partyPosition);
            await this.partyPositionsRespository.SaveChangesAsync();

            return partyPosition.IsDeleted;
        }

        public int GetCountFromMember(string userId)
        {
            Member member = this.membersRespository.AllAsNoTracking().FirstOrDefault(m => m.ApplicationUserId == userId);
            if (member == null)
            {
                return 0;
            }

            return this.partyPositionsRespository.All().Where(pp => pp.MemberId == member.Id).Count();
        }

        public IEnumerable<PartyPositionViewModel> GetAllFromMember(string userId)
        {
            Member member = this.membersRespository.AllAsNoTracking().FirstOrDefault(m => m.ApplicationUserId == userId);
            if (member == null)
            {
                return null;
            }

            List<PartyPositionViewModel> partyPositions = this.partyPositionsRespository.AllAsNoTracking()
                .Where(pp => pp.MemberId == member.Id)
                .OrderByDescending(pp => pp.CreatedOn)
                .To<PartyPositionViewModel>().ToList();

            return partyPositions;
        }
    }
}