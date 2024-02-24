using Gesd.Features.Compute.Communs;
using Gesd.Features.Dtos.Fichiers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Features.Compute.Commands.Fichiers
{
    public class AddFileCmd: BaseComputeCmd
    {
        public FileToAddDto File {  get; set; }
    }
}
