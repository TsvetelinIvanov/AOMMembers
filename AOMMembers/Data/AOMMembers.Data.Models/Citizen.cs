using System.ComponentModel.DataAnnotations;
using AOMMembers.Data.Common.Models;
using static AOMMembers.Common.DataConstants;

namespace AOMMembers.Data.Models
{
    public class Citizen : BaseDeletableModel
    {

        [Required]
        [StringLength(CitizenFirstNameMaxLength, MinimumLength = CitizenFirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(CitizenSecondNameMaxLength, MinimumLength = CitizenSecondNameMinLength)]
        public string SecondName { get; set; }

        [Required]
        [StringLength(CitizenLastNameMaxLength, MinimumLength = CitizenLastNameMinLength)]
        public string LastName { get; set; }

        [Required]
        [StringLength(CitizenGenderMaxLength, MinimumLength = CitizenGenderMinLength)]
        public string Gender { get; set; }

        [Required]
        [Range(CitizenAgeMinValue, CitizenAgeMaxValue)]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeathDate { get; set; }

        public string? MemberId { get; set; }
        public virtual Member? Member { get; set; }

        public string? EducationId { get; set; }
        public virtual Education? Education { get; set; }

        public string? CareerId { get; set; }
        public virtual Career? Career { get; set; }

        public string? MaterialStateId { get; set; }
        public virtual MaterialState? MaterialState { get; set; }

        public string? LawStateId { get; set; }
        public virtual LawState? LawState { get; set; }

        public string? WorldviewId { get; set; }
        public virtual Worldview? Worldview { get; set; }

        public virtual ICollection<PartyMembership> PartyMemberships { get; set; } = new HashSet<PartyMembership>();

        public virtual ICollection<SocietyHelp> SocietyHelps { get; set; } = new HashSet<SocietyHelp>();

        public virtual ICollection<SocietyActivity> SocietyActivities { get; set; } = new HashSet<SocietyActivity>();

        public virtual ICollection<Setting> Settings { get; set; } = new HashSet<Setting>();
    }
}