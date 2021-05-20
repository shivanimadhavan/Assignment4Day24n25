using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBAssignment.Models
{
    public class SBAccountContext: DbContext
    {
        public SBAccountContext(DbContextOptions<SBAccountContext> options) : base(options)
        { }
        public DbSet<SBAccount> sbaccount { get; set; }
    }
}
