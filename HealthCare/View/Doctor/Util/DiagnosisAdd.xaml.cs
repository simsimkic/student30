using Controller.DiagnosisController;
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
using System.Xaml;

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for DiagnosisAdd.xaml
    /// </summary>
    public partial class DiagnosisAdd : Window
    {
        private const string INPUT_ERROR_CODE = "Sifra leka mora imati bar 3 karaktera";
        private const string INPUT_ERROR_NAME = "Ime leka mora imati bar 3 karaktera";
        private const string INPUT_ERROR_SYMPTOMS = "Morate uneti bar jedan simptom";
        private const string INPUT_ERROR_THERAPY = "Morate uneti bar jednu terapiju";

        private IList<Symptom> _symptoms = new List<Symptom>();
        private List<Symptom> _addedSymptoms = new List<Symptom>();
        private IList<Drug> _therapies = new List<Drug>();
        private List<Drug> _addedTheraphies = new List<Drug>();

        private Diagnosis _newDiagnosis;


        private readonly ISymptomController _symptomController;
        private readonly IDrugController _drugControler;
        public DiagnosisAdd()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbAddSympthom.KeyDown += new KeyEventHandler(HandleCB);
            this.cbAddTherapy.KeyDown += new KeyEventHandler(HandleCB);

            var app = Application.Current as App;

            _symptomController = app.symptomsController;
            _drugControler = app.drugControler;

            _symptoms = _symptomController.GetAll().ToList();
            _therapies = _drugControler.GetApprovedDrugs().ToList();

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
                Symptom symptom = (Symptom)cbAddSympthom.SelectedValue;
                bool postoji = false;
                foreach(Symptom item in _addedSymptoms)
                {
                    if (item.Name.Equals(symptom.Name))
                    {
                        postoji = true;
                        break;
                    }
                }
                if(!postoji)
                    _addedSymptoms.Add(symptom);
                listBoxSymptoms.Items.Refresh();
            }

        }

        private void handleListBoxDeleteItem()
        {
            int index = this.listBoxSymptoms.SelectedIndex;
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                _addedSymptoms.RemoveAt(index);
                listBoxSymptoms.Items.Refresh();
            }
        }

        private void btnAddDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                var app = Application.Current as App;

                String code = tbDiagnosisId.Text ;
                String name = tbDiagnosisName.Text;
                String theraphy = tbTheraphy.Text;
                String doctor = app._currentUser.Name + " " + app._currentUser.Surname;
                _newDiagnosis = new Diagnosis { Date = DateTime.Now, Code = code, Name = name, Theraphy = theraphy, Symptoms = _addedSymptoms, TheraphyDrug = _addedTheraphies,DoctorCreated= doctor };
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
            }else if(tbDiagnosisName.Text.Length < 3)
            {
                ShowError(INPUT_ERROR_CODE);
                return false;
            } else if (_addedSymptoms.Count==0)
            {
                ShowError(INPUT_ERROR_SYMPTOMS);
                return false;
            }else if (_addedTheraphies.Count == 0)
            {
                ShowError(INPUT_ERROR_THERAPY);
                return false;
            }
            return true;
        }

        public Diagnosis NewDiagnosis
        {
            get { return _newDiagnosis; }
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

        private void listBoxSymptoms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            handleListBoxDeleteItem();
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
                    _addedTheraphies.Remove(theraphy);
                    string theraphyString = "";
                    foreach (Drug item in _addedTheraphies)
                    {
                        theraphyString += item.Manufacturer + " - " + item.Name + " - " + item.Quantity + "mg  " + "\n";
                    }
                    tbTheraphy.Text = theraphyString;
                }
            }
        }

        private bool isAddedTheraphy(int id)
        {
            foreach(Drug item in _addedTheraphies)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ShowYesNoDialog(string YesNoquestion)
        {
            return new SubmitCancelDialog(YesNoquestion, this).ShowDialog() ?? true;
        }

        private void cbAddSympthom_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnAddSympthom_Click(sender, e);
            }
        }
    }
}
