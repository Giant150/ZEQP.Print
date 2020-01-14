using Microsoft.EntityFrameworkCore;
using System;

namespace ZEQP.Print.Entities
{
    public class PrintContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }
        public DbSet<History> History { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=E:\\GitHub\\ZEQP.Print\\ZEQPPrint.db");
        }
    }
}
