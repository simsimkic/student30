using Controller.OtherDataController;
using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.Drug;
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
    /// Interaction logic for AllergyEdit.xaml
    /// </summary>
    public partial class AllergyEdit : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";
        private IList<Allergen> _alergies;
        private IAllergyController _allergyController;


        private RiskAllergies _newRiskAllergies;
        public AllergyEdit(RiskAllergies _riskAllergyForEdit)
        {
            InitializeComponent();
            this.DataContext = this;
            _newRiskAllergies = _riskAllergyForEdit;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbEditAllergyDiagnosis.KeyDown += new KeyEventHandler(HandleCB);
            this.cbEditAllergyStatus.KeyDown += new KeyEventHandler(HandleCB);

            this.cbEditAllergyStatus.SelectedItem = _newRiskAllergies.MedicalStatus;

            var app = Application.Current as App;

            _allergyController = app.allergyController;


            _alergies = _allergyController.GetAll().ToList();

            int i = 0;
            foreach (Allergen item in _alergies)
            {
                if (item.Name.Equals(_newRiskAllergies.Allergen.Name))
                    break;
                i++;
            }

            cbEditAllergyDiagnosis.SelectedIndex = i;
        }

        public IList<Allergen> Alergies
        {
            get { return _alergies; }
            set
            {
                _alergies = value;
                //OnPropertyChanged("MedicalHistory");
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

        private void btnEditAllergy_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                Allergen alergen = (Allergen)cbEditAllergyDiagnosis.SelectedValue;
                MedicalStatus status = (MedicalStatus)cbEditAllergyStatus.SelectedValue;

                _newRiskAllergies.Allergen = alergen;
                _newRiskAllergies.MedicalStatus = status;
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
            if (cbEditAllergyDiagnosis.SelectedItem != null && cbEditAllergyStatus.SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();
    }
}
