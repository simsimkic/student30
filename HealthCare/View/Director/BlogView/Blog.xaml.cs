using Controller.CommunicationControllers;
using Controller.UserControllers;
using HCIProjekat.View.BlogView.DataView;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using HealthCare.View.Director.BlogView.Converter;
using Model.Communication;
using Model.Users;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.BlogView
{
    /// <summary>
    /// Interaction logic for Blog.xaml
    /// </summary>
    public partial class Blog : Page
    {
        public ObservableCollection<UserControl> Blogs { get; set; }
        private IArticleController articleController;
        private readonly IUserController userController;

        public Blog()
        {
            var app = Application.Current as App;
            articleController = app.articleController;
            userController = app.userController;

            List<Article> articles = articleController.GetAll().ToList();
            foreach (Article a in articles)
                a.CreatedBy = (Staff)userController.GetFromID(a.CreatedBy.Id);

            Blogs = new ObservableCollection<UserControl>(BlogItemConverter.ConvertArticleListToArticleViewList(articles));

            InitializeComponent();

            DataContext = this;
        }

        private void addPost_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new BlogAdd(), false);
        }

        private void listaClanaka_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                MainWindow.AppWindow.navigateWithTitleChange(new BlogDetails((BlogItem)listaClanaka.SelectedItem), false);
            }
        }

        private void details_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new BlogDetails((BlogItem)listaClanaka.SelectedItem), false);
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawAddingArticleEvent));
        }
        private void withdrawAddingArticleEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

    }
}
