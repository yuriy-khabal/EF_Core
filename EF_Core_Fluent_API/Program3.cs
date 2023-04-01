using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace EF_Core_Part3
{
    public class Program3
    {
       public static void Main(string[] args)
        {
            Creating_Empty_DataBase();
        }

        private static void Creating_Empty_DataBase()
        {
            using var dbContext = new ApplicationDbContext();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<Author> Autors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=EfCoreBasicDb;Trusted_Connection=True;")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .LogTo(
                Console.WriteLine, LogLevel.Information);
        }

        //Тут ми налаштовуємо моделювання сутностей в EF Core
        //через FluentApi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Course>()
                .ToTable("MyCourses");

            modelBuilder
                .Entity<Course>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Course>()
                .Property(x => x.Name)
                .HasColumnName("MyName")
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder
                .Entity<Course>()
                .Property(x => x.LessonsQuantity)
                .HasColumnName("MyLessonsQuantity");

            modelBuilder
                .Entity<Course>()
                .Property(x => x.CreatedAt)
                .HasColumnName("MyCreatedAt");

            modelBuilder
                .Entity<Course>()
                .Property(x => x.Price)
                .HasColumnName("MyPrice")
                .HasColumnType("money");

            modelBuilder
                .Entity<Course>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Courses)
                .IsRequired();

            modelBuilder
                .Entity<Author>()
                .ToTable("MyAuthor");

            modelBuilder
                .Entity<Author>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Author>()
                .Property(x => x.FName)
                .HasColumnName("MyFirstName")
                .IsRequired();

            modelBuilder
                .Entity<Author>()
                .Property(x => x.LName)
                .HasColumnName("MyLastName")
                .IsRequired();

            modelBuilder
                .Entity<Author>()
                .Property(x => x.Age)
                .HasColumnName("Age");
        }

        //Fluent_API_IEntity_Type_Configuration
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }*/
    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int LessonsQuantity { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public decimal Price { get; set; }

        public Author Author { get; set; }
    }

    //Fluent_API_IEntity_Type_Configuration

    /*public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
    {   
        //метод Configure аналогічний виклику model.Builder.Entity<Course> з OnModelCreating
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .ToTable("MyCourses");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasColumnType("MyName")
                .HasMaxLength(500)
                .IsRequired();

            builder
               .Property(x => x.LessonsQuantity)
               .HasColumnName("MyLessonsQuantity");

            builder
                .Property(x => x.CreatedAt)
                .HasColumnName("MyCreatedAt");

            builder
                .Property(x => x.Price)
                .HasColumnName("MyPrice")
                .HasColumnType("money");

            builder              
                .HasOne(x => x.Author)
                .WithMany(x => x.Courses)
                .IsRequired();
            //Аналогічно для моделі Author
        }
    }*/

    public class Author
    {
        public int Id { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }
        public int? Age { get; set; }

        public ICollection<Course> Courses { get; set; }
    }

    //Fluent_API_IEntity_Type_Configuration

    /*public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        //метод Configure аналогічний виклику model.Builder.Entity<Course> з OnModelCreating
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .ToTable("MyAuthors");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.FName)
                .HasColumnName("MyFirstName")
                .IsRequired();

            builder
                .Property(x => x.LName)
                .HasColumnName("MyLastName")
                .IsRequired();

            builder
                .Property(x => x.Age)
                .HasColumnName("Age");
        }
    }*/


}
