using iCompany.Areas.Design.Models;
using iCompany.Areas.Shared.Models;
using iCompany.Areas.Systems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace iCompany.Models
{
    public class CompanyDbContext : DbContext
    {
        private DbConfigs dbConfig;

        public CompanyDbContext(DbConfigs config)
        {
            dbConfig = config;
        }

        public CompanyDbContext(IOptions<DbConfigs> config)
        {
            dbConfig = config.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(dbConfig.DecryptConnString);
        }

        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Config> Config { get; set; }

        public DbSet<Product> Product { get; set; }
        public DbSet<Components> Components { get; set; }
        public DbSet<ProductComponents> ProductComponents { get; set; }
    }
}
