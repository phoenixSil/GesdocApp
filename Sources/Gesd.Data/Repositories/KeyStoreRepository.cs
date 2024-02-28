using Gesd.Data.Context;
using Gesd.Entite;
using Gesd.Features.Contrats.Repositories;

namespace Gesd.Data.Repositories
{
    public class KeyStoreRepository : GenericRepository<KeyStore>, IKeyStoreRepository
    {
        public KeyStoreRepository(GesdContext context) : base(context)
        { }
    }
}
