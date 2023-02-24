using Controller.UserControllers;
using HealthCare;
using Model.DataReport;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using View.Director.ReportView;

namespace HCIProjekat.View.ReportView
{
    /// <summary>
    /// Interaction logic for ZauzetostLekara.xaml
    /// </summary>
    public partial class ZauzetostLekara : Page
    {
        public class Chart
        {
            public string naslov { get; set; }
            public decimal procenat { get; set; }
        }
        private readonly IUserController userController;

        private DoctorOccupationReport _report;
        public ObservableCollection<Chart> Charts { get; set; }
        public ZauzetostLekara(DoctorOccupationReport report)
        {
            this._report = report;
            var app = Application.Current as App;
            userController = app.userController;
            Charts = new ObservableCollection<Chart>();
            decimal ukupnoRadnoVreme = 0;
            decimal ukupnoZauzeto = 0;
            decimal prosek = 0;
            foreach (var v in report.doctorOccupation)
            {
                ukupnoRadnoVreme += v.TotalWorkTime;
                ukupnoZauzeto += v.OccupationWorkTime;
            }
            if (ukupnoRadnoVreme != 0)
                prosek = ukupnoZauzeto / ukupnoRadnoVreme;

            prosek *= 100;
            Charts.Add(new Chart() { naslov = "", procenat = prosek });
            InitializeComponent();

            DataContext = this;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new FilterZauzetostiLekara(), false);
        }

        private void spisakLekara_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new SpisakFiltriranihLekara(_report), false);
        }

        private void pdfExport_Click(object sender, RoutedEventArgs e)
        {
            int ukupnp = 0;
            int zauzeto = 0;
            foreach (var v in _report.doctorOccupation)
            {
                ukupnp += v.TotalWorkTime;
                zauzeto += v.OccupationWorkTime;
            }
            ExportAsDocument.ExportAsPDF(_report, ukupnp, zauzeto, userController);
        }
    }
}
