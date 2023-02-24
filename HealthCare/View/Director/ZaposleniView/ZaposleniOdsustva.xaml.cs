using Controller.UserControllers;
using Director.ZaposleniView.Converter;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.ZaposleniView.DataView;
using HealthCare;
using Model.Users;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniOdsustva.xaml
    /// </summary>
    public partial class ZaposleniOdsustva : Page
    {
        public ObservableCollection<UserControl> Absences { get; set; }
        private readonly IAbsenceController _absenceController;
        private readonly IUserController _userController;

        public ZaposleniOdsustva()
        {
            var app = Application.Current as App;
            _absenceController = app.absenceController;
            _userController = app.userController;
            List<Absence> absences = _absenceController.GetNonApprovedAbsence().ToList();
            foreach (Absence a in absences)
                a.staff = (Staff)_userController.GetFromID(a.staff.Id);

            Absences = new ObservableCollection<UserControl>(EmployeAbsenceRequestItemConverter.ConvertAbsenceListToAbsenceViewList(absences));

            InitializeComponent();

            DataContext = this;
        }

        private void listaOdsustava_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaOdsustava.SelectedItem != null)
            {
                dugmici.Visibility = Visibility.Visible;
            }
            else
            {
                dugmici.Visibility = Visibility.Collapsed;
            }
        }

        private void kalendar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniIstorijaOdsustava((EmployeeAbsenceRequestItem)listaOdsustava.SelectedItem), false);
        }

        private void confirmAbsence_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;

            dialog.Children.Add(new ConfirmDialog("Odobravanje odsustva", "Da li ste sigurni da zelite da odobrite odsustvo?",
                                                                            new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmAbsenceEvent)));
            dialog.Visibility = Visibility.Visible;
        }

        private void confirmAbsenceEvent(object sender, RoutedEventArgs e)
        {
            _absenceController.ApproveAbsence(_absenceController.GetFromID(((EmployeeAbsenceRequestItem)listaOdsustava.SelectedItem).IdAbsence));
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniOdsustva(), false);
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void rejectAbsence_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new OdbijanjeZahteva((EmployeeAbsenceRequestItem)listaOdsustava.SelectedItem), false);
        }

    }
}
