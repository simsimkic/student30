using Controller.UserControllers;
using HealthCare;
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

namespace Sekretar.MojProfil
{
    /// <summary>
    /// Interaction logic for PosaljiZahtev.xaml
    /// </summary>
    public partial class PosaljiZahtev : Window
    {
        private IAbsenceController absenceController;

        private static PosaljiZahtev instance = null;
        public static PosaljiZahtev Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PosaljiZahtev();
                }
                return instance;
            }
        }
        private PosaljiZahtev()
        {
            InitializeComponent();
            var app = Application.Current as App;
            absenceController = app.absenceController;

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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        private Absence createAbscence()
        {
            var app = Application.Current as App;
            return new Absence() { AbsenceType = (AbsenceType)Vrsta.SelectedItem, StartDate = FROM.SelectedDate.Value.Date, EndDate = TO.SelectedDate.Value.Date ,
             Approved = false, Reason = "", staff = (Staff)app._currentUser};
        }
        private void ButtonClickPotvrdi(object sender, RoutedEventArgs e)
        {
            if (!TO.SelectedDate.HasValue
                || !FROM.SelectedDate.HasValue)
            {
                MessageBox.Show(Secretary.Properties.Langs.Lang.dateError, Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (TO.SelectedDate.Value < FROM.SelectedDate)
                {
                    MessageBox.Show("Niste dobro selektovali datume!", Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                
                absenceController.Create(createAbscence());
                this.Visibility = Visibility.Collapsed;
                ZahteviZaOdsustvo.Instance.ResetVisibility();
            }
        }
       
    }
}
