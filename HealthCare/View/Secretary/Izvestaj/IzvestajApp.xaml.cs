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

namespace HealthCare.View.Secretary.Izvestaj
{
    /// <summary>
    /// Interaction logic for IzvestajApp.xaml
    /// </summary>
    public partial class IzvestajApp : Window
    {
        public ObservableCollection<Appointment> izvestaj
        {
            get;
            set;
        }
        public IzvestajApp()
        {
            InitializeComponent();
            this.DataContext = this;
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
