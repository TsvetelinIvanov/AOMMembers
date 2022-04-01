using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Relationships
{
    public class RelationshipViewModel : IMapFrom<Relationship>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = RelationshipKindDisplayName)]
        public string Kind { get; set; }

        [Display(Name = RelationshipDescriptionDisplayName)]
        public string Description { get; set; }

        //public string MemberId { get; set; }

        //public string CitizenId { get; set; }

        [Display(Name = RelationshipCitizenFullNameDisplayName)]
        public string RelationshipCitizenFullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Relationship, RelationshipViewModel>().ForMember(
                m => m.RelationshipCitizenFullName,
                opt => opt.MapFrom(r => r.Citizen.FirstName + " " + r.Citizen.SecondName + " " + r.Citizen.LastName));
        }
    }
}