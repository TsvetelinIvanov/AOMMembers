using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Members;

namespace AOMMembers.Services.Data.Services
{
    public class MembersService : IMembersService
    {
        private readonly IDeletableEntityRepository<Member> membersRespository;

        public MembersService(IDeletableEntityRepository<Member> membersRespository)
        {
            this.membersRespository = membersRespository;
        }        

        public async Task<string> CreateAsync(MemberInputModel inputModel, string applicationUserId)
        {
            Member member = new Member
            {
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                PhoneNumber = inputModel.PhoneNumber,
                PictureUrl = inputModel.PictureUrl,
                ApplicationUserId = applicationUserId,
                CreatedOn = DateTime.UtcNow
            };

            await this.membersRespository.AddAsync(member);
            await this.membersRespository.SaveChangesAsync();

            return member.Id;
        }

        public async Task<MemberDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Member member = await this.membersRespository.GetByIdAsync(id);

            PartyPosition partyPosition = member.PartyPositions.FirstOrDefault(pp => pp.IsCurrent);
            string partyPositionName = partyPosition != null ? partyPosition.Name : "---";

            MemberDetailsViewModel detailsViewModel = new MemberDetailsViewModel
            {
                Id = member.Id,
                FullName = member.FullName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                PictureUrl = member.PictureUrl,                
                CreatedOn = member.CreatedOn,
                ModifiedOn = member.ModifiedOn,
                CurrentPartyPosition = partyPositionName,                
                RelationshipsCount = member.Relationships.Count
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Member member = await this.membersRespository.GetByIdAsync(id);

            return member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, MemberEditModel editModel)
        {
            Member member = this.membersRespository.All().FirstOrDefault(m => m.Id == id);
            if (member == null)
            {
                return false;
            }

            member.FullName = editModel.FullName;
            member.Email = editModel.Email;
            member.PhoneNumber = editModel.PhoneNumber;
            member.PictureUrl = editModel.PictureUrl;
            member.ModifiedOn = DateTime.UtcNow;

            await this.membersRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Member member = this.membersRespository.All().FirstOrDefault(m => m.Id == id);
            if (member == null)
            {
                return false;
            }

            this.membersRespository.Delete(member);
            await this.membersRespository.SaveChangesAsync();

            return member.IsDeleted;
        }
    }
}