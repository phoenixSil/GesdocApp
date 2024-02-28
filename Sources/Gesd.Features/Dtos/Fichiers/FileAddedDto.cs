namespace Gesd.Features.Dtos.Fichiers
{
    public class FileAddedDto : IFileDto
    {
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public double FileSize { get; set; }
        public int Version { get; set; }
        public string FileType { get; set; }
    }
}
