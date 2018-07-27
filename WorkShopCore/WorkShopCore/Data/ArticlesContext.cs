using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkShopCore.Entities;

namespace WorkShopCore.Data
{
    public class ArticlesContext : DbContext
    {
        public ArticlesContext(DbContextOptions options) :
           base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./articles.db");
        }

        /*
         * Generate DB --> desde el cmd
         * Run migrations with: WorkShopCore>dotnet ef migrations add InitialCreate
         * Apply the changes: WorkShopCore>dotnet ef database update
         * 
         * Some ef commands:
         * add – Add a new migration
         * apply – Apply migrations to the database
         * list – List the migrations
         * script – Generate a SQL script from migrations
         * remove – Remove the last migration
         */
    }
}
