using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Data.Context
{
    public class GesdContext: DbContext
    {
        public GesdContext(DbContextOptions<GesdContext> options): base(options) { }
    }
}
