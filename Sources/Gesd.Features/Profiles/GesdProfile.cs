using AutoMapper;

using Gesd.Features.Dtos.Fichiers;

using File = Gesd.Entite.File;

namespace Gesd.Features.Profiles
{
    public class GesdProfile : Profile
    {
        public GesdProfile()
        {
            CreateMap<File, FileToAddDto>().ReverseMap();
            CreateMap<File, FileAddedDto>().ReverseMap();
            CreateMap<File, FileDto>().ReverseMap();
        }
    }
}
