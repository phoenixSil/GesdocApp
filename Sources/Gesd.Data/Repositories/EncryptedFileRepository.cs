using Gesd.Data.Context;
using Gesd.Entite;
using Gesd.Features.Contrats.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Data.Repositories
{
    public class EncryptedFileRepository : GenericRepository<EncryptedUrlFile>, IEncryptedFileRepository
    {
        public EncryptedFileRepository(GesdContext context) : base(context)
        { }
    }
}