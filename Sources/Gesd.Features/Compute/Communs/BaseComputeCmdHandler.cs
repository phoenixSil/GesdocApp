using AutoMapper;
using Gesd.Entite.Responses;
using Gesd.Features.Contrats.Repositories;
using MediatR;

namespace Gesd.Features.Compute.Communs
{
    public abstract class BaseComputeCmdHandler<T> : IRequestHandler<T, RequestResponse> where T : BaseComputeCmd
    {
        protected readonly IMediator _mediator;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseComputeCmdHandler(IMediator mediator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public abstract Task<RequestResponse> Handle(T request, CancellationToken cancellationToken);
    }
}
