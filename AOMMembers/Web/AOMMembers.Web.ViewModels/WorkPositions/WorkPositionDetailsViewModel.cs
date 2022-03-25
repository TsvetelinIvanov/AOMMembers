﻿using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.WorkPositions
{
    public class WorkPositionDetailsViewModel
    {
        [Display(Name = WorkPositionNameDisplayName)]        
        public string Name { get; set; }

        [Display(Name = WorkPositionDescriptionDisplayName)]        
        public string Description { get; set; }

        [Display(Name = WorkPositionIsCurrentDisplayName)]
        public bool IsCurrent { get; set; }

        [Display(Name = WorkPositionStartDateDisplayName)]        
        public string StartDate { get; set; }

        [Display(Name = WorkPositionEndDateDisplayName)]        
        public string? EndDate { get; set; }

        public string CareerId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }
    }
}