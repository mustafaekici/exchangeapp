using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDataAccess.Models
{
    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext(String sqlConnectionName) : base(sqlConnectionName)
        {
        }

        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                        .ToTable("Currencies", "dbo")
                        .HasKey(t => t.CurrencyId);
        }
    }

    public partial class AlterDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER DATABASE CurrencyDb SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE", true);
        }

        public override void Down()
        {

        }
    }
}
