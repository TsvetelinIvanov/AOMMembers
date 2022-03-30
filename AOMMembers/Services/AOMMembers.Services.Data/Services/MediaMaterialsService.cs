using AutoMapper;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Mapping;
using AOMMembers.Web.ViewModels.MediaMaterials;

namespace AOMMembers.Services.Data.Services
{
    public class MediaMaterialsService : IMediaMaterialsService
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<MediaMaterial> mediaMaterialsRespository;

        public MediaMaterialsService(IMapper mapper, IDeletableEntityRepository<MediaMaterial> mediaMaterialsRespository)
        {
            this.mapper = mapper;
            this.mediaMaterialsRespository = mediaMaterialsRespository;
        }        

        public async Task<string> CreateAsync(MediaMaterialInputModel inputModel, string publicImageId)
        {
            MediaMaterial mediaMaterial = new MediaMaterial
            {
                Kind = inputModel.Kind,
                MediaName = inputModel.MediaName,
                IssueDate = inputModel.IssueDate,
                Author = inputModel.Author,
                Heading = inputModel.Heading,
                Description = inputModel.Description,
                MediaMaterialLink = inputModel.MediaMaterialLink,
                PublicImageId = publicImageId,
                CreatedOn = DateTime.UtcNow
            };

            await this.mediaMaterialsRespository.AddAsync(mediaMaterial);
            await this.mediaMaterialsRespository.SaveChangesAsync();

            return mediaMaterial.Id;
        }

        public async Task<MediaMaterialDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            MediaMaterial mediaMaterial = await this.mediaMaterialsRespository.GetByIdAsync(id);
            MediaMaterialDetailsViewModel detailsViewModel = this.mapper.Map<MediaMaterialDetailsViewModel>(mediaMaterial);

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            MediaMaterial mediaMaterial = await this.mediaMaterialsRespository.GetByIdAsync(id);

            return mediaMaterial.PublicImage.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, MediaMaterialEditModel editModel)
        {
            MediaMaterial mediaMaterial = this.mediaMaterialsRespository.All().FirstOrDefault(mm => mm.Id == id);
            if (mediaMaterial == null)
            {
                return false;
            }

            mediaMaterial.Kind = editModel.Kind;
            mediaMaterial.MediaName = editModel.MediaName;
            mediaMaterial.IssueDate = editModel.IssueDate;
            mediaMaterial.Author = editModel.Author;
            mediaMaterial.Heading = editModel.Heading;
            mediaMaterial.Description = editModel.Description;
            mediaMaterial.MediaMaterialLink = editModel.MediaMaterialLink;
            mediaMaterial.ModifiedOn = DateTime.UtcNow;

            await this.mediaMaterialsRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            MediaMaterial mediaMaterial = this.mediaMaterialsRespository.All().FirstOrDefault(mm => mm.Id == id);
            if (mediaMaterial == null)
            {
                return false;
            }

            this.mediaMaterialsRespository.Delete(mediaMaterial);
            await this.mediaMaterialsRespository.SaveChangesAsync();

            return mediaMaterial.IsDeleted;
        }

        public int GetCountFromMember(string publicImageId)
        {
            return this.mediaMaterialsRespository.All().Where(mm => mm.PublicImageId == publicImageId).Count();
        }

        public IEnumerable<MediaMaterialViewModel> GetAllFromMember(string publicImageId)
        {
            List<MediaMaterialViewModel> mediaMaterials = this.mediaMaterialsRespository.AllAsNoTracking()
                .Where(mm => mm.PublicImageId == publicImageId)
                .OrderByDescending(mm => mm.CreatedOn)
                .To<MediaMaterialViewModel>().ToList();

            return mediaMaterials;
        }
    }
}