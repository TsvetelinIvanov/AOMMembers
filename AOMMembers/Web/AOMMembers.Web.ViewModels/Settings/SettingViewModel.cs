using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AOMMembers.Data.Models;
using AOMMembers.Services.Mapping;
using static AOMMembers.Common.DataDisplayNames;

namespace AOMMembers.Web.ViewModels.Settings
{
    public class SettingViewModel
    {
        public int Id { get; set; }

        [Display(Name = SettingNameDisplayName)]
        public string Name { get; set; }

        [Display(Name = SettingValueDisplayName)]
        public string Value { get; set; }

        //public string CitizenId { get; set; }

        [Display(Name = SettingNameAndValueDisplayName)]
        public string NameAndValue { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Setting, SettingViewModel>().ForMember(
                m => m.NameAndValue,
                opt => opt.MapFrom(nv => nv.Name + " = " + nv.Value));
        }
    }
}