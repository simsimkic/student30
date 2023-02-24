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
    /// Interaction logic for MedicalHistoryEdit.xaml
    /// </summary>
    public partial class MedicalHistoryEdit : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";

        private IList<Diagnosis> _diagnosis;
        private readonly IDiagnosisController _diagnosisControler;


        private MedicalHistory _newMedicalHistory = new MedicalHistory();

        public MedicalHistoryEdit(MedicalHistory _medicalHistoryForEdit)
        {
            InitializeComponent();
            this.DataContext = this;
            _newMedicalHistory = _medicalHistoryForEdit;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbEditMedicalHistoryDiagnosis.KeyDown += new KeyEventHandler(HandleCB);
            this.cbEditMedicalHistoryStatus.KeyDown += new KeyEventHandler(HandleCB);

            this.cbEditMedicalHistoryStatus.SelectedItem = _newMedicalHistory.MedicalStatus;

            var app = Application.Current as App;

            _diagnosisControler = app.diagnosisController;

            _diagnosis = _diagnosisControler.GetAll().ToList();

            int i = 0;
            foreach(Diagnosis item in _diagnosis)
            {
                if (item.Name.Equals(_newMedicalHistory.Diagnosis.Name))
                    break;
                i++;
            }

            cbEditMedicalHistoryDiagnosis.SelectedIndex = i;

        }

        public IList<Diagnosis> Diagnosis
        {
            get { return _diagnosis; }
            set
            {
                _diagnosis = value;
            }
        }

        private void btnSaveEditMedicalHistory_Click(object sender, RoutedEventArgs e)
        {

            if (isValidInputData())
            {
                Diagnosis diagnosis = (Diagnosis)cbEditMedicalHistoryDiagnosis.SelectedValue;
                MedicalStatus selectedItemStatus = (MedicalStatus)cbEditMedicalHistoryStatus.SelectedValue;

                _newMedicalHistory.Diagnosis = new Diagnosis() { Code = diagnosis.Code, Name = diagnosis.Name };
                _newMedicalHistory.MedicalStatus = selectedItemStatus;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                ShowError(INPUT_ERROR_MESSAGE);
            }

        }


        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();
        public MedicalHistory newMedicalHistory
        {
            get { return _newMedicalHistory; }
        }

        private bool isValidInputData()
        {
            if (cbEditMedicalHistoryDiagnosis.SelectedItem != null && cbEditMedicalHistoryStatus.SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {

                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
