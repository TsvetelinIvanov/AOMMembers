using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Web.ViewModels.PublicImages;

namespace AOMMembers.Services.Data.Services
{
    public class PublicImagesService : IPublicImagesService
    {
        private readonly IDeletableEntityRepository<PublicImage> publicImagesRespository;

        public PublicImagesService(IDeletableEntityRepository<PublicImage> publicImagesRespository)
        {
            this.publicImagesRespository = publicImagesRespository;
        }        

        public async Task<string> CreateAsync(PublicImageInputModel inputModel, string memberId)
        {
            PublicImage publicImage = new PublicImage
            {
                Rating = inputModel.Rating,
                MemberId = memberId,
                CreatedOn = DateTime.UtcNow
            };

            await this.publicImagesRespository.AddAsync(publicImage);
            await this.publicImagesRespository.SaveChangesAsync();

            return publicImage.Id;
        }

        public async Task<PublicImageDetailsViewModel> GetDetailsByIdAsync(string id)
        {
            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(id);
            PublicImageDetailsViewModel detailsViewModel = new PublicImageDetailsViewModel
            {
                Id = publicImage.Id,
                Rating = publicImage.Rating,
                MemberId = publicImage.MemberId,
                CreatedOn = publicImage.CreatedOn,
                ModifiedOn = publicImage.ModifiedOn,
                MediaMaterialsCount = publicImage.MediaMaterials.Count
            };

            return detailsViewModel;
        }

        public async Task<bool> IsFromMember(string id, string userId)
        {
            PublicImage publicImage = await this.publicImagesRespository.GetByIdAsync(id);

            return publicImage.Member.ApplicationUserId == userId;
        }

        public async Task<bool> EditAsync(string id, PublicImageEditModel editModel)
        {
            PublicImage publicImage = this.publicImagesRespository.All().FirstOrDefault(pi => pi.Id == id);
            if (publicImage == null)
            {
                return false;
            }

            publicImage.Rating = editModel.Rating;
            publicImage.ModifiedOn = DateTime.UtcNow;

            await this.publicImagesRespository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            PublicImage publicImage = this.publicImagesRespository.All().FirstOrDefault(pi => pi.Id == id);
            if (publicImage == null)
            {
                return false;
            }

            this.publicImagesRespository.Delete(publicImage);
            await this.publicImagesRespository.SaveChangesAsync();

            return publicImage.IsDeleted;
        }
    }
}