using EtiqaFreelancerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EtiqaFreelancerApi.DataContext
{
    public class FreelancerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-OC677T4;Initial Catalog=EtiqaFreelancerDB;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<User>? Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);      
            modelBuilder.Entity<User>(b => {
                b.HasKey(l => l.Id);
                b.ToTable("User");
            });
        }
    }
}
