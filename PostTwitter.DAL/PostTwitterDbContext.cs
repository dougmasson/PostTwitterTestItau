using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PostTwitter.Model;
using System.IO;

namespace PostTwitter.DataAcess
{
    /// <summary>
    /// Contexto do Entity Framework
    /// </summary>
    public class PostTwitterDbContext : DbContext
    {
        public DbSet<Status> Status { get; set; }
        public DbSet<Execucao> Execucao { get; set; }
        public DbSet<Twitters> Twitters { get; set; }
        public DbSet<HashTag> HashTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
