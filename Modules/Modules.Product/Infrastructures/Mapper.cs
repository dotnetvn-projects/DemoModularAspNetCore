using Modules.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity = DataAccess.Entities;

namespace Modules.Product.Infrastructures
{
    public static class Mapper
    {
        public static ProductModel MapToModel(this Entity.Product entity)
        {
            return new ProductModel
            {
                CreatedDate = entity.CreatedDate,
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Sku = entity.Sku              
            };
        }

        public static IEnumerable<ProductModel> MapToModels(this IEnumerable<Entity.Product> entity)
        {
            List<ProductModel> data = new List<ProductModel>();
            foreach (var item in entity)
            {
                data.Add(item.MapToModel());
            }
            return data;
        }
    }
}
