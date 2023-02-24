using Model.Users;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HealthCareClinic.View.Doctor.Model
{
    /// <summary>
    /// Interaction logic for BlogView.xaml
    /// </summary>
    public partial class ArticleView : UserControl
    {
        private int _id;
        private string _title;
        private string _text;
        private DateTime _date;
        private string _image;
        private Staff _createdBy;

        public Staff CreatedBy
        {
            get { return _createdBy; }
            set
            {
                _createdBy = value;
                OnPropertyChanged();
            }
        }

        public ArticleView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnArticleDetails_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var articleView = btn.DataContext as ArticleView;

            ArticleDetails window = new ArticleDetails(articleView);
            window.ShowDialog();
        }
    }
}
