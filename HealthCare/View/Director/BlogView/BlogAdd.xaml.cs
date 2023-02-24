using Controller.CommunicationControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace HCIProjekat.BlogView
{
    /// <summary>
    /// Interaction logic for BlogAdd.xaml
    /// </summary>
    public partial class BlogAdd : Page
    {
        private const string MANDATORY_TEXT_FIELD = "Tekst clanka je obavezno polje!";
        private const string MANDATORY_TITLE_FIELD = "NASLOV clanka je obavezno polje!";

        private string _text;
        private string _title;
        private string _picture = "";
        private IArticleController articleController;
        public BlogAdd()
        {
            var app = Application.Current as App;
            articleController = app.articleController;

            InitializeComponent();

            DataContext = this;
        }
        public string TitleBlog
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void imageSearch_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                _picture = dlg.FileName;
                BitmapImage picture = new BitmapImage();
                picture.BeginInit();
                picture.UriSource = new Uri(_picture);
                picture.EndInit();
                slika.Source = picture;
            }
        }

        private void addArticleEvent(object sender, RoutedEventArgs e)
        {
            articleController.Create(createArticle());
            MainWindow.AppWindow.navigateToRootPage(new Blog(), false);
        }

        private Article createArticle()
        {
            var app = Application.Current as App;
            return new Article() { CreatedBy =  new Model.Users.Staff() { Id = app._currentUser.Id }, Date = DateTime.Now, Image = _picture, Text = _text, Title = _title };
        }

        private void withdrawAddingArticleEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawAddingArticleEvent));
        }
        private void addArticle_Click(object sender, RoutedEventArgs e)
        {
            if (!isTitleFulfilled())
            {
                ShowError(MANDATORY_TITLE_FIELD);
            }
            else if (!isTextFulfilled())
            {
                ShowError(MANDATORY_TEXT_FIELD);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Dodavanje novog clanka", "Da li ste sigurni da zelite da dodate novi clanak?",
                                                        new RoutedEventHandler(withdrawAddingArticleEvent), new RoutedEventHandler(addArticleEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private bool isTextFulfilled()
        {
            return _text != "" && _text != null;
        }

        private bool isTitleFulfilled()
        {
            return _title != "" && _title != null;
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od objavljivanja clanka", "Da li ste sigurni da zelite da odustanete od objavljivanja clanka?",
                                                   new RoutedEventHandler(withdrawAddingArticleEvent), new RoutedEventHandler(withdrawAddingEvent),
                                                   "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void withdrawAddingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new HCIProjekat.BlogView.Blog(), false);
        }
    }
}
