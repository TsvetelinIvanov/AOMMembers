using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.PublicImages;
using AOMMembers.Web.ViewModels.MediaMaterials;
using static AOMMembers.Common.DataBadResults;

namespace AOMMembers.Services.Data.Services
{
    public class PublicImagesService : IPublicImagesService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<PublicImage> publicImagesRespository;
        private readonly IDeletableEntityRepository<Member> membersRespository;
        private readonly IDeletableEntityRepository<MediaMaterial> mediaMaterialsRespository;

        public PublicImagesService(IMapper mapper, IDeletableEntityRepository<PublicImage> publicImagesRespository, IDeletableEntityRepository<Member> membersRespository, IDeletableEntityRepository<MediaMaterial> mediaMaterialsRespository)
        {
            this.mapper = mapper;
            this.publicImagesRespository = publicImagesRespository;
            this.membersRespository = membersRespository;
            this.mediaMaterialsRespository = mediaMaterialsRespository;
        }

        public bool IsCreated(string userId)
        {
            return this.publicImagesRespository.AllAsNoTracking().Any(pi => pi.Member.ApplicationUserId == userId);
        }

        public async Task<string> CreateAsync(PublicImageInputModel inputModel, string userId)
        {
            Member member = this.membersRespository.AllAsNoTracking().FirstOrDefault(m => m.ApplicationUserId == userId);
            if (member == null)
            {
                return PublicImageCreateWithoutMemberBadResult;
            }

            PublicImage publicImage = new PublicImage
            {
                Rating = inputModel.Rating,
                MemberId = member.Id,
                CreatedOn = DateTime.UtcNow
            };

            await this.publicImagesRespository.AddAsync(publicImage);
            await this.publicImagesRespository.SaveChangesAsync();

            return publicImage.Id;
        }

        public async Task<bool> IsAbsent(string id)
        {
            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(id);

            return publicImage == null;
        }

        public async Task<PublicImageViewModel> GetViewModelByIdAsync(string id)
        {
            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(id);
            PublicImageViewModel viewModel = new PublicImageViewModel
            {
                Id = publicImage.Id,
                Rating = publicImage.Rating,                
                MediaMaterialsCount = publicImage.MediaMaterials.Count
            };

            return viewModel;
        }

        public async Task<PublicImageDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(id);

            HashSet<MediaMaterialViewModel> mediaMaterials = new HashSet<MediaMaterialViewModel>();
            foreach (MediaMaterial mediaMaterial in publicImage.MediaMaterials)
            {
                MediaMaterialViewModel mediaMaterialViewModel = this.mapper.Map<MediaMaterialViewModel>(mediaMaterial);
                mediaMaterials.Add(mediaMaterialViewModel);
            }

            PublicImageDetailsViewModel detailsViewModel = new PublicImageDetailsViewModel
            {
                Id = publicImage.Id,
                Rating = publicImage.Rating,
                MemberId = publicImage.MemberId,
                CreatedOn = publicImage.CreatedOn,
                ModifiedOn = publicImage.ModifiedOn,
                MediaMaterialsCount = publicImage.MediaMaterials.Count,
                MediaMaterials = mediaMaterials
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(id);

            return publicImage.Member.ApplicationUserId == userId;
        }

        public async Task<PublicImageEditModel> GetEditModelByIdAsync(string id)
        {
            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(id);
            PublicImageEditModel editModel = this.mapper.Map<PublicImageEditModel>(publicImage);

            return editModel;
        }

        public async Task<bool> EditAsync(string id, PublicImageEditModel editModel)
        {
            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(id);
            if (publicImage == null)
            {
                return false;
            }

            publicImage.Rating = editModel.Rating;
            publicImage.ModifiedOn = DateTime.UtcNow;

            await this.publicImagesRespository.SaveChangesAsync();

            return true;
        }

        public async Task<PublicImageDeleteModel> GetDeleteModelByIdAsync(string id)
        {
            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(id);
            PublicImageDeleteModel deleteModel = new PublicImageDeleteModel
            {
                Id = publicImage.Id,
                Rating = publicImage.Rating,                
                CreatedOn = publicImage.CreatedOn,
                ModifiedOn = publicImage.ModifiedOn,
                MediaMaterialsCount = publicImage.MediaMaterials.Count
            };

            return deleteModel;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(id);
            if (publicImage == null)
            {
                return false;
            }

            foreach (MediaMaterial mediaMaterial in publicImage.MediaMaterials)
            {
                this.mediaMaterialsRespository.Delete(mediaMaterial);
            }

            await this.mediaMaterialsRespository.SaveChangesAsync();

            this.publicImagesRespository.Delete(publicImage);
            await this.publicImagesRespository.SaveChangesAsync();

            return publicImage.IsDeleted;
        }
    }
}