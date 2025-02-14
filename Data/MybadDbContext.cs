using MvcMyBad.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMyBad.Data
{
    public class MybadDbContext : DbContext
    {
        public MybadDbContext(DbContextOptions<MybadDbContext> options)
            : base(options) { }

        public DbSet<Models.UserModel> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>()
                .HasKey(u => u.Id);
        }
    }
}
