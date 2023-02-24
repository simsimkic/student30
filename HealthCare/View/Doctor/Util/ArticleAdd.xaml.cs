using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for BlogAdd.xaml
    /// </summary>
    public partial class ArticleAdd : Window
    {
        private Article _newArticle;
        private String _image="";
        private string ArticleTextLenghtErrorMessage = "Tekst clanka mora imati bar 50 karaktera";
        private string ArticleTitleLenghtErrorMessage = "Naslov clanka mora imati minimalno 10 karaktera";
        private string ImageErrorMessage = "Clanak mora imati sliku";
        public ArticleAdd()
        {
            InitializeComponent();
            _image = "/View/Doctor/ViewResources/Images/chooseImage.png";
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        public String Image
        {
            get { return _image; }
            set { _image = value; 
                
            }
        }

        private void btnClickChooseImageArticle_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                _image = filename;
                imageBrush.ImageSource = new BitmapImage(new Uri(filename, UriKind.Relative));


            }
        }

        private void btnAddNewArticle_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                String title = tbArticleTitle.Text;
                String text = tbArticleText.Text;
                DateTime date = DateTime.Now;
                String image = _image;

                _newArticle = new Article { Date=date, Image=image, Text=text, Title= title, CreatedBy= (Staff)(Application.Current as App)._currentUser };
                this.DialogResult = true;
                this.Close();
            }

        }



        private bool isValidInputData()
        {
            if (tbArticleTitle.Text.Length < 10)
            {
                ShowError(ArticleTitleLenghtErrorMessage);
                return false;
            }else if (tbArticleText.Text.Length < 50)
            {
                ShowError(ArticleTextLenghtErrorMessage);
                return false;
            }else if (_image == "/View/Doctor/ViewResources/Images/chooseImage.png")
            {
                ShowError(ImageErrorMessage);
                return false;
            }

            return true;
        }

        public Article NewArticle
        {
            get { return _newArticle; }
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

    }
}
