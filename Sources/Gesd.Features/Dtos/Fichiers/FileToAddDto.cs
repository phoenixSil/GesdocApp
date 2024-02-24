using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Features.Dtos.Fichiers
{
    public class FileToAddDto: IFileDto
    {
        [Required]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public double FileSize { get; set; }
        public string FileType { get; set; }
        public int Version { get; set; }
    }
}
