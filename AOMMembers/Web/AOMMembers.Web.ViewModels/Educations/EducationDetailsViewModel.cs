using System.ComponentModel.DataAnnotations;
using AOMMembers.Web.ViewModels.Qualifications;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Educations
{
    public class EducationDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = EducationDescriptionDisplayName)]       
        public string Description { get; set; }

        [Display(Name = EducationCVLinkDisplayName)]
        public string? CVLink { get; set; }

        public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = EducationQualificationsCountDisplayName)]
        public int QualificationsCount { get; set; }
        
        public QualificationListViewModel Qualifications { get; set; }
    }
}