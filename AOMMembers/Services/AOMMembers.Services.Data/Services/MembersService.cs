using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.Members;
using AOMMembers.Web.ViewModels.Relationships;
using AOMMembers.Web.ViewModels.PartyPositions;

namespace AOMMembers.Services.Data.Services
{
    public class MembersService : IMembersService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Member> membersRespository;
        private readonly IDeletableEntityRepository<PublicImage> publicImagesRespository;
        private readonly IDeletableEntityRepository<MediaMaterial> mediaMaterialsRespository;
        private readonly IDeletableEntityRepository<Relationship> relationshipsRespository;
        private readonly IDeletableEntityRepository<PartyPosition> partyPositionsRespository;

        public MembersService(IMapper mapper, IDeletableEntityRepository<Member> membersRespository, IDeletableEntityRepository<PublicImage> publicImagesRespository, IDeletableEntityRepository<MediaMaterial> mediaMaterialsRespository, IDeletableEntityRepository<Relationship> relationshipsRespository, IDeletableEntityRepository<PartyPosition> partyPositionsRespository)
        {
            this.mapper = mapper;
            this.membersRespository = membersRespository;
            this.publicImagesRespository = publicImagesRespository;
            this.mediaMaterialsRespository = mediaMaterialsRespository;
            this.relationshipsRespository = relationshipsRespository;
            this.partyPositionsRespository = partyPositionsRespository;
        }

        public bool IsCreated(string userId)
        {
            return this.membersRespository.AllAsNoTracking().Any(m => m.ApplicationUserId == userId);
        }

        public async Task<string> CreateAsync(MemberInputModel inputModel, string userId)
        {
            Member member = new Member
            {
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                PhoneNumber = inputModel.PhoneNumber,
                PictureUrl = inputModel.PictureUrl,
                ApplicationUserId = userId,
                CreatedOn = DateTime.UtcNow
            };

            await this.membersRespository.AddAsync(member);
            await this.membersRespository.SaveChangesAsync();

            return member.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            Member member = await this.membersRespository.GetByIdAsync(id);

            return member == null;
        }

        public IEnumerable<MemberViewModel> GetViewModels()
        {
            List<MemberViewModel> memberViewModels = new List<MemberViewModel>();
            IEnumerable<Member> members = this.membersRespository.All();
            foreach (Member member in members)
            {
                PartyPosition partyPosition = member.PartyPositions.FirstOrDefault(pp => pp.IsCurrent);
                string partyPositionName = partyPosition != null ? partyPosition.Name : "---";

                MemberViewModel viewModel = new MemberViewModel
                {
                    Id = member.Id,
                    FullName = member.FullName,
                    Email = member.Email,
                    PhoneNumber = member.PhoneNumber,
                    PictureUrl = member.PictureUrl,
                    CurrentPartyPosition = partyPositionName,
                    RelationshipsCount = member.Relationships.Count
                };

                memberViewModels.Add(viewModel);
            }            

            return memberViewModels;
        }

        public async Task<MemberDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            Member member = await this.membersRespository.GetByIdAsync(id);

            PartyPosition currentPartyPosition = member.PartyPositions.FirstOrDefault(pp => pp.IsCurrent);
            string partyPositionName = currentPartyPosition != null ? currentPartyPosition.Name : "---";

            HashSet<RelationshipViewModel> relationships = new HashSet<RelationshipViewModel>();
            foreach (Relationship relationship in member.Relationships)
            {
                RelationshipViewModel relationshipViewModel = new RelationshipViewModel
                {
                    Id = relationship.Id,
                    Kind = relationship.Kind,
                    Description = relationship.Description,
                    RelationshipCitizenFullName = relationship.Citizen.FirstName + " " + relationship.Citizen.SecondName + " " + relationship.Citizen.LastName
                };
                relationships.Add(relationshipViewModel);
            }

            HashSet<PartyPositionViewModel> partyPositions = new HashSet<PartyPositionViewModel>();
            foreach (PartyPosition partyPosition in member.PartyPositions)
            {
                PartyPositionViewModel partyPositionViewModel = this.mapper.Map<PartyPositionViewModel>(partyPosition);
                partyPositions.Add(partyPositionViewModel);
            }

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
                RelationshipsCount = member.Relationships.Count,
                ApplicationUserId = member.ApplicationUserId,
                CitizenId = member.CitizenId,
                PublicImageId = member.PublicImageId,
                Relationships = relationships,
                PartyPositions = partyPositions
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            Member member = await this.membersRespository.GetByIdAsync(id);

            return member.ApplicationUserId == userId;
        }

        public async Task<MemberEditModel> GetEditModelByIdAsync(string id)
        {
            Member member = await this.membersRespository.GetByIdAsync(id);
            MemberEditModel editModel = this.mapper.Map<MemberEditModel>(member);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, MemberEditModel editModel)
        {
            Member member = await this.membersRespository.GetByIdAsync(id);
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

        public async Task<MemberDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            Member member = await this.membersRespository.GetByIdAsync(id);

            PartyPosition partyPosition = member.PartyPositions.FirstOrDefault(pp => pp.IsCurrent);
            string partyPositionName = partyPosition != null ? partyPosition.Name : "---";

            MemberDeleteModel deleteModel = new MemberDeleteModel
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

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Member member = await this.membersRespository.GetByIdAsync(id);
            if (member == null)
            {
                return false;
            }

            PublicImage publicImage = member.PublicImage;
            if (publicImage != null)
            {
                foreach (MediaMaterial mediaMaterial in publicImage.MediaMaterials)
                {
                    this.mediaMaterialsRespository.Delete(mediaMaterial);
                }

                await this.mediaMaterialsRespository.SaveChangesAsync();

                this.publicImagesRespository.Delete(publicImage);
                await this.publicImagesRespository.SaveChangesAsync();
            }

            foreach (Relationship relationship in member.Relationships)
            {
                this.relationshipsRespository.Delete(relationship);
            }

            await this.relationshipsRespository.SaveChangesAsync();

            foreach (PartyPosition partyPosition in member.PartyPositions)
            {
                this.partyPositionsRespository.Delete(partyPosition);
            }

            await this.partyPositionsRespository.SaveChangesAsync();

            this.membersRespository.Delete(member);
            await this.membersRespository.SaveChangesAsync();

            return member.IsDeleted;
        }
    }
}