using AutoMapper;

using Gesd.Entite.Responses;
using Gesd.Features.Compute.Commands.Fichiers;
using Gesd.Features.Compute.Communs;
using Gesd.Features.Contrats.Repositories;

using File = Gesd.Entite.File;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Gesd.Features.Compute.Handlers.Fichiers
{
    public class GetFileCmdHandler : BaseComputeCmdHandler<GetFileCmd>
    {
        public GetFileCmdHandler(IMediator mediator, IUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseComputeCmdHandler<GetFileCmd>> logger) : base(mediator, unitOfWork, mapper, logger)
        {
        }

        public async override Task<RequestResponse> Handle(GetFileCmd request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Debut de Traitement");
            try
            {
                _logger.LogInformation("Ajout du fichier dans le Blob");

                var listFichier = await _unitOfWork.FileRepository.Get().ConfigureAwait(false);

                if (listFichier?.Any() != true)
                {
                    return ApiResponse<IEnumerable<File>>.CreateNotFoundResponse("Empty Database");
                }

                return ApiResponse<IEnumerable<File>>.CreateSuccessResponse(listFichier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Une erreur est survenue lors de la lecture des fichiers {ex.Message}");
                return ApiResponse<IEnumerable<File>>.CreateErrorResponse(ex, "Un problème est survenu lors de la lecture des fichiers");
            }
        }
    }
}
