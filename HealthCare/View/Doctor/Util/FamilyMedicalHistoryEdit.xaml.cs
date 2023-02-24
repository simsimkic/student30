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
    /// Interaction logic for FamilyMedicalHistoryEdit.xaml
    /// </summary>
    public partial class FamilyMedicalHistoryEdit : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";

        private IList<Diagnosis> _diagnosis;

        private FamilyRiskFactor _familyRiskFactorForEdit = new FamilyRiskFactor();

        private readonly IDiagnosisController _diagnosisControler;


        public FamilyMedicalHistoryEdit(FamilyRiskFactor familyRiskFactorForEdit)
        {
            InitializeComponent();
            this.DataContext = this;
            _familyRiskFactorForEdit = familyRiskFactorForEdit;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbEditFMedicalHistoryDiagnosis.KeyDown += new KeyEventHandler(HandleCB);
            this.cbEditFMedicalHistorySrodstvo.KeyDown += new KeyEventHandler(HandleCB);

            this.cbEditFMedicalHistorySrodstvo.SelectedItem = _familyRiskFactorForEdit.Relationship;

            var app = Application.Current as App;

            _diagnosisControler = app.diagnosisController;

            _diagnosis = _diagnosisControler.GetAll().ToList();

            int i = 0;
            foreach (Diagnosis item in _diagnosis)
            {
                if (item.Name.Equals(_familyRiskFactorForEdit.familyRiskFactor.Name))
                    break;
                i++;
            }

            cbEditFMedicalHistoryDiagnosis.SelectedIndex = i;
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
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {

                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }

        private void btnEditFMedicalHistory_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                Diagnosis diagnosis = (Diagnosis)cbEditFMedicalHistoryDiagnosis.SelectedValue;
                Relationship relationship = (Relationship)cbEditFMedicalHistorySrodstvo.SelectedValue;

                _familyRiskFactorForEdit.familyRiskFactor = new Diagnosis() { Code = diagnosis.Code, Name = diagnosis.Name };
                _familyRiskFactorForEdit.Relationship = relationship;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                ShowError(INPUT_ERROR_MESSAGE);
            }
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

        public FamilyRiskFactor editedMedicalHistory
        {
            get { return _familyRiskFactorForEdit; }
        }

        private bool isValidInputData()
        {
            if (cbEditFMedicalHistoryDiagnosis.SelectedItem != null && cbEditFMedicalHistorySrodstvo.SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
