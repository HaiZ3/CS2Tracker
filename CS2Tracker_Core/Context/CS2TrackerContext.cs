using Microsoft.EntityFrameworkCore;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Classes;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Context
{
    public class CS2TrackerContext : DbContext
    {
        public CS2TrackerContext(DbContextOptions<CS2TrackerContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<SkinInfo> SkinInfos { get; set; }
        public DbSet<DailyStat> DailyStats { get; set; }
        public DbSet<OverallStat> OverallStats { get; set; }
        public DbSet<CurrencyInfo> CurrencyInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
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
                .HasForeignKey<SkinInfo>(s => s.EntryId)
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