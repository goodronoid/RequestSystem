using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RequestSystem.Models
{
    public class RequestSystemContext : DbContext
    {
        public RequestSystemContext (DbContextOptions<RequestSystemContext> options)
            : base(options)
        {
        }

        public DbSet<RequestSystem.Models.Note> Note { get; set; }
    }
}
