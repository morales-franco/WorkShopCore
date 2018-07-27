using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkShopCore.Entities;

namespace WorkShopCore.Business
{
    public interface IArticlesService
    {
        Article GetOneArticle(int id);

        List<Article> GetAllArticles();

        Article AddArticle(Article article);

        Article EditArticle(Article article);

        void DeleteArticle(int id);
    }
}
