using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace EF_Lessons
{
    class Program
    {
        static void Main(string[] args)
        {
            Create_Empty_DB();
            Adding_Course();
            Reading_Course();
        }
        public static void Create_Empty_DB()
        {
            using var dbContext = new ApplicationDbContext();

            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();

        }

        public static void Adding_Course()
        {
            using var dbContext = new ApplicationDbContext();

            var charpCourses = new Course { Name = "C# Advanced", LessonQuantity = 7 };
            var efCoreCourse = new Course { Name = "EF Core Basic", LessonQuantity = 10 };

            dbContext.Add(charpCourses);
            dbContext.Add(efCoreCourse);

            dbContext.SaveChanges();
        }

        public static void Reading_Course()
        {
            using var dbContext = new ApplicationDbContext();

            var courses = dbContext.Courses.ToList();

            foreach(var course in courses)
            {
                Console.WriteLine(
                    $"Name of course: {course.Name}. The lessons quantity: {course.LessonQuantity}.");
            }
            
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=EfCoreBasicDb;Trusted_Connection=True;");
        }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LessonQuantity { get; set; }
    }

}
