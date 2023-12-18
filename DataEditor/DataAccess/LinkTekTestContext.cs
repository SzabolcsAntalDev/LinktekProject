using DataEditor.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DataEditor.DataAccess
{
    public partial class LinkTekTestContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["LinkTekTest"].ConnectionString);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
