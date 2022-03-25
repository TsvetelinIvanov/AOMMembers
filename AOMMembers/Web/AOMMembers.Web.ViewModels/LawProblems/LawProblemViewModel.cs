﻿using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.LawProblems
{
    public class LawProblemViewModel
    {
        [Display(Name = LawProblemDescriptionDisplayName)]
        public string Description { get; set; }

        [Display(Name = LawProblemLawProblemLinkDisplayName)]
        public string? LawProblemLink { get; set; }

        //public string LawStateId { get; set; }
    }
}