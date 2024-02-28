using AutoMapper;

using Gesd.Entite.Responses;
using Gesd.Features.Compute.Commands.Fichiers;
using Gesd.Features.Compute.Communs;
using Gesd.Features.Compute.Tools;
using Gesd.Features.Contrats.Repositories;
using Gesd.Features.Dtos.Fichiers;
using Gesd.Features.Dtos.Fichiers.Validators;

using MediatR;

using Microsoft.Extensions.Logging;

using System.Net;

using File = Gesd.Entite.File;

namespace Gesd.Features.Compute.Handlers.Fichiers
{
    public class AddFileCmdHandler : BaseComputeCmdHandler<AddFileCmd>
    {
        public AddFileCmdHandler(IMediator mediator, IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddFileCmdHandler> logger) 
            : base(mediator, unitOfWork, mapper, logger)
        {
        }

        public override async Task<RequestResponse> Handle(AddFileCmd request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Vérification de la Taille du fichier");
            VerifierLaTailleDuFichier(request);

            _logger.LogInformation("Début de Traitement");

            var validator = new AddFile_Validator();
            var result = await validator.ValidateAsync(request.File, cancellationToken).ConfigureAwait(false);

            if (!result.IsValid)
            {
                return ApiResponse<File>.CreateErrorResponse(HttpStatusCode.InternalServerError, "Une erreur est survenue dans le serveur", result.Errors.Select(q => q.ErrorMessage).ToList());
            }

            string? url = null;
            string? fileName = null;

            try
            {
                _logger.LogInformation("Ajout du fichier dans le Blob");
                (fileName, url) = await _unitOfWork.BlobRepository.Add(request.File.File).ConfigureAwait(false);

                _logger.LogInformation("Début de la transaction de sauvegarde ");
                await using var transaction = _unitOfWork.BeginTransaction();
                var dto = ComputeFile.GenererLeDtoDeSauvegarde(request.File, fileName);
                var fileToSave = _mapper.Map<File>(dto);
                var blobdataResponse = await _unitOfWork.FileRepository.Add(fileToSave).ConfigureAwait(false);

                if (blobdataResponse == null)
                {
                    transaction.Rollback();
                    return ApiResponse<File>.CreateErrorResponse(HttpStatusCode.InternalServerError, "Une erreur est survenue dans le serveur", null);
                }

                _logger.LogInformation($"Sauvegarde des métadonnées URL:: {url}, BLOBDATARESPONSE:: {blobdataResponse}");
                await SauvegarderLesMetaData(url, blobdataResponse).ConfigureAwait(false);

                transaction.Commit();

                _logger.LogInformation("Sauvegarde terminée");

                return ApiResponse<File>.CreateSuccessResponse(fileToSave, "File added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Une erreur est survenue lors de la sauvegarde du fichier sélectionné {ex.Message}");
                if (url != null)
                {
                    // Rollback if the blob URL was generated but an exception occurred later
                    _logger.LogWarning($"Suppression du fichier sauvegardé précédemment, une erreur est survenue {url}");
                    _ = _unitOfWork.BlobRepository.Delete(url);
                }

                // Log exception here if needed
                return ApiResponse<File>.CreateErrorResponse(ex, "Un Problème est survenu lors de la sauvegarde du fichier");
            }
        }


        #region PRIVATE FUNCTION

        private async Task SauvegarderLesMetaData(string url, File blobdataResponse)
        {
            var cleDeChiffrement = ComputeFile.GenererCleDeChiffrement();
            var encryptedUrl = ComputeFile.EncryptagedeLUrl(url, cleDeChiffrement);
            var dtoEncryptedFile = ComputeFile.GenererDtoEncFile(blobdataResponse, encryptedUrl);
            var encUrlResponse = await _unitOfWork.EncryptedFileRepository.Add(dtoEncryptedFile).ConfigureAwait(false);
            var dtoKeyStoreDto = ComputeFile.GenererDtoKeyStore(cleDeChiffrement, encUrlResponse.Id);
            await _unitOfWork.KeyStoreRepository.Add(dtoKeyStoreDto).ConfigureAwait(false);
        }

        private static RequestResponse VerifierLaTailleDuFichier(AddFileCmd request)
        {
            const long maxSizeBytes = 5 * 1024 * 1024; // 5 Mo en octets

            if (request.File.File.Length > maxSizeBytes)
            {
                return new RequestResponse((int)HttpStatusCode.BadRequest, false, "Le fichier dépasse la taille maximale autorisée de 5 Mo", null);
            }

            return new RequestResponse();
        }
        #endregion
    }
}
