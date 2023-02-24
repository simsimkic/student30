using HCIProjekat.View.BlogView.DataView;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.View.Director.BlogView.Converter
{
    class BlogItemConverter : AbstractConverter
    {
        public static BlogItem ConvertArticleToArticleView(Article article)
           => new BlogItem
           {
               Id = article.Id,
               Title = article.Title,
               Text = article.Text,
               Picture = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, article.Image),
               Date = article.Date,
               Author = article.CreatedBy.Name + " " + article.CreatedBy.Surname
           };

        public static IList<BlogItem> ConvertArticleListToArticleViewList(IList<Article> articles)
            => ConvertEntityListToViewList(articles, ConvertArticleToArticleView);
    }
}
