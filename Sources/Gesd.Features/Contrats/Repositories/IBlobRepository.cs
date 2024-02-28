using Microsoft.AspNetCore.Http;

using File = Gesd.Entite.File;

namespace Gesd.Features.Contrats.Repositories
{
    public interface IBlobRepository
    {
        Task<(string fileName, string filePath)> Add(IFormFile file);
        Task<bool> Delete(string url);
        Task<File> Get();
    }
}
