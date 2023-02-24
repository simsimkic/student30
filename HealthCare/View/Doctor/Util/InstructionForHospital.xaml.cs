using Controller.OtherDataController;
using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.MedicalRecords;
using Model.Rooms;
using Model.Users;
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
using System.Windows.Shapes;

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for InstructionForHospital.xaml
    /// </summary>
    public partial class InstructionForHospital : Window
    {
        private const string INPUT_ERROR_SECTOR = "Morate navesti odeljenje";
        private const string INPUT_ERROR_DATE = "Morate navesti datum";
        private const string INPUT_ERROR_NOTE = "Morate navesti napomenu";

        private readonly ISectorController _sectorController;

        private HospitalTreatment _refferal;
        private Patient patient;

        private List<Sector> _sectors = new List<Sector>();

        private bool confirm = false;

        public InstructionForHospital(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.dpStartDate.PreviewKeyDown += new KeyEventHandler(HandleDP);
            this.cbSector.KeyDown += new KeyEventHandler(HandleCB);
            this.patient = patient;
            var app = Application.Current as App;
            _sectorController = app.sectorController;
            _sectors = _sectorController.GetAll().ToList();
        }

        public HospitalTreatment NewRefferal
        {
            get { return _refferal; }
        }

        public List<Sector> Sektori
        {
            get { return _sectors; }
        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }

        private void HandleDP(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                ((DatePicker)sender).IsDropDownOpen = true;
            }
        }
        private void btnSendHospitalInstruction_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                HospitalTreatment refferal = new HospitalTreatment();
                refferal.Sector = (Sector)cbSector.SelectedItem;
                refferal.Date = (DateTime)dpStartDate.SelectedDate;
                refferal.Note = tbNote.Text;
                refferal.Patient = patient;
                _refferal = refferal;
                this.DialogResult = true;
                this.confirm = true;
                this.Close();
            }
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();


        private bool isValidInputData()
        {
            if (dpStartDate.SelectedDate == null)
            {
                ShowError(INPUT_ERROR_DATE);
                return false;
            }else if (cbSector.SelectedItem == null)
            {
                ShowError(INPUT_ERROR_SECTOR);
                return false;
            }
            else if (tbNote.Text == "")
            {
                ShowError(INPUT_ERROR_NOTE);
                return false;
            }
            return true;
        }

        public bool isConfirmed()
        {
            return confirm;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
