using Gesd.Data.Context;
using Gesd.Entite;
using Gesd.Features.Contrats.Repositories;

namespace Gesd.Data.Repositories
{
    public class EncryptedFileRepository : GenericRepository<EncryptedUrlFile>, IEncryptedFileRepository
    {
        public EncryptedFileRepository(GesdContext context) : base(context)
        { }
    }
}