using System.ComponentModel.DataAnnotations;
using AOMMembers.Web.ViewModels.LawProblems;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.LawStates
{
    public class LawStateDetailsViewModel
    {
        [Display(Name = LawStateConditionDisplayName)]
        public string Condition { get; set; }

        public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }

        [Display(Name = LawStateLawProblemsCountDisplayName)]
        public int LawProblemsCount { get; set; }

        public LawProblemListViewModel LawProblems { get; set; }
    }
}