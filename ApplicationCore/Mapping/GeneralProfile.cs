using ApplicationCore.Domain;
using ApplicationCore.DTOs;
using AutoMapper;

namespace ApplicationCore.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<PersonRequest, Person>().ForMember(dest => dest.Skills, opts => opts.MapFrom(src => src.Skills.Select(x => new Skill() { Name = x.Name, Level = x.Level })));
            CreateMap<Person, PersonRequest>().ForMember(dest => dest.Skills, opts => opts.MapFrom(src => src.Skills.Select(x => new SkillRequest() { Name = x.Name, Level = x.Level })));

            CreateMap<PersonResponse, Person>().ForMember(dest => dest.Skills, opts => opts.MapFrom(src => src.Skills.Select(x => new Skill() { Name = x.Name, Level = x.Level })));
            CreateMap<Person, PersonResponse>().ForMember(dest => dest.Skills, opts => opts.MapFrom(src => src.Skills.Select(x => new SkillResponse() { Name = x.Name, Level = x.Level })));
        }
    }
}
