using Gesd.Features.Compute.Communs;
using Gesd.Features.Dtos.Fichiers;

namespace Gesd.Features.Compute.Commands.Fichiers
{
    public class AddFileCmd : BaseComputeCmd
    {
        public FileToAddDto File { get; set; }
    }
}
