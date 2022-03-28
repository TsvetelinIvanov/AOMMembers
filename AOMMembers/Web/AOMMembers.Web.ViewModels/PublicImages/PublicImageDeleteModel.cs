using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.PublicImages
{
    public class PublicImageDeleteModel
    {
        public string Id { get; set; }

        [Display(Name = PublicImageRatingDisplayName)]
        public int? Rating { get; set; }

        //public string MemberId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }

        [Display(Name = PublicImageMediaMaterialsCountDisplayName)]
        public int MediaMaterialsCount { get; set; }
    }
}