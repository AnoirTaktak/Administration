using Administration.Dtos;
using Administration.Models;
using AutoMapper;

namespace Administration.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          
            CreateMap<Societe, SocieteDto>()
            .ForMember(dest => dest.CachetSignature, opt => opt.Ignore());

            CreateMap<SocieteDto, Societe>().ForMember(src => src.CachetSignature, opt => opt.Ignore());

            CreateMap<Document, DocumentDto>()
           .ForMember(dest => dest.Doc_Pdf, opt => opt.Ignore());

            CreateMap<DocumentDto, Document>().ForMember(src => src.Doc_Pdf, opt => opt.Ignore());

        }
    }
}
