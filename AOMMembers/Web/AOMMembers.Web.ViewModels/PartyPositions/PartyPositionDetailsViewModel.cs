﻿using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.PartyPositions
{
    public class PartyPositionDetailsViewModel
    {
        [Display(Name = PartyPositionNameDisplayName)]        
        public string Name { get; set; }

        [Display(Name = PartyPositionDescriptionDisplayName)]        
        public string Description { get; set; }

        [Display(Name = PartyPositionIsCurrentDisplayName)]
        public bool IsCurrent { get; set; }

        [Display(Name = PartyPositionStartDateDisplayName)]        
        public string StartDate { get; set; }

        [Display(Name = PartyPositionEndDateDisplayName)]        
        public string? EndDate { get; set; }

        public string MemberId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }
    }
}