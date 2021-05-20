using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTransactions.Models
{
    public class SBContext : DbContext
    {
        public SBContext(DbContextOptions<SBContext> options) : base(options)
        { }
        public DbSet<SBTransact> Sbtransaction { get; set; }
    }
}
