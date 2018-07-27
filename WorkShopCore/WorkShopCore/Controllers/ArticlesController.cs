using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WorkShopCore.Business;
using WorkShopCore.Entities;

namespace netCoreWorkshop.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(articlesService.GetAllArticles());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                articlesService.AddArticle(article);
                return RedirectToAction("Index");
            }

            return View(article);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = articlesService.GetOneArticle(id);

            if (article == null)
                return NotFound();

            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                var updatedArticle = articlesService.EditArticle(article);

                if (updatedArticle == null)
                    return NotFound();

                return RedirectToAction("Index");
            }

            return View(article);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var article = articlesService.GetOneArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var article = articlesService.GetOneArticle(id.Value);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            articlesService.DeleteArticle(id);

            return RedirectToAction("Index");
        }

    }
}