using System;
using System.Linq;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Modules.News.Infrastructures;

namespace Modules.News.Controllers
{
    public class NewsController : Controller
    {
        DemoDbContext _dbContext;

        public NewsController(DemoDbContext dbContext)
        {
            _dbContext = dbContext;
            SampleData();
        }

        [Route("news.html")]
        public IActionResult Index()
        {
            var data = _dbContext.News.ToList();
            return View(data.MapToModels());
        }

        private void SampleData()
        {
            var lastItem = _dbContext.News.LastOrDefault();
            int count = 1;
            if (lastItem != null)
                count = lastItem.Id + 1;

            for (int i = count; i < (count + 10); i++)
            {
                var n = new DataAccess.Entities.News
                {
                    CreatedDate = DateTime.Now,
                    Content = $"I am the content {i}",
                    Title = "$Title {i}"
                };
                _dbContext.News.Add(n);
            }
            _dbContext.SaveChanges();
        }
    }
}
