using System.ComponentModel.DataAnnotations;
using AOMMembers.Web.ViewModels.LawProblems;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.LawStates
{
    public class LawStateDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = LawStateConditionDisplayName)]
        public string Condition { get; set; }

        public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = LawStateLawProblemsCountDisplayName)]
        public int LawProblemsCount { get; set; }

        public HashSet<LawProblemViewModel> LawProblems { get; set; }
    }
}