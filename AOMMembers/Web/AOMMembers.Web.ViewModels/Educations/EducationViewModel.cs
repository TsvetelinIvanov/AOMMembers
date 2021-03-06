using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Educations
{
    public class EducationViewModel : IMapFrom<Education>
    {
        public string Id { get; set; }

        [Display(Name = EducationDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = EducationCVLinkDisplayName)]
        public string? CVLink { get; set; }

        //public string CitizenId { get; set; }       

        [Display(Name = EducationQualificationsCountDisplayName)]
        public int QualificationsCount { get; set; }
    }
}