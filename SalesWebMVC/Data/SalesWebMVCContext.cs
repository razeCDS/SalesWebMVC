using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;

namespace SalesWebMVC.Data
{
    public class SalesWebMVCContext : DbContext
    {
        public SalesWebMVCContext(DbContextOptions<SalesWebMVCContext> options)
            : base(options)
        {
        }

        public DbSet<SalesWebMVC.Models.Department> Department { get; set; } // tabelas a serem adicionadas no banco de dados
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }

        internal object FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
