using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Modules.Product.Infrastructures;
using Modules.Product.Models;

namespace Modules.Product.Controllers
{
    public class ProductController : Controller
    {
        DemoDbContext _dbContext;
        public ProductController(DemoDbContext demoDbContext)
        {
            _dbContext = demoDbContext;
            SampleData();
        }

        [Route("products.html")]
        public IActionResult Index()
        {
            var data = _dbContext.Products.ToList();
            return View(data.MapToModels());
        }

        private void SampleData()
        {
            var lastItem = _dbContext.Products.LastOrDefault();
            int count = 1;
            if (lastItem != null)
                count = lastItem.Id + 1;

            for (int i = count; i < (count +10); i++)
            {
                var p = new DataAccess.Entities.Product
                {
                    CreatedDate = DateTime.Now,
                    Name = $"Product {i}",
                    Price = new Random().Next(20000, 100000),
                    Sku = $"Sku {i}"
                };
                _dbContext.Products.Add(p);
            }

            _dbContext.SaveChanges();
        }
    }
}
