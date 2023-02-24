using Controller.DiagnosisController;
using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.Diagnosis;
using Model.MedicalRecords;
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
    /// Interaction logic for MedicalHistoryAdd.xaml
    /// </summary>
    public partial class MedicalHistoryAdd : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";

        private IList<Diagnosis> _diagnosis;
        private readonly IDiagnosisController _diagnosisControler;

        private MedicalHistory _newMedicalHistory;
        public MedicalHistoryAdd()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbAddMedicalHistoryDiagnosis.KeyDown += new KeyEventHandler(HandleCB);
            this.cbAddMedicalHistoryStatus.KeyDown += new KeyEventHandler(HandleCB);

            var app = Application.Current as App;

            _diagnosisControler = app.diagnosisController;

            _diagnosis = _diagnosisControler.GetAll().ToList();

        }


        public IList<Diagnosis> Diagnosis
        {
            get { return _diagnosis; }
            set
            {
                _diagnosis = value;
            }
        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key==Key.Space)
            {
                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }


        private void btnSaveMedicalHistory_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                Diagnosis diagnosis = (Diagnosis)cbAddMedicalHistoryDiagnosis.SelectedValue;
                MedicalStatus selectedItemStatus = (MedicalStatus)cbAddMedicalHistoryStatus.SelectedValue;

                _newMedicalHistory = new MedicalHistory { Date = DateTime.Now, MedicalStatus = selectedItemStatus, Diagnosis = new Diagnosis() { Code = diagnosis.Code, Name=diagnosis.Name } };
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                ShowError(INPUT_ERROR_MESSAGE);
            }
            
            
        }

        private bool isValidInputData()
        {
            if(cbAddMedicalHistoryDiagnosis.SelectedItem!=null && cbAddMedicalHistoryStatus.SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog(); 


        public MedicalHistory newMedicalHistory
        {
            get { return _newMedicalHistory; }
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
