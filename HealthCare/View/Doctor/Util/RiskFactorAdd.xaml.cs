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
    /// Interaction logic for RiskFactorAdd.xaml
    /// </summary>
    public partial class RiskFactorAdd : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";
        
        private IList<Diagnosis> _diagnosis;
        private readonly IDiagnosisController _diagnosisControler;

        private RiskFactor _newRiskFactor;

        public RiskFactorAdd()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbAddRiskFactorDiagnosis.KeyDown += new KeyEventHandler(HandleCB);
            this.cbAddRiskFactorStatus.KeyDown += new KeyEventHandler(HandleCB);

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

        public RiskFactor NewRiskFactor
        {
            get { return _newRiskFactor; }
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

        private void btnAddRiskFactor_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                Diagnosis diagnosis = (Diagnosis)cbAddRiskFactorDiagnosis.SelectedValue;
                MedicalStatus status = (MedicalStatus)cbAddRiskFactorStatus.SelectedValue;

                _newRiskFactor = new RiskFactor { Date = DateTime.Now, MedicalStatus = status, riskFactor = new Diagnosis() { Code = diagnosis.Code, Name = diagnosis.Name }};
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
            if (cbAddRiskFactorDiagnosis.SelectedItem != null && cbAddRiskFactorStatus.SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

    }
}
