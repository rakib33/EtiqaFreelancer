using EtiqaFreelancerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EtiqaFreelancerApi.DataContext
{
    public class FreelancerContext : DbContext
    {
        public FreelancerContext(DbContextOptions<FreelancerContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=eu-az-sql-serv1.database.windows.net;Initial Catalog=dcy8k5s7tqgue9h;User ID=uyf63hof7fhkbqj;Password=uwtbMW@0wlnYUklMB40aoOVc!;Integrated Security=False");
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-OC677T4;Initial Catalog=EtiqaFreelancerDB;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<User> Users { get; set; }
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
