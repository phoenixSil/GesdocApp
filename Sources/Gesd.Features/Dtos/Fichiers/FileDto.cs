using Microsoft.AspNetCore.Http;

namespace Gesd.Features.Dtos.Fichiers
{
    public class FileDto : IFileDto
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public double FileSize { get; set; }
        public string FileType { get; set; }
        public int Version { get; set; }
    }
}
