using System;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EF_Core2
{
    class Program2
    {
        static void Main(string[] args)
        {
            using var dbContext = new ApplicationDbContext();

            dbContext.Database.EnsureCreated();
            dbContext.Database.ExecuteSqlRaw("SELECT 1");

            Console.WriteLine();
            Console.WriteLine($"Name provider DB: {dbContext.Database.ProviderName}.");
            Console.WriteLine();
        }

        public class ApplicationDbContext : DbContext
        {
            //public DbSet<Course> Courses { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                /*var connectionStringBuilder = new SqlConnectionStringBuilder
                {
                    ["Server"] = @"(localdb)\mssqllocaldb",
                    ["Database"] = "EfCoreBasicDb",
                    ["Trusted_Connection"] = true
                };*/

                //Console.WriteLine(connectionStringBuilder.ConnectionString);

                var configuration = new ConfigurationBuilder()
                    .AddUserSecrets<
                        ApplicationDbContext>()
                        .Build();   //Шифрування

                Console.WriteLine(configuration.GetDebugView());

                var connectionString = configuration.GetConnectionString("EfCoreBasicDatabase");

                optionsBuilder
                    //.UseSqlServer(connectionStringBuilder.ConnectionString)
                    .UseSqlServer(connectionString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .LogTo(
                        Console.WriteLine,
                        new[] { DbLoggerCategory.Database.Command.Name },
                        LogLevel.Information);
            }
        }
    }
}
