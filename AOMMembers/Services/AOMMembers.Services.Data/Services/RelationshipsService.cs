using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.Relationships;

namespace AOMMembers.Services.Data.Services
{
    public class RelationshipsService : IRelationshipsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Relationship> relationshipsRespository;

        public RelationshipsService(IMapper mapper, IDeletableEntityRepository<Relationship> relationshipsRespository)
        {
            this.mapper = mapper;
            this.relationshipsRespository = relationshipsRespository;
        }        

        public async Task<string> CreateAsync(RelationshipInputModel inputModel, string memberId, string citizenId)
        {
            Relationship relationship = new Relationship
            {
                Kind = inputModel.Kind,
                Description = inputModel.Description,
                MemberId = memberId,
                CitizenId = citizenId,
                CreatedOn = DateTime.UtcNow
            };

            await this.relationshipsRespository.AddAsync(relationship);
            await this.relationshipsRespository.SaveChangesAsync();

            return relationship.Id;
        }

        public async Task<RelationshipDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Relationship relationship = await this.relationshipsRespository.GetByIdAsync(id);
            RelationshipDetailsViewModel detailsViewModel = this.mapper.Map<RelationshipDetailsViewModel>(relationship);

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

        public int GetCountFromMember(string memberId, string citizenId)
        {
            return this.relationshipsRespository.All().Where(r => r.MemberId == memberId && r.CitizenId == citizenId).Count();
        }

        public IEnumerable<RelationshipViewModel> GetAllFromMember(string memberId, string citizenId)
        {
            List<RelationshipViewModel> relationships = this.relationshipsRespository.AllAsNoTracking()
                .Where(r => r.MemberId == memberId && r.CitizenId == citizenId)
                .OrderByDescending(r => r.CreatedOn)
                .To<RelationshipViewModel>().ToList();

            return relationships;
        }
    }
}