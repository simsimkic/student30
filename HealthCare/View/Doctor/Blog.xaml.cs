using Controller.CommunicationControllers;
using HealthCare;
using HealthCareClinic.View.Doctor.Converter;
using HealthCareClinic.View.Doctor.Model;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace HealthCareClinic.View.Doctor
{
    /// <summary>
    /// Interaction logic for Blog.xaml
    /// </summary>
    public partial class Blog : Page
    {
        private readonly IArticleController _articleController;
        public Blog()
        {
            InitializeComponent();
            this.DataContext = this;
            this.btnAddNewArticle.Focus();
            var app = Application.Current as App;

            _articleController = app.articleController;
        }

        private void btnAddNewArticle_Click(object sender, RoutedEventArgs e)
        {
            Article _newArticle;
            ArticleAdd inputDialog = new ArticleAdd();
            if (inputDialog.ShowDialog() == true)
            {
                _newArticle = inputDialog.NewArticle;
                _articleController.Create(_newArticle);
                NavigationService.Navigate(new Blog());
            }
        }

    }
}
