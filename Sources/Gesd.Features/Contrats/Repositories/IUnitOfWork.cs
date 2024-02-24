using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
