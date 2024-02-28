using Microsoft.EntityFrameworkCore.Storage;

namespace Gesd.Features.Contrats.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IFileRepository FileRepository { get; }
        IBlobRepository BlobRepository { get; }
        IEncryptedFileRepository EncryptedFileRepository { get; }
        IKeyStoreRepository KeyStoreRepository { get; }

        IDbContextTransaction BeginTransaction();
        Task Enregistrer();
    }
}
