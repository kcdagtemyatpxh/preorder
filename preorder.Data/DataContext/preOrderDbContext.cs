using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using preorder.Data.Configurations;
using preorder.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TripleSix.Core.DataContext;

namespace preorder.Data.EF
{
    internal class preOrderDbContext : BaseDbContext
    {
        private readonly IConfiguration _configuration;

        public preOrderDbContext(IConfiguration configuration)
            : base(typeof(IpreOrderDbContext).Assembly, Assembly.GetExecutingAssembly())
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            var serverVersion = new MySqlServerVersion(new Version(8, 0));
            builder.UseMySql(_configuration.GetConnectionString("Default"), serverVersion);

            //builder.UseSqlServer(_configuration.GetConnectionString("Default"));

#if DEBUG
            builder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new sr_headerConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
