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
    /// Interaction logic for AllergyAdd.xaml
    /// </summary>
    public partial class AllergyAdd : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";
        private IList<Allergen> _alergies;
        private IAllergyController _allergyController;
        private RiskAllergies _newRiskAllergies;


        public AllergyAdd()
        {
            InitializeComponent();
            this.DataContext = this;

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbAddAllergyDiagnosis.KeyDown += new KeyEventHandler(HandleCB);
            this.cbEditAllergyStatus.KeyDown += new KeyEventHandler(HandleCB);

            var app = Application.Current as App;

            _allergyController = app.allergyController;


            _alergies = _allergyController.GetAll().ToList();
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
        private void btnAddAllergy_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                Allergen allergy = (Allergen)cbAddAllergyDiagnosis.SelectedValue;
                MedicalStatus status = (MedicalStatus)cbEditAllergyStatus.SelectedValue;
               
                _newRiskAllergies = new RiskAllergies { Date = DateTime.Now, MedicalStatus = status, Allergen = allergy };
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
            if (cbAddAllergyDiagnosis.SelectedItem != null && cbEditAllergyStatus.SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        public RiskAllergies NewRiskAllergy
        {
            get { return _newRiskAllergies; }
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();
    }
}
