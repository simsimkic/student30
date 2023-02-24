using Controller.ReportController;
using HealthCare;
using HealthCare.View.Doctor.Util;
using HealthCareClinic.View.Doctor;
using Model.DataReport;
using Model.MedicalRecords;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for TreatmentReview.xaml
    /// </summary>
    public partial class TreatmentReview : Window, INotifyPropertyChanged
    {
        private string INPUT_ERROR_MESSAGE = "Morate uneti datum od i datum do";
        private IList<Treatment> lista;
        private Patient _patient;
        private readonly IReportController _reportController;
        public TreatmentReview(IList<Treatment> treatment, Patient patient)
        {
            InitializeComponent();
            lista = treatment;
            _patient = patient;
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.dPickerReviewTreatmentsFrom.PreviewKeyDown += new KeyEventHandler(HandleDP);
            _reportController = (Application.Current as App).reportController;
            this.dPickerReviewTreatmentsTo.PreviewKeyDown += new KeyEventHandler(HandleDP);
        }


        private void HandleDP(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {

                ((DatePicker)sender).IsDropDownOpen = true;
            }
        }

        private DateTime _dateFrom = DateTime.Now;
        public DateTime dateFrom
        {
            get { return _dateFrom; }
            set
            {
                this._dateFrom = value;
                OnPropertyChanged("dateFrom");
            }
        }

        private DateTime _dateTo = DateTime.Now;

        public DateTime dateTo
        {
            get { return _dateTo; }
            set
            {
                this._dateTo = value;
                OnPropertyChanged("dateTo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private void btnPrintReviewTreatments_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                ReviewPDFGenerator.ExportAsPDF(_reportController.GetReportForPatientTreatment(_patient,_dateFrom,_dateTo));
                this.Close();
            }
            else
            {
                ShowError(INPUT_ERROR_MESSAGE);
            }
        }

        private IList<Treatment> generateListOfTreatment()
        {
            IList<Treatment> pretrazenaLista = new List<Treatment>();
            foreach (Treatment item in lista)
            {
                Console.WriteLine(item.Date.ToString() + "     -      " + _dateTo.ToString() + "datefrom: " + dateFrom.ToString());
                if(DateTime.Compare(item.Date.Date, _dateTo.Date) <=0 && DateTime.Compare(item.Date.Date, _dateFrom.Date) >= 0)
                {
                    pretrazenaLista.Add(item);
                }
            }
            return pretrazenaLista;
        }

        private bool isValidInputData()
        {
            if (_dateFrom != null && _dateFrom != null)
            {
                return true;
            }
            return false;
        }


        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
