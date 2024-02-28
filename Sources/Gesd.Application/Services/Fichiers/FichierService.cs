using AutoMapper;
using Gesd.Entite.Responses;
using Gesd.Features.Compute.Commands.Fichiers;
using Gesd.Features.Contrats.Services.Fichiers;
using Gesd.Features.Dtos.Fichiers;
using File = Gesd.Entite.File;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Gesd.Application.Services.Fichiers
{
    public class FichierService : IFichierService
    {
        private readonly IMediator _sender;
        private readonly IMapper _mapper;

        public FichierService(IMediator sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ApiResponse<FileAddedDto>?> Add(IFormFile file)
        {
            var fileToAdd = new FileToAddDto { File = file };
            var response = await _sender.Send(new AddFileCmd { File = fileToAdd }).ConfigureAwait(false);

            return new ApiResponse<FileAddedDto>
            {
                Data = _mapper.Map<FileAddedDto>(((ApiResponse<File>)response).Data),
                Errors = null,
                Message = response.Message,
                StatusCode = response.StatusCode,
                Success = response.Success,
            };
        }

        
        public async Task<ApiResponse<List<FileDto>>> Get()
        {
            var response = await _sender.Send(new GetFileCmd()).ConfigureAwait(false);
            var data = ((ApiResponse<IEnumerable<File>>)response).Data;
            var dto = _mapper.Map<List<FileDto>>(data);
            return new ApiResponse<List<FileDto>>
            {
                Data = dto,
                Errors = null,
                Message = response.Message,
                StatusCode = response.StatusCode,
                Success = response.Success,
            };
        }

        public Task<ApiResponse<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
