using Gesd.Entite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Gesd.Entite.File;

namespace Gesd.Features.Contrats.Repositories
{
    public interface IEncryptedFileRepository : IGenericRepository<EncryptedUrlFile>
    {
    }
}