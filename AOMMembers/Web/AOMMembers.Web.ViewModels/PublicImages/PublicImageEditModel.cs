using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.PublicImages
{
    public class PublicImageEditModel
    {
        public string Id { get; set; }

        [Display(Name = PublicImageRatingDisplayName)]
        public int? Rating { get; set; }

        //public string MemberId { get; set; }
    }
}