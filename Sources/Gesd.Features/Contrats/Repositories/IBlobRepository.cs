using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Features.Contrats.Repositories
{
    public interface IBlobRepository
    {
        Task<(string fileName, string filePath)> Add(IFormFile file);
        bool Delete(string url);
    }
}
