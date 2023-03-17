using Infra.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace desafioBack.Infra
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<User> User { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<EventHistory> EventHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasOne(x => x.Subscription)
                        .WithOne(x => x.User)
                        .HasForeignKey<Subscription>(p => p.UserId);

            modelBuilder.Entity<Status>()
                        .HasOne(x => x.Subscription)
                        .WithOne(x => x.Status)
                        .HasForeignKey<Subscription>(p => p.StatusId);

            modelBuilder.Entity<Subscription>()
                        .HasOne(x => x.EventHistory)
                        .WithOne(x => x.Subscription)
                        .HasForeignKey<EventHistory>(p => p.SubscriptionId);

            modelBuilder.GenerateSeed();
        }
    }
}