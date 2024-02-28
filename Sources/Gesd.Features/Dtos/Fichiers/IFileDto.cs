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
