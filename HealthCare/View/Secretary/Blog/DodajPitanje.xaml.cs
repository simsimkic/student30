using HealthCare;
using Model.Communication;
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
using System.Windows.Shapes;

namespace Sekretar.Blog
{
    /// <summary>
    /// Interaction logic for DodajPitanje.xaml
    /// </summary>
    
    public partial class DodajPitanje : Window
    {
        private DodajPitanje()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static DodajPitanje instance = null;
        public static DodajPitanje Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DodajPitanje();
                }
                return instance;
            }
        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void ButtonPotvrdi(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            Question question = new Question() { Doctor = new Doctor(), Patient = new Patient(),  Title = NaslovPitanja.Text, Questions = NaslovPitanja.Text, Answer = TextPitanje.Text, IsFAQ = true
            , IsAnswered = true};
            app.questionController.Create(question);
            CestaPitanja.Instance.ResetComponents();
            this.Visibility = Visibility.Hidden;
        }
    }
}
