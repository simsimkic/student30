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
    /// Interaction logic for RiskFactorEdit.xaml
    /// </summary>
    public partial class RiskFactorEdit : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";

        private IList<Diagnosis> _diagnosis;
        private readonly IDiagnosisController _diagnosisControler;

        private RiskFactor _riskFactorForEdit;

        public RiskFactorEdit(RiskFactor riskFactorForEdit)
        {
            InitializeComponent();
            this.DataContext = this;
            _riskFactorForEdit = riskFactorForEdit;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbEditRiskFactorDiagnosis.KeyDown += new KeyEventHandler(HandleCB);
            this.cbEditRiskFactorStatus.KeyDown += new KeyEventHandler(HandleCB);

            this.cbEditRiskFactorStatus.SelectedItem = _riskFactorForEdit.MedicalStatus;

            var app = Application.Current as App;

            _diagnosisControler = app.diagnosisController;

            _diagnosis = _diagnosisControler.GetAll().ToList();

            int i = 0;
            foreach (Diagnosis item in _diagnosis)
            {
                if (item.Name.Equals(_riskFactorForEdit.riskFactor.Name))
                    break;
                i++;
            }

            cbEditRiskFactorDiagnosis.SelectedIndex = i;
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

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private void btnEditRiskFactor_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                Diagnosis diagnosis = (Diagnosis)cbEditRiskFactorDiagnosis.SelectedValue;
                MedicalStatus status = (MedicalStatus)cbEditRiskFactorStatus.SelectedValue;

                _riskFactorForEdit.riskFactor = new Diagnosis() { Code = diagnosis.Code, Name = diagnosis.Name };
                _riskFactorForEdit.MedicalStatus = status;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                ShowError(INPUT_ERROR_MESSAGE);
            }
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();


        private bool isValidInputData()
        {
            if (cbEditRiskFactorDiagnosis.SelectedItem != null && cbEditRiskFactorStatus.SelectedItem != null)
            {
                return true;
            }
            return false;
        }
    }
}
