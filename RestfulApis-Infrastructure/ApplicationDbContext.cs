using Microsoft.EntityFrameworkCore;
using Restfulapis_Domain.Entities;

namespace RestfulApis_Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Email="a@gmail.com",  Username="abed", Password="abed123" },
                new User() { Id = 2, Email="b@gmail.com",  Username="ibrahim", Password="ibrahim123" }
            );
        }
    }
}
