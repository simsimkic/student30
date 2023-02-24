using Controller.UserControllers;
using HealthCare;
using HealthCareClinic.View;
using HealthCareClinic.View.Doctor;
using Model.DataFiltration;
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

namespace HealthCareClinic.HCI
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        private IList<User> _patients;
        private readonly IUserController _userController;
        private const string INPUT_ERROR_MESSAGE = "Morate uneti bar jedan kriterijum pretrage";
        
        private const string INPUT_ERROR_NUMBER = "JBMG i broj kartona moraju biti numericke vrednosti";

        public PatientWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _userController = app.userController;

            _patients = _userController.GetPatientForDoctor((Doctor)app._currentUser).ToList();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            tbPatientName.Focus();
        }

        private IEnumerable<Patient> getPatient()
        {
            IList<Patient> _lista = new List<Patient>();
            return _lista;
    }

        void btnMedicalRecord(object sender, RoutedEventArgs e)
        {
            Patient _patient = (Patient)tablePatientSearch.SelectedItem;
            
            Record dialog = new Record(_patient.Id);
            dialog.ShowDialog();
        }

        public IList<User> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
            }
        }


        private void btnSearchPatient_Click(object sender, RoutedEventArgs e)
        {
            if (validInput())
            {
                PatientFitler pf = new PatientFitler();
                if (tbPatientJBMG.Text.Length > 0)
                {
                    pf.JMBG = tbPatientJBMG.Text;
                }
                else
                {
                    pf.JMBG = "";
                }

                if (tbPatientCardID.Text.Length > 0)
                {
                    pf.RecordNumber = Int32.Parse(tbPatientCardID.Text);
                }
                else
                {
                    pf.RecordNumber = -1;
                }

                if (tbPatientName.Text.Length > 0)
                {
                    pf.Name =tbPatientName.Text;
                }
                else
                {
                    pf.Name = "";
                }

                if (tbPatientSurname.Text.Length > 0)
                {
                    pf.Surname = tbPatientSurname.Text;
                }
                else
                {
                    pf.Surname = "";
                }

                if (pf.JMBG == "" && pf.RecordNumber == -1 && pf.Name == "" && pf.Surname == "")
                {
                    tablePatientSearch.ItemsSource = new List<Patient>();
                    tablePatientSearch.ItemsSource = _patients;
                    tablePatientSearch.Items.Refresh();
                }
                else
                {
                    tablePatientSearch.ItemsSource = new List<Patient>();
                    tablePatientSearch.ItemsSource = _userController.GetFilteredPatient(pf, (Doctor)(Application.Current as App)._currentUser).ToList();
                    tablePatientSearch.Items.Refresh();
                }
            }
        }

        public IEnumerable<Patient> GetFilteredPatient(PatientFitler patientFilter)
            => getPatient().Where(x => ((patientFilter.JMBG != "") ? x.JMBG == patientFilter.JMBG : true) &&
                                   ((patientFilter.RecordNumber != -1) ? x.MedicalRecord.Id == patientFilter.RecordNumber : true) &&
                                   ((patientFilter.Name != "") ? x.Name.Equals(patientFilter.Name) : true) &&
                                   ((patientFilter.Surname != "") ? x.Surname.Equals(patientFilter.Surname) : true));

        private bool validInput()
        {
            if (tbPatientJBMG.Text.Length > 0)
            {
                try
                {
                    long.Parse(tbPatientJBMG.Text);
                }
                catch (Exception e)
                {
                    ShowError(INPUT_ERROR_NUMBER);
                    return false;
                }
            }

            if (tbPatientCardID.Text.Length > 0)
            {
                try
                {
                    Int32.Parse(tbPatientCardID.Text);
                }
                catch (Exception e)
                {
                    ShowError(INPUT_ERROR_NUMBER);
                    return false;
                }
            }

            return true;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();


        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void tablePatientSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key==Key.Enter)
            {
                Patient _patient = (Patient)tablePatientSearch.SelectedItem;

                if (_patient != null)
                {
                    Record dialog = new Record(_patient.Id);
                    dialog.ShowDialog();
                }
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                tbPatientName.Focus();
            }
        }
    }
}
