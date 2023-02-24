using HealthCareClinic.HCI;
using HealthCareClinic.View.Doctor;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for DefaultPage.xaml
    /// </summary>
    public partial class DefaultPage : Page
    {
        public DefaultPage()
        {
            InitializeComponent();
            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(HandleKeyboard);
            
        }
        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }


        private void btnTreatmentView_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TreatmentViewPage());
        }


        private void btnDrugApproval_Click(object sender, RoutedEventArgs e)
        {
            DrugApproval window = new DrugApproval();
            window.ShowDialog();
        }

        private void btnOpenSearchPatient_Click(object sender, RoutedEventArgs e)
        {
            PatientWindow window = new PatientWindow();
            window.ShowDialog();
        }

        private void btnOpenPatientQuestions_Click(object sender, RoutedEventArgs e)
        {
            PatientQuestions window = new PatientQuestions();
            window.ShowDialog();
        }

        private void btnOpenNotificationList_Click(object sender, RoutedEventArgs e)
        {
            Notifications window = new Notifications();
            window.ShowDialog();
        }

        private void btnOpenBlogPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Blog());
        }

        private void btnOpenCalendaPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CalendarPage());
        }

        private void btnOpenDiagnosisList_Click(object sender, RoutedEventArgs e)
        {
            DiagnosisList window = new DiagnosisList();
            window.ShowDialog();
        }
    }
}
