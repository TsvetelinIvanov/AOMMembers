using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.MediaMaterials
{
    public class MediaMaterialDetailsViewModel
    {
        [Display(Name = MediaMaterialKindDisplayName)]        
        public string Kind { get; set; }

        [Display(Name = MediaMaterialMediaNameDisplayName)]        
        public string MediaName { get; set; }

        [Display(Name = MediaMaterialIssueDateDisplayName)]        
        public string IssueDate { get; set; }

        [Display(Name = MediaMaterialAuthorDisplayName)]        
        public string Author { get; set; }

        [Display(Name = MediaMaterialHeadingDisplayName)]        
        public string Heading { get; set; }

        [Display(Name = MediaMaterialDescriptionDisplayName)]        
        public string Description { get; set; }

        [Display(Name = MediaMaterialMediaMaterialLinkDisplayName)]
        public string? MediaMaterialLink { get; set; }

        public string PublicImageId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }
    }
}