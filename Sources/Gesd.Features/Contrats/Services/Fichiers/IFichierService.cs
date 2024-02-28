using Gesd.Entite.Responses;
using Gesd.Features.Dtos.Fichiers;

using Microsoft.AspNetCore.Http;

namespace Gesd.Features.Contrats.Services.Fichiers
{
    public interface IFichierService
    {
        Task<ApiResponse<FileAddedDto>?> Add(IFormFile fichier);
        Task<ApiResponse<bool>> Delete(Guid id);
        Task<ApiResponse<List<FileDto>>> Get();
    }
}
