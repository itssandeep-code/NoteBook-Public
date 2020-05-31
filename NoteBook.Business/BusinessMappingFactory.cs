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

            CreateMap<Contact, Models.Contact>()
                .ReverseMap()
                .ForMember(dest => dest.Address, source => source.MapFrom(src => src.Address))
                .ForMember(dest => dest.AlternateAddress, source => source.MapFrom(src => src.AlternateAddress))
                .ForMember(dest => dest.AlternateMobile, source => source.MapFrom(src => src.AlternateMobile))
                .ForMember(dest => dest.ContactTypeId, source => source.MapFrom(src => src.ContactTypeId))
                .ForMember(dest => dest.ContactWith, source => source.MapFrom(src => src.ContactWith))
                .ForMember(dest => dest.DateOfBirth, source => source.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.DateOfMarriage, source => source.MapFrom(src => src.DateOfMarriage))
                .ForMember(dest => dest.Email, source => source.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, source => source.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastName, source => source.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Mobile, source => source.MapFrom(src => src.Mobile))
                .ForMember(dest => dest.PhotoPath, source => source.MapFrom(src => src.PhotoPath))
                .ForMember(dest => dest.Title, source => source.MapFrom(src => src.Title))
                .ForMember(dest => dest.UserId, source => source.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CreatedBy, source => source.MapFrom(src => src.CreatedBy))
               .ForMember(dest => dest.CreatedOn, source => source.MapFrom(src => src.CreatedOn)); 

            CreateMap<ContactType, Models.ContactType>()
             .ReverseMap()
             .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
             .ForMember(dest => dest.TypeName, source => source.MapFrom(src => src.TypeName));
        }
    }
}
