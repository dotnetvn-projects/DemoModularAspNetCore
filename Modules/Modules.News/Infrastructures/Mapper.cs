using Modules.News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity = DataAccess.Entities;

namespace Modules.News.Infrastructures
{
    public static class Mapper
    {
        public static NewsModel MapToModel(this Entity.News entity)
        {
            return new NewsModel
            {
                Content = entity.Content,
                CreatedDate = entity.CreatedDate,
                Id = entity.Id,
                Title = entity.Title
            };
        }

        public static IEnumerable<NewsModel> MapToModels(this IEnumerable<Entity.News> entity)
        {
            List<NewsModel> data = new List<NewsModel>();
            foreach(var item in entity)
            {
                data.Add(item.MapToModel());
            }
            return data;
        }
    }
}
