using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.LawStates
{
    public class LawStateViewModel : IMapFrom<LawState>
    {
        public string Id { get; set; }

        [Display(Name = LawStateConditionDisplayName)]
        public string Condition { get; set; }

        //public string CitizenId { get; set; }        

        [Display(Name = LawStateLawProblemsCountDisplayName)]
        public int LawProblemsCount { get; set; }
    }
}