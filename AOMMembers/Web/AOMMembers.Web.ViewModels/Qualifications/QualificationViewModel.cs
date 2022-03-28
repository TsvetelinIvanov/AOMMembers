using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Qualifications
{
    public class QualificationViewModel
    {
        public string Id { get; set; }

        [Display(Name = QualificationNameDisplayName)]
        public string Name { get; set; }

        [Display(Name = QualificationDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = QualificationStartDateDisplayName)]
        public string StartDate { get; set; }

        [Display(Name = QualificationEndDateDisplayName)]
        public string? EndDate { get; set; }

        //public string EducationId { get; set; }
    }
}