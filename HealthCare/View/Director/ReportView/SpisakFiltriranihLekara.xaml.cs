using Controller.UserControllers;
using Director.ReportView.Converter;
using HCIProjekat.View.ReportView.DataView;
using HealthCare;
using Model.DataReport;
using Model.Users;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.View.ReportView
{
    /// <summary>
    /// Interaction logic for SpisakFiltriranihLekara.xaml
    /// </summary>
    public partial class SpisakFiltriranihLekara : Page
    {
        private readonly IUserController userController;
        private DoctorOccupationReport _report;
        public ObservableCollection<UserControl> Employees { get; set; }
        public SpisakFiltriranihLekara(DoctorOccupationReport report)
        {
            _report = report;
            var app = Application.Current as App;
            userController = app.userController;
            foreach (var v in _report.doctorOccupation)
                v.doctor = (Doctor)userController.GetFromID(v.doctor.Id);

            Employees = new ObservableCollection<UserControl>(DoctorItemConverter.ConvertDoctorListToDoctorViewList(_report.doctorOccupation));

            InitializeComponent();

            DataContext = this;
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                MainWindow.AppWindow.navigateWithTitleChange(new DetaljiFiltriranihLekara((FilteredDoctorItem)listaLekara.SelectedItem), false);
            }
        }

        private void details_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new DetaljiFiltriranihLekara((FilteredDoctorItem)listaLekara.SelectedItem), false);
        }
    }
}
