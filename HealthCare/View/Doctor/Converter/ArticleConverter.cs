using HealthCareClinic.View.Converter;
using HealthCareClinic.View.Doctor.Model;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareClinic.View.Doctor.Converter
{
    class ArticleConverter : AbstractConverter
    {
        public static ArticleView ConvertArticleToArticleView(Article article)
           => new ArticleView
           {
               Id=article.Id,
               Title= article.Title,
               Text= article.Text,
               Date=article.Date,
               Image= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, article.Image),
               CreatedBy=article.CreatedBy,
           };


        public static IList<ArticleView> ConvertArticleListToTArticleViewList(IList<Article> article)
            => ConvertEntityListToViewList(article, ConvertArticleToArticleView);
    }
}
