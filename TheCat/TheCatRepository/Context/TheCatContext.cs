using Microsoft.EntityFrameworkCore;
using TheCatDomain.Entities;

namespace TheCatRepository.Context
{
    public class TheCatContext : DbContext
    {
        public TheCatContext(DbContextOptions<TheCatContext> options) : base(options)
        {
        }

        public virtual DbSet<Breeds> Breeds { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<ImageUrl> ImageUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureBreeds(modelBuilder);
            ConfigureCategory(modelBuilder);
            ConfigureImage(modelBuilder);
        }

        void ConfigureBreeds(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Breeds>()
                .HasKey(x => x.BreedsId);
            modelBuilder.Entity<Breeds>()
                .Property(x => x.BreedsId)
                .HasMaxLength(80)
                .IsRequired();
            modelBuilder.Entity<Breeds>()
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
            modelBuilder.Entity<Breeds>()
                .Property(x => x.Origin)
                .HasMaxLength(255);
            modelBuilder.Entity<Breeds>()
                .Property(x => x.Temperament)
                .HasMaxLength(255);
            modelBuilder.Entity<Breeds>()
                .Property(x => x.Description)
                .HasMaxLength(1024);
        }

        void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(x => x.CategoryId);
            modelBuilder.Entity<Category>()
                .Property(x => x.CategoryId)
                .IsRequired();
            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
        }

        void ConfigureImage(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageUrl>()
                .HasKey(x => x.ImageUrlId);
            modelBuilder.Entity<ImageUrl>()
                .Property(x => x.ImageUrlId)
                .HasMaxLength(80)
                .IsRequired();
            modelBuilder.Entity<ImageUrl>()
                .Property(x => x.Url)
                .HasMaxLength(512)
                .IsRequired();

            modelBuilder.Entity<ImageUrl>()
                .HasOne(x => x.Breeds)
                .WithMany(x => x.Images);

            modelBuilder.Entity<ImageUrl>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Images);
        }
    }
}
