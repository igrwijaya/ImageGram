using System;
using Microsoft.EntityFrameworkCore;

namespace ImageGram.App.DbMigrations
{
    public class ApplyMigration
    {
        public static void Run()
        {
            Console.WriteLine("Start Migrating...");
            var dbContextFactory = new SqlDesignTimeDbContextFactory();
            var dbContext = dbContextFactory.CreateDbContext();

            var migrationData = dbContext.Database.GetPendingMigrations();

            foreach (var migration in migrationData)
            {
                Console.WriteLine(migration);
            }

            dbContext.Database.Migrate();
            
            Console.WriteLine("Success to Migrate...");
        }
    }
}