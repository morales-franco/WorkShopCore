using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WorkShopCore.Business;
using WorkShopCore.Entities;

namespace WorkShopCore.API
{
    [Route("/api/articles")]
    public class ArticlesApiController : Controller
    {
        private readonly IArticlesService articlesService;

        public ArticlesApiController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var article = articlesService.GetOneArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        [HttpGet]
        public IActionResult Get() => Ok(articlesService.GetAllArticles());

        [HttpPost]
        public IActionResult Create([FromBody]Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            articlesService.AddArticle(article);

            return CreatedAtAction(nameof(Get), new { id = article.Title }, article);
        }

        [HttpPut("{id}")]
        public IActionResult Edit([FromBody]Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentArticle = articlesService.EditArticle(article);

            if (currentArticle == null)
            {
                return NotFound();
            }


            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var article = Article.DataSource.Where(m => m.Id == id).FirstOrDefault();

            if (article == null)
            {
                return NotFound();
            }

            articlesService.DeleteArticle(id);

            return NoContent();
        }
    }
}
