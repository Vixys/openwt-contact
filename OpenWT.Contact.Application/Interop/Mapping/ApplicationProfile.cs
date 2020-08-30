using AutoMapper;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Data.Entity;

namespace OpenWT.Contact.Application.Interop.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ContactEntity, ContactDto>();
            CreateMap<ContactDto, ContactEntity>();
            
            CreateMap<SkillEntity, SkillDto>();
            CreateMap<SkillDto, SkillEntity>();
        }
    }
}