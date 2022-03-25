﻿using System.ComponentModel.DataAnnotations;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.SocietyActivities
{
    public class SocietyActivityDetailsViewModel
    {
        [Display(Name = SocietyActivityNameDisplayName)]        
        public string Name { get; set; }

        [Display(Name = SocietyActivityDescriptionDisplayName)]        
        public string Description { get; set; }

        [Display(Name = SocietyActivityResultDisplayName)]        
        public string Result { get; set; }

        [Display(Name = SocietyActivityEventLinkDisplayName)]
        public string? EventLink { get; set; }

        public string CitizenId { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public string CreatedOn { get; set; }

        [Display(Name = ModifiedOnDisplayName)]
        public string? ModifiedOn { get; set; }
    }
}