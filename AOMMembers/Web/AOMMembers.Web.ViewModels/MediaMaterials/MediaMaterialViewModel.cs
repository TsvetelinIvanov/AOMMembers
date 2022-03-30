using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.MediaMaterials
{
    public class MediaMaterialViewModel : IMapFrom<MediaMaterial>
    {
        public string Id { get; set; }

        [Display(Name = MediaMaterialKindDisplayName)]
        public string Kind { get; set; }

        [Display(Name = MediaMaterialMediaNameDisplayName)]
        public string MediaName { get; set; }

        [Display(Name = MediaMaterialIssueDateDisplayName)]
        public DateTime IssueDate { get; set; }

        [Display(Name = MediaMaterialAuthorDisplayName)]
        public string Author { get; set; }

        [Display(Name = MediaMaterialHeadingDisplayName)]
        public string Heading { get; set; }

        [Display(Name = MediaMaterialDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = MediaMaterialMediaMaterialLinkDisplayName)]
        public string? MediaMaterialLink { get; set; }

        //public string PublicImageId { get; set; }
    }
}