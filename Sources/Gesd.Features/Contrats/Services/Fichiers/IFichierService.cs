using Gesd.Entite;
using Gesd.Entite.Responses;
using Gesd.Features.Dtos.Fichiers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Features.Contrats.Services.Fichiers
{
    public interface IFichierService
    {
        Task<ApiResponse<FileAddedDto>?> Add(IFormFile fichier);
    }
}
