using Ef7FirstLook.Configurations;
using Ef7FirstLook.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ef7FirstLook.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var serverVersion = new MySqlServerVersion(new Version(8, 0));
            optionsBuilder.UseMySql(_configuration.GetConnectionString("Default"), serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new sr_headerConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<sr_header> sr_header { get; set; }
    }
}
