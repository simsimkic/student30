using Controller.CommunicationControllers;
using HealthCare;
using HealthCareClinic.View.Doctor;
using HealthCareClinic.View.Doctor.Model;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for BlogDetails.xaml
    /// </summary>
    public partial class ArticleDetails : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nije moguce obrisati clanak koji je neko drugi postavio";

        private readonly IArticleController _articleController;
        private string _image;
        private ArticleView _article;
        public ArticleDetails(ArticleView article)
        {
            InitializeComponent();

            var app = Application.Current as App;

            _articleController = app.articleController;

            _article = article;
            tbBlogText.Text = _article.Text;
            lblArticleName.Content = _article.Title;
            _image = _article.Image;
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }



        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

    }
}
