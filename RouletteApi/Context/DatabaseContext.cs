using Microsoft.EntityFrameworkCore;
using RouletteApi.Models;

namespace RouletteApi.Context
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Roulette> Roulettes { get; set; }
        public DbSet<RecordedReward> Rewards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=123;Database=roulette");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("items");
            modelBuilder.Entity<Item>().HasKey(item => item.Id);
            modelBuilder.Entity<Item>().Property(item => item.Name).IsRequired();
            modelBuilder.Entity<Item>().Property(item => item.Value).IsRequired();

            modelBuilder.Entity<Roulette>().ToTable("roulettes");
            modelBuilder.Entity<Roulette>().HasKey(roulette => roulette.Id);
            modelBuilder.Entity<Roulette>().Property(roulette => roulette.Name).IsRequired();
            modelBuilder.Entity<Roulette>().Property(roulette => roulette.Id).IsRequired();
            modelBuilder.Entity<Roulette>().HasMany(roulette => roulette.Items).WithOne();

            modelBuilder.Entity<RecordedReward>().ToTable("recorded_rewards");
            modelBuilder.Entity<RecordedReward>().HasKey(reward => reward.Id);
            modelBuilder.Entity<RecordedReward>().HasOne(reward => reward.Item).WithMany();
            modelBuilder.Entity<RecordedReward>().Property(reward => reward.Timestamp).IsRequired();
            modelBuilder.Entity<RecordedReward>().Property(reward => reward.Username).IsRequired();
        }
    }
}
