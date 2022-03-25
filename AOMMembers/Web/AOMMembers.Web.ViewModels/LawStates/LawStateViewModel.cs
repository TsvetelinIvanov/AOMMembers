using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.LawStates
{
    public class LawStateViewModel
    {
        [Display(Name = LawStateConditionDisplayName)]
        public string Condition { get; set; }

        //public string CitizenId { get; set; }        

        [Display(Name = LawStateLawProblemsCountDisplayName)]
        public int LawProblemsCount { get; set; }
    }
}