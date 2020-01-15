using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace ZEQP.Print.Entities
{
    public class PrintContextFactory : IDesignTimeDbContextFactory<PrintContext>
    {
        public PrintContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PrintContext>();
            optionsBuilder.UseSqlite("Data Source=E:\\GitHub\\ZEQP.Print\\ZEQPPrint.db");
            return new PrintContext(optionsBuilder.Options);
        }
    }
    public class PrintContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateField> TemplateFields { get; set; }
        public DbSet<PrintTask> PrintTasks { get; set; }

        public PrintContext(DbContextOptions<PrintContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
