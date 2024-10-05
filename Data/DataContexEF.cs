
using Microsoft.EntityFrameworkCore;
using Helloworld.Models;
using Microsoft.Extensions.Configuration;

namespace Helloworld.Data
{
    public class DataContextEF : DbContext

    {
        
        private IConfiguration _config;

        

        public DataContextEF(IConfiguration config)
        {
            _config = config;
            // _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public DbSet<Computer>? Computer { get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    options => options.EnableRetryOnFailure());
                
            }
        
        }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
            // .HasNoKey();
            .HasKey(c => c.ComputerId);
            // .ToTable("Computer", "TutorialAppSchema");
        }
    }
}