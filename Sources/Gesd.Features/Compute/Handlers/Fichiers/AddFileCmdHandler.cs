using AutoMapper;
using Gesd.Entite;
using Gesd.Entite.Responses;
using Gesd.Features.Compute.Commands.Fichiers;
using Gesd.Features.Compute.Communs;
using Gesd.Features.Compute.Tools;
using Gesd.Features.Contrats.Repositories;
using Gesd.Features.Dtos.Fichiers.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using File = Gesd.Entite.File;

namespace Gesd.Features.Compute.Handlers.Fichiers
{
    public class AddFileCmdHandler : BaseComputeCmdHandler<AddFileCmd>
    {
        public AddFileCmdHandler(IMediator mediator, IUnitOfWork unitOfWork, IMapper mapper) : base(mediator, unitOfWork, mapper)
        {
        }

        public override async Task<RequestResponse> Handle(AddFileCmd request, CancellationToken cancellationToken)
        {
            if (request.File.File.Length > 5 * 1024 * 1024) // 5 Mo en octets
            {
                return new RequestResponse((int)HttpStatusCode.BadRequest, false, "Le fichier dépasse la taille maximale autorisée de 5 Mo", null);
            }

            var validator = new AddFile_Validator();
            var result = await validator.ValidateAsync(request.File, cancellationToken).ConfigureAwait(false);

            if (!result.IsValid)
            {
                return new RequestResponse((int)HttpStatusCode.InternalServerError, false, "Une erreur est survenue dans le serveur", result.Errors.Select(q => q.ErrorMessage).ToList());
            }

            string url = null;
            string fileName = null;

            try
            {
                (fileName, url) = await _unitOfWork.BlobRepository.Add(request.File.File).ConfigureAwait(false);

                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    var dto = ComputeFile.GenererLeDtoDeSauvegarde(request.File, fileName);
                    var fileToSave = _mapper.Map<File>(dto);
                    var blobdataResponse = await _unitOfWork.FileRepository.Add(fileToSave).ConfigureAwait(false);

                    if (blobdataResponse == null)
                    {
                        transaction.Rollback();
                        return new RequestResponse((int)HttpStatusCode.InternalServerError, false, "Une erreur est survenue dans le serveur", null);
                    }

                    var cleDeChiffrement = ComputeFile.GenererCleDeChiffrement();
                    var encryptedUrl = ComputeFile.EncryptagedeLUrl(url, cleDeChiffrement);
                    var dtoEncryptedFile = ComputeFile.GenererDtoEncFile(blobdataResponse, encryptedUrl);
                    var encUrlResponse = await _unitOfWork.EncryptedFileRepository.Add(dtoEncryptedFile).ConfigureAwait(false);
                    var dtoKeyStoreDto = ComputeFile.GenererDtoKeyStore(cleDeChiffrement, encUrlResponse.Id);
                    await _unitOfWork.KeyStoreRepository.Add(dtoKeyStoreDto).ConfigureAwait(false);

                    transaction.Commit();

                    return new RequestResponse((int)HttpStatusCode.OK, true, "File added successfuly", null);
                }
            }
            catch (Exception ex)
            {
                if (url != null)
                {
                    // Rollback if the blob URL was generated but an exception occurred later
                    _unitOfWork.BlobRepository.Delete(url);
                }

                // Log exception here if needed
                return new RequestResponse((int)HttpStatusCode.InternalServerError, false, "Une erreur est survenue dans le serveur", new List<string> { ex.Message });
            }
        }

    }
}
