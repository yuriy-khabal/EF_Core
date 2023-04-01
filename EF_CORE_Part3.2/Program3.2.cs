using System;
using System.Collections.Generic;
using System.Linq;
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
            Adding_Data();
            Reading_Data();
            Change_Data();
            Reading_Data();
        }

        private static void Creating_Empty_DataBase()
        {
            using var dbContext = new ApplicationDbContext();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        private static void Adding_Data()
        {
            using var dbContext = new ApplicationDbContext();

            /*var course = new Course
            {
                Name = "EF Core Basic",
                LessonsQuantity = 10
            };

            dbContext.Add(course);

            dbContext.SaveChanges();*/

            for(int i = 0; i < 10; i++)
            {
                dbContext.Courses.Add(new Course());
            }

            for(int i = 0; i < 5; i++)
            {
                dbContext.Authors.Add(new Author());
            }

            for (int i = 0; i < 10; i++)
            {
                dbContext.Courses.Add(new Course());
            }

            dbContext.SaveChanges();
        }
        private static void Reading_Data()
        {
            using var dbContext = new ApplicationDbContext();

            var courses = dbContext.Courses.ToList(); //Список всіх наших курсів

            Console.WriteLine("Courses:");
            foreach(var course in courses)
            {
                Console.WriteLine($"Identifier: {course.Id}. Number: {course.Number}.");
            }

            Console.WriteLine(
                new string(
                    '-',
                    80));
            var authors = dbContext.Authors.ToList(); //Список всіх наших авторів

            Console.WriteLine("Authors:");
            foreach(var author in authors)
            {
                Console.WriteLine($"Identifier: {author.Id}. Number: {author.Number}.");
            }
        }
        private static void Change_Data()
        {

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

        //Настроюємо моделювання сутностей в EF Core
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder
                .Entity<Course>()
                .Property(t => t.Title)
                    //Передаємо SQL вираз для обчислення значення
                    //Також передаємо флаг, зберігаємо колонку чи ні
                .HasComputedColumnSql(
                    "[Name] + ' contains ' + CAST([LessonsQuantity] as NVARCHAR) + 'lessons.'",
                stored: true);*/
        }
    }

    public class Course
    {
        public int Id { get; set; }

        public int Number { get; set; }

        //public string Name { get; set; }

        //public int LessonsQuantity { get; set; }

       // public string Title { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public int Number { get; set; }
    }

}