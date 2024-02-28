using Gesd.Entite.Responses;
using Gesd.Features.Compute.Tools;

using MediatR;

namespace Gesd.Features.Compute.Communs
{
    public abstract class BaseComputeCmd : IRequest<RequestResponse>
    {
        public TypeDeRequette Operation { get; set; }
    }
}
