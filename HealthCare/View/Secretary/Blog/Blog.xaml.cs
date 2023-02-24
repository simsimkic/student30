using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Controller.CommunicationControllers;
using Controller.UserControllers;
using HealthCare;
using Model.Communication;

namespace Sekretar.Blog
{
    /// <summary>
    /// Interaction logic for Blog.xaml
    /// </summary>
    public partial class Blog : Window
    {
        public ObservableCollection<Article> clanci
        {
            get;
            set;
        }
        private IArticleController articleController;
        private readonly IUserController userController;

        private readonly INotificationController _notificationController;

        private Blog()
        {
            InitializeComponent();
            var app = Application.Current as App;
            articleController = app.articleController;
            userController = app.userController;

            clanci = new ObservableCollection<Article>();
            List<Article> articles = articleController.GetAll().ToList();
            foreach (Article a in articles)
                clanci.Add(a);

            this.DataContext = this;
 
        }
        private static Blog instance = null;
        public static Blog Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Blog();
                }
                return instance;
            }
        }
        private void ButtonClickProcitaj(object sender, RoutedEventArgs e)
        {
            try
            {
                Article dataRowView = (Article)((Button)e.Source).DataContext;
                String ProductName = dataRowView.Title;
                Clanak clanak = Clanak.Instance;
                clanak.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                clanak.Naslov.Text = ProductName;
                clanak.TekstClanka.Text = dataRowView.Text;
                String Picture = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataRowView.Image);
               
                if (!dataRowView.Image.Equals(""))
                {
                    Uri fileUri = new Uri(Picture);
                    clanak.Slika.Source = new BitmapImage(fileUri);
                }
                 clanak.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void ResetVisibility()
        {
            var app = Application.Current as App;
            articleController = app.articleController;
            clanci.Clear();
            List<Article> articles = articleController.GetAll().ToList();
            foreach (Article a in articles)
                clanci.Add(a);

            this.DataContext = this;
        }
        private void ButtonClickPocetna(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Application.Current.MainWindow.ShowDialog();

        }
        private void dodajClanak(object sender, RoutedEventArgs e)
        {
            DodajClanak dodajClanak = DodajClanak.Instance;
            dodajClanak.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dodajClanak.ShowDialog();
        }
        private void CestoPitanje(object sender, RoutedEventArgs e)
        {
            CestaPitanja cestaPitanja = CestaPitanja.Instance;
            cestaPitanja.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cestaPitanja.ResetComponents();
            cestaPitanja.ShowDialog();
        }
        private void odgovoriLekara(object sender, RoutedEventArgs e)
        {
            OdgovoriLekara odgovoriLekara = OdgovoriLekara.Instance;
            odgovoriLekara.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            odgovoriLekara.ResetComponents();
            odgovoriLekara.ShowDialog();
        }
    }
}
