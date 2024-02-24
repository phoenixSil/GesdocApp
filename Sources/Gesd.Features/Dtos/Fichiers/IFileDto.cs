using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Features.Dtos.Fichiers
{
    public interface IFileDto
    {
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public string FileType { get; set; }
        public int Version { get; set; }
    }
}
