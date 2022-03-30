using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Qualifications
{
    public class QualificationViewModel : IMapFrom<Qualification>
    {
        public string Id { get; set; }

        [Display(Name = QualificationNameDisplayName)]
        public string Name { get; set; }

        [Display(Name = QualificationDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = QualificationStartDateDisplayName)]
        public DateTime StartDate { get; set; }

        [Display(Name = QualificationEndDateDisplayName)]
        public DateTime? EndDate { get; set; }

        //public string EducationId { get; set; }
    }
}