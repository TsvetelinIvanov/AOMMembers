using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Qualifications
{
    public class QualificationDetailsViewModel
    {
        [Display(Name = QualificationNameDisplayName)]        
        public string Name { get; set; }

        [Display(Name = QualificationDescriptionDisplayName)]        
        public string Description { get; set; }

        [Display(Name = QualificationStartDateDisplayName)]        
        public string StartDate { get; set; }

        [Display(Name = QualificationEndDateDisplayName)]        
        public string? EndDate { get; set; }

        public string EducationId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }
    }
}