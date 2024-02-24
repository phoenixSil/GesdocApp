using AutoMapper;
using Gesd.Entite;
using Gesd.Features.Dtos.Fichiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Gesd.Entite.File;

namespace Gesd.Features.Profiles
{
    public class GesdProfile: Profile
    {
        public GesdProfile()
        {
            CreateMap<File, FileToAddDto>().ReverseMap();
            CreateMap<File, FileAddedDto>().ReverseMap();
            CreateMap<File, FileDto>().ReverseMap();
        }
    }
}
