using Gesd.Data.Context;
using Gesd.Features.Contrats.Repositories;

using File = Gesd.Entite.File;

namespace Gesd.Data.Repositories
{
    public class FileRepository : GenericRepository<File>, IFileRepository
    {
        public FileRepository(GesdContext context) : base(context)
        { }
    }
}
