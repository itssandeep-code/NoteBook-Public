using AutoMapper;
using NoteBook.Data.EntityModels;
namespace NoteBook.Business
{
    public class BusinessMappingFactory : Profile
    {
        public BusinessMappingFactory()
        {
            CreateMap<Note, Models.Note>()
               .ReverseMap()
               .ForMember(dest => dest.Subject, source => source.MapFrom(src => src.Subject.ToString()))
               .ForMember(dest => dest.RemindMeOn, source => source.MapFrom(src => src.RemindMeOn))
               .ForMember(dest => dest.IsActive, source => source.MapFrom(src => src.IsActive))
               .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
               .ForMember(dest => dest.Description, source => source.MapFrom(src => src.Description))
               .ForMember(dest => dest.CreatedBy, source => source.MapFrom(src => src.CreatedBy))
               .ForMember(dest => dest.CreatedOn, source => source.MapFrom(src => src.CreatedOn));
        }
    }
}
