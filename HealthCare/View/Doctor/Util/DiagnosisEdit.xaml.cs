using Controller.DrugController;
using Controller.OtherDataController;
using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.Diagnosis;
using Model.Drug;
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
    public partial class DiagnosisEdit : Window
    {
        private const string INPUT_ERROR_CODE = "Sifra leka mora imati bar 3 karaktera";
        private const string INPUT_ERROR_NAME = "Ime leka mora imati bar 3 karaktera";
        private const string INPUT_ERROR_SYMPTOMS = "Morate uneti bar jedan simptom";
        private const string INPUT_ERROR_THERAPY = "Morate uneti bar jednu terapiju";

        private IList<Symptom> _symptoms = new List<Symptom>();
        private List<Symptom> _addedSymptoms = new List<Symptom>();
        private IList<Drug> _therapies = new List<Drug>();
        private List<Drug> _addedTheraphies = new List<Drug>();


        private readonly ISymptomController _symptomController;
        private readonly IDrugController _drugControler;

        private Diagnosis _newDiagnosis;

        public DiagnosisEdit(Diagnosis _diagnosisForEdit)
        {
            InitializeComponent();
            this.DataContext = this;
            _newDiagnosis = _diagnosisForEdit;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbAddSympthom.KeyDown += new KeyEventHandler(HandleCB);
            this.cbAddTherapy.KeyDown += new KeyEventHandler(HandleCB);

            var app = Application.Current as App;

            tbDiagnosisId.Text = _newDiagnosis.Code;
            tbDiagnosisName.Text = _newDiagnosis.Name;
            tbTheraphy.Text = _newDiagnosis.Theraphy;
            tbDiagnosisId.IsEnabled = false;
            foreach(Symptom item in _newDiagnosis.Symptoms)
            {
                _addedSymptoms.Add(item);
            }

            _symptomController = app.symptomsController;
            _drugControler = app.drugControler;

            _symptoms = _symptomController.GetAll().ToList();

            _therapies = _drugControler.GetApprovedDrugs().ToList();

            _addedTheraphies = _newDiagnosis.TheraphyDrug;

            tbTheraphy.IsEnabled = false;
    
        }
        public IList<Drug> Therapies
        {
            get { return _therapies; }
            set
            {
                _therapies = value;
            }
        }
        public IList<Symptom> Symptoms
        {
            get { return _symptoms; }
            set
            {
                _symptoms = value;
            }
        }

        public List<Symptom> AddedSymptoms
        {
            get { return _addedSymptoms; }
            set
            {
                _addedSymptoms = value;
            }
        }

        public List<Drug> AddedTheraphy
        {
            get { return _addedTheraphies; }
            set
            {
                _addedTheraphies = value;
            }
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {

                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }


        private void btnAddSympthom_Click(object sender, RoutedEventArgs e)
        {
            if ((Symptom)cbAddSympthom.SelectedValue != null)
            {
                Symptom simptom = (Symptom)cbAddSympthom.SelectedValue;
                bool postoji = false;
                foreach (Symptom item in _addedSymptoms)
                {
                    if (item.Name.Equals(simptom.Name))
                    {
                        postoji = true;
                        break;
                    }
                }
                if (!postoji)
                {
                    _addedSymptoms.Add(simptom);
                }

                listBoxSymptoms.Items.Refresh();
            }
        }

        private void listBoxSymptoms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            handleListBoxDeleteItem();
        }

        private void btnSaveEditedDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                string code = tbDiagnosisId.Text;
                string name = tbDiagnosisName.Text;
                string theraphy = tbTheraphy.Text;

                _newDiagnosis.Code = code;
                _newDiagnosis.Name = name;
                _newDiagnosis.Theraphy = theraphy;
                _newDiagnosis.Symptoms = _addedSymptoms;
                _newDiagnosis.TheraphyDrug = _addedTheraphies;
                
                this.DialogResult = true;
                this.Close();
            }

        }

        private bool isValidInputData()
        {
            if (tbDiagnosisId.Text.Length < 3)
            {
                ShowError(INPUT_ERROR_CODE);
                return false;
            }
            else if (tbDiagnosisName.Text.Length < 3)
            {
                ShowError(INPUT_ERROR_CODE);
                return false;
            }
            else if (_addedSymptoms.Count == 0)
            {
                ShowError(INPUT_ERROR_SYMPTOMS);
                return false;
            }
            else if (_addedTheraphies.Count == 0)
            {
                ShowError(INPUT_ERROR_THERAPY);
                return false;
            }
            return true;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

        private void handleListBoxDeleteItem()
        {
            int index = this.listBoxSymptoms.SelectedIndex;
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                _addedSymptoms.RemoveAt(index);
                listBoxSymptoms.Items.Refresh();
            }
        }

        private bool isAddedTheraphy(int id)
        {
            foreach (Drug item in _addedTheraphies)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        private void listBoxSymptoms_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                handleListBoxDeleteItem();
            }
        }

        private void cbAddTherapy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Drug theraphy = (Drug)cbAddTherapy.SelectedValue;
            if (!isAddedTheraphy(theraphy.Id))
            {
                string message = "Zelite da dodate terapiju: ";
                message += theraphy.Name;
                if (ShowYesNoDialog(message))
                {
                    _addedTheraphies.Add(theraphy);
                    string theraphyString = "";
                    theraphyString = theraphy.Manufacturer + " - " + theraphy.Name + " - " + theraphy.Quantity + "mg  " + "\n";
                    tbTheraphy.Text += theraphyString;
                }
            }
            else
            {
                string message = "Zelite da izbrisete terapiju: ";
                message += theraphy.Name;
                if (ShowYesNoDialog(message))
                {
                    foreach(Drug item in _addedTheraphies)
                    {
                        if(item.Id== theraphy.Id)
                        {
                            _addedTheraphies.Remove(item);
                            break;
                        }
                    }

                    string theraphyString = "";
                    foreach (Drug item in _addedTheraphies)
                    {
    
                        theraphyString += item.Manufacturer + " - " + item.Name + " - " + item.Quantity + "mg  " + "\n";
                    }
                    tbTheraphy.Text = theraphyString;
                }
            }
        }

        private bool ShowYesNoDialog(string YesNoquestion)
        {
            return new SubmitCancelDialog(YesNoquestion, this).ShowDialog() ?? true;
        }
    }
}
