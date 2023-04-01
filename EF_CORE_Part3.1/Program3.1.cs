using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<Author> Authors { get; set; }

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
    }

    //модель курса
    [Table("MyCourses")]

    public class Course
    {
        [Key] //Первинний ключ сутності
        public int Id { get; set; }

        [Required] //ця колонка обов'язкова і не може бути Null - {NOT NULL}
        [MinLength(10)]
        [MaxLength(500)]
        [Column("MyName")] //ім'я колонки

        public string Name { get; set; }

        //Кількість уроків в курсі
        [Column("MyLessonsQuantity")] //ім'я колонки

        public int LessonsQuantity { get; set; }

        [Column("MyCreatedAt")]

        public DateTimeOffset CreatedAt { get; set; }

        [Column("MyPrice", TypeName = "money")] //ім'я колонки і тип money

        public decimal Price { get; set; }

        public int AuthorId { get; set; }

        // В курса є Автор
        [ForeignKey("AuthorId")] //Вказує на те що властивість Author
        //використовує властивість AuthorId в якості зовнішнього ключа

        public Author Author { get; set; }

    }
        
    //DataAnnotations
    [Table("MyAuthors")]

    public class Author
    {
        [Key] public int Id { get; set; }
        
        [Required]
        [Column("MyFirstName")]
        public string FirstName { get; set; }

        [Required]
        [Column("MyLastName")]

        public string LastName { get; set; }

        [Column("MyAge")]
        
        public int? Age { get; set; }

        //У автора є багато курсів
        public ICollection<Course> Courses { get; set; }

    }
}
