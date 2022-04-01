using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.Relationships;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class RelationshipsService : IRelationshipsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Relationship> relationshipsRespository;
        private readonly IDeletableEntityRepository<Member> membersRespository;
        private readonly IDeletableEntityRepository<Citizen> citizensRespository;

        public RelationshipsService(IMapper mapper, IDeletableEntityRepository<Relationship> relationshipsRespository, IDeletableEntityRepository<Member> membersRespository, IDeletableEntityRepository<Citizen> citizensRespository)
        {
            this.mapper = mapper;
            this.relationshipsRespository = relationshipsRespository;
            this.membersRespository = membersRespository;
            this.citizensRespository = citizensRespository;
        }        

        public async Task<string> CreateAsync(RelationshipInputModel inputModel, string userId)
        {
            Member member = this.membersRespository.AllAsNoTracking().FirstOrDefault(m => m.ApplicationUserId == userId);
            if (member == null)
            {
                return RelationshipCreateWithoutMemberBadResult;
            }

            Citizen citizen = this.citizensRespository.AllAsNoTracking().FirstOrDefault(c => c.FirstName == inputModel.FirstName && c.SecondName == inputModel.SecondName && c.LastName == inputModel.LastName && c.Age == inputModel.Age && c.BirthDate == inputModel.BirthDate);
            if (citizen == null)
            {
                return RelationshipCreateWithInexistantCitizenBadResult;
            }

            Relationship relationship = new Relationship
            {
                Kind = inputModel.Kind,
                Description = inputModel.Description,
                MemberId = member.Id,
                CitizenId = citizen.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.relationshipsRespository.AddAsync(relationship);
            await this.relationshipsRespository.SaveChangesAsync();

            return relationship.Id;
        }

        public async Task<RelationshipDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Relationship relationship = await this.relationshipsRespository.GetByIdAsync(id);
            RelationshipDetailsViewModel detailsViewModel = new RelationshipDetailsViewModel
            {
                Id = relationship.Id,
                Kind = relationship.Kind,
                Description = relationship.Description,
                MemberId = relationship.MemberId,
                CitizenId = relationship.CitizenId,
                CreatedOn = relationship.CreatedOn,
                ModifiedOn = relationship.ModifiedOn,
                RelationshipCitizenFullName = relationship.Citizen.FirstName + " " + relationship.Citizen.SecondName + " " + relationship.Citizen.LastName
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Relationship relationship = await this.relationshipsRespository.GetByIdAsync(id);

            return relationship.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, RelationshipEditModel editModel)
        {
            Relationship relationship = this.relationshipsRespository.All().FirstOrDefault(r => r.Id == id);
            if (relationship == null)
            {
                return false;
            }

            relationship.Kind = editModel.Kind;
            relationship.Description = editModel.Description;
            relationship.ModifiedOn = DateTime.UtcNow;

            await this.relationshipsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Relationship relationship = this.relationshipsRespository.All().FirstOrDefault(r => r.Id == id);
            if (relationship == null)
            {
                return false;
            }

            this.relationshipsRespository.Delete(relationship);
            await this.relationshipsRespository.SaveChangesAsync();

            return relationship.IsDeleted;
        }

        public int GetCountFromMember(string userId)
        {
            Member member = this.membersRespository.AllAsNoTracking().FirstOrDefault(m => m.ApplicationUserId == userId);
            if (member == null)
            {
                return 0;
            }            

            return this.relationshipsRespository.All().Where(r => r.MemberId == member.Id).Count();
        }

        public IEnumerable<RelationshipViewModel> GetAllFromMember(string userId)
        {
            Member member = this.membersRespository.AllAsNoTracking().FirstOrDefault(m => m.ApplicationUserId == userId);
            if (member == null)
            {
                return null;
            }

            List<RelationshipViewModel> relationships = this.relationshipsRespository.AllAsNoTracking()
                .Where(r => r.MemberId == member.Id)
                .OrderByDescending(r => r.CreatedOn)
                .To<RelationshipViewModel>().ToList();

            return relationships;
        }
    }
}