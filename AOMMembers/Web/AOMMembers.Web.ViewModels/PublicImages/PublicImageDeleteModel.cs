using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.PublicImages
{
    public class PublicImageDeleteModel
    {
        [Display(Name = PublicImageRatingDisplayName)]
        public int? Rating { get; set; }

        //public string MemberId { get; set; }        

        [Display(Name = PublicImageMediaMaterialsCountDisplayName)]
        public int MediaMaterialsCount { get; set; }
    }
}