using Microsoft.EntityFrameworkCore;
using TheCatDomain.Entities;

namespace TheCatRepository.Context
{
    /// <summary>
    /// Classe que herda do DbContext do Entity Framework
    /// É utilizada nas classes de repositório e responsável em fazer os comandos de banco de dados
    /// </summary>
    public class TheCatContext : DbContext
    {
        /// <summary>
        /// Construtor: Recebe como parâmetro DbContextOption, que pode conter a conexão de qualquer
        /// banco de dados, tornado com isso, esta classe multi-banco de dados
        /// </summary>
        /// <param name="options"></param>
        public TheCatContext(DbContextOptions<TheCatContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet que realiza a interface com a tabela Breeds
        /// A partir desta propriedade poderá ser realizado os comandos de banco de dados
        /// </summary>
        public virtual DbSet<Breeds> Breeds { get; set; }

        /// <summary>
        /// DbSet que realiza a interface com a tabela Category
        /// A partir desta propriedade poderá ser realizado os comandos de banco de dados
        /// </summary>
        public virtual DbSet<Category> Category { get; set; }

        /// <summary>
        /// DbSet que realiza a interface com a tabela ImageUrl
        /// A partir desta propriedade poderá ser realizado os comandos de banco de dados
        /// </summary>
        public virtual DbSet<ImageUrl> ImageUrl { get; set; }

        /// <summary>
        /// Método sobrecarregado da classe DbContext
        /// É responsável em fazer a chamada de mapeamento entre as classes de modelo e as tabelas
        /// do banco de dados. Este método é chamado no momento em que um DbSet é executado
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureBreeds(modelBuilder);
            ConfigureCategory(modelBuilder);
            ConfigureImage(modelBuilder);
        }

        /// <summary>
        /// Mapeia a classe Breeds com a tabela Breeds
        /// </summary>
        /// <param name="modelBuilder"></param>
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

        /// <summary>
        /// Mapeia a classe Category com a tabela Category
        /// </summary>
        /// <param name="modelBuilder"></param>
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

        /// <summary>
        /// Mapeia a classe ImageUrl com a tabela ImageUrl, incluindo seus relacionamentos
        /// com as tabelas Breeds e Category
        /// </summary>
        /// <param name="modelBuilder"></param>
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
