using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkShopCore.Entities
{
    public class Article : IEntityBase
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public static List<Article> DataSource = new List<Article>(new[] {
          new Article() { Id = 1, Title = "Intro to ASP.NET Core" },
          new Article() { Id = 2, Title = "Docker Fundamentals" },
          new Article() { Id = 3, Title = "Deploying to Azure with Docker" },
      });
    }
}
