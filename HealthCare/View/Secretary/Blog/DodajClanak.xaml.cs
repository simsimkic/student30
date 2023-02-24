using Controller.CommunicationControllers;
using HealthCare;
using Microsoft.Win32;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Sekretar.Blog
{
    /// <summary>
    /// Interaction logic for DodajClanak.xaml
    /// </summary>
    public partial class DodajClanak : Window
    {

        private IArticleController articleController;
        private string slika;
        private DodajClanak()
        {
            InitializeComponent(); 
            var app = Application.Current as App;
            articleController = app.articleController;

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static DodajClanak instance = null;
        public static DodajClanak Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DodajClanak();
                }
                return instance;
            }
        }
        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void ButtonPotvrdi(object sender, RoutedEventArgs e)
        {
            Blog blog = Blog.Instance;
            articleController.Create(createArticle());
            this.Visibility = Visibility.Hidden;
            blog.ResetVisibility();
        }
        private Article createArticle()
        {
            var app = Application.Current as App;
            return new Article() { Date = DateTime.Now, Text = TekstClanka.Text, Title = Naslov.Text, Image = slika, Id = 65465, CreatedBy = (global::Model.Users.Staff)app._currentUser };
        }
        private void ButtonClickDodajSliku(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            slika = openFileDialog.FileName;
            Uri fileUri = new Uri(slika);
            SlikaBlog.Source = new BitmapImage(fileUri);
        }
    }
}
