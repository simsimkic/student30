using Model.Appointment;
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
using System.Windows.Shapes;

namespace Sekretar.Izvestaj
{
    /// <summary>
    /// Interaction logic for Izvestaj.xaml
    /// </summary>
    public partial class Izvestaj : Window
    {
        public ObservableCollection<Appointment> preglediDan
        {
            get;
            set;
        }
        public ObservableCollection<Appointment> izvestaj
        {
            get;
            set;
        }
        public Izvestaj()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private static Izvestaj instance = null;
        public static Izvestaj Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Izvestaj();
                }
                return instance;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonClickPocetna(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Application.Current.MainWindow.ShowDialog();

        }
        private void NapraviPDF(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
