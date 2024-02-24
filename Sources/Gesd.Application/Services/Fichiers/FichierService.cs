using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gesd.Entite.Responses;
using Gesd.Features.Compute.Commands.Fichiers;
using Gesd.Features.Contrats.Services.Fichiers;
using Gesd.Features.Dtos.Fichiers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Gesd.Application.Services.Fichiers
{
    public class FichierService : IFichierService
    {
        private readonly IMediator _sender;

        public FichierService(IMediator sender)
        {
            _sender  = sender;
        }

        public async Task<ApiResponse<FileAddedDto>?> Add(IFormFile file)
        {
            var fileToAdd = new FileToAddDto { File = file };
            var response = await _sender.Send(new AddFileCmd { File = fileToAdd }).ConfigureAwait(false);
            return response as ApiResponse<FileAddedDto>;
        }
    }
}
