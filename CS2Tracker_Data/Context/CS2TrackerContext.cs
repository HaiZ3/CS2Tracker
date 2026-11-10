using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CS2Tracker_Models;

namespace CS2Tracker_Data.Context
{
    public class CS2TrackerContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public CS2TrackerContext(DbContextOptions<CS2TrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<SkinInfo> SkinInfos { get; set; }
        public DbSet<DailyStat> DailyStats { get; set; }
        public DbSet<OverallStat> OverallStats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<Sheet>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sheets)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Entry>()
                .HasOne(s => s.Sheet)
                .WithMany(e => e.Entries)
                .HasForeignKey(s => s.SheetId);

            modelBuilder.Entity<Entry>()
                .HasOne(e => e.SkinInfo)
                .WithOne(s => s.Entry)
                .HasForeignKey<Entry>(e => e.SkinInfoId)
                .OnDelete(DeleteBehavior.Cascade);

            foreach (var property in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(4);
            }
        }
    }
}
