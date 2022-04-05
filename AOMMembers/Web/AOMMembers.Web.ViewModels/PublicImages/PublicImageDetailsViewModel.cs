using System.ComponentModel.DataAnnotations;
using AOMMembers.Web.ViewModels.MediaMaterials;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.PublicImages
{
    public class PublicImageDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = PublicImageRatingDisplayName)]
        public int? Rating { get; set; }

        public string MemberId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = PublicImageMediaMaterialsCountDisplayName)]
        public int MediaMaterialsCount { get; set; }

        public HashSet<MediaMaterialViewModel> MediaMaterials { get; set; }
    }
}