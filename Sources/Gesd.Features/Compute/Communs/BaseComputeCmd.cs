using Gesd.Entite.Responses;
using Gesd.Features.Compute.Tools;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Features.Compute.Communs
{
    public abstract class BaseComputeCmd : IRequest<RequestResponse>
    {
        public TypeDeRequette Operation { get; set; }
    }
}
