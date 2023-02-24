using Controller.CommunicationControllers;
using HealthCare;
using HealthCare.View.Patient;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for Blog.xaml
    /// </summary>

    public partial class Blog : Page
    {
        public ObservableCollection<Article> Articles { get; set; }

        private IArticleController articleController;

        public Blog()
        {
            var app = Application.Current as App;
            articleController = app.articleController;
            Articles = new ObservableCollection<Article>(articleController.GetAll());
            InitializeComponent();
            this.DataContext = this;
        }

        private void OpenBlogPost_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Article dataContext = button.DataContext as Article;
            this.NavigationService.Navigate(new BlogPost(dataContext));
        }
    }
}
