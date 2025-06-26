using Microsoft.EntityFrameworkCore;
using RHView_TCC.Models;

namespace RHView_TCC.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProcessLog> ProcessLogs { get; set; }
        public DbSet<InactivityLog> InactivityLogs { get; set; }
        public DbSet<DailyWorkLog> DailyWorkLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserModel>()
                        .Property(u => u.Role)
                        .HasConversion<string>();

            modelBuilder.Entity<UserModel>()
                        .Property(u => u.WorkHours)
                        .HasConversion<string>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProcessLog>()
                .HasOne(p => p.User)
                .WithMany(u => u.ProcessLogs)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InactivityLog>()
                .HasOne(i => i.User)
                .WithMany(u => u.InactivityLogs)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DailyWorkLog>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
