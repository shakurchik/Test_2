using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Backpacks> Backpacks { get; set; }
        public DbSet<Characters> Characters { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<CharacterTitle> CharacterTitles { get; set; }

        protected DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Backpacks>()
                .HasKey(b => new { b.CharacterId, b.ItemId });

            modelBuilder.Entity<CharacterTitle>()
                .HasKey(ct => new { ct.CharacterId, ct.TitleId });
            
            modelBuilder.Entity<Item>().HasData(new Item { Id = 1, Name = "Item1", Weight = 10 }, new Item { Id = 2, Name = "Item2", Weight = 20 });
            modelBuilder.Entity<Characters>().HasData(new Characters { Id = 1, FirstName = "John", LastName = "Doe", CurrentWeight = 0, MaxWeight = 100 });
            modelBuilder.Entity<Title>().HasData(new Title { Id = 1, Name = "Title1" });
            modelBuilder.Entity<CharacterTitle>().HasData(new CharacterTitle { CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Now });
            modelBuilder.Entity<Backpacks>().HasData(new Backpacks { CharacterId = 1, ItemId = 1, Amount = 1 });
        }
    }
}