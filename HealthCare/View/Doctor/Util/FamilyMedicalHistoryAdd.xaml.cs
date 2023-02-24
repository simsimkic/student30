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
    /// Interaction logic for FamilyMedicalHistoryAdd.xaml
    /// </summary>
    public partial class FamilyMedicalHistoryAdd : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";

        private IList<Diagnosis> _diagnosis;

        private readonly IDiagnosisController _diagnosisControler;


        private FamilyRiskFactor _newFamilyRiskFactor;
        public FamilyMedicalHistoryAdd()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbAddFMedicalHistoryDiagnosis.KeyDown += new KeyEventHandler(HandleCB);
            this.cbAddFMedicalHistorySrodstvo.KeyDown += new KeyEventHandler(HandleCB);

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

        public FamilyRiskFactor NewFamilyRiskFactor
        {
            get { return _newFamilyRiskFactor; }
        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {

                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }

        private void btnAddFMedicalHistory_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                Diagnosis diagnosis = (Diagnosis)cbAddFMedicalHistoryDiagnosis.SelectedValue;
                Relationship relationship = (Relationship)cbAddFMedicalHistorySrodstvo.SelectedValue;

                _newFamilyRiskFactor = new FamilyRiskFactor { Date = DateTime.Now, Relationship = relationship, familyRiskFactor = new Diagnosis() { Code = diagnosis.Code, Name = diagnosis.Name } };
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
            if (cbAddFMedicalHistoryDiagnosis.SelectedItem != null && cbAddFMedicalHistorySrodstvo.SelectedItem != null)
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
