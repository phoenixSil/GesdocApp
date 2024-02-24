using Gesd.Data.Context;
using Gesd.Features.Contrats.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Gesd.Entite.File;

namespace Gesd.Data.Repositories
{
    public class FileRepository : GenericRepository<File>, IFileRepository
    {
        public FileRepository(GesdContext context) : base(context)
        { }
    }
}
