using Microsoft.EntityFrameworkCore;
using PostTwitter.Model;

namespace PostTwitter.DataAcess
{
    public class PostTwitterDbContext : DbContext
    {
        private const string connectionString = @"Server=.\SQLEXPRESS;Database=BaseTestItau;Trusted_Connection=True;";

        public DbSet<Status> Status { get; set; }
        public DbSet<Execucao> Execucao { get; set; }
        public DbSet<Twitters> Twitters { get; set; }
        public DbSet<HashTag> HashTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
