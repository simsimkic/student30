using Controller.OtherDataController;
using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.Diagnosis;
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
    /// Interaction logic for PotentialDiagnosis.xaml
    /// </summary>
    public partial class PotentialDiagnosis : Window
    {
        private const string INPUT_ERROR_SYMPTOMS = "Morate uneti bar jedan simptom";

        private readonly ISymptomController _symptomController;
        private List<Symptom> _addedSymptoms = new List<Symptom>();
        private Diagnosis _selectedDiagnosis;

        private List<Symptom> _symptoms;
        public PotentialDiagnosis()
        {
            InitializeComponent();
            this.DataContext = this;

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbAddPotentialDiagnosis.KeyDown += new KeyEventHandler(HandleCB);

            _symptomController = (Application.Current as App).symptomsController;
            _symptoms = _symptomController.GetAll().ToList();
        }

        public List<Symptom> AddedSymptoms
        {
            get { return _addedSymptoms; }
            set
            {
                _addedSymptoms = value;
            }
        }


        public List<Symptom> Symptoms
        {
            get { return _symptoms; }
        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                ((ComboBox)sender).IsDropDownOpen = true;
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

        private void listBoxSymptoms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            handleListBoxDeleteItem();
        }

        public Diagnosis SelectedDiagnosis()
        {
            return _selectedDiagnosis;
        }



        private void btnshowDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            if (validInput())
            {
                PotentialDiagnosisList inputDialog = new PotentialDiagnosisList(_addedSymptoms);

                if (inputDialog.ShowDialog() == true)
                {
                    _selectedDiagnosis = inputDialog.SelectedDiagnosis();
                    this.DialogResult = true;
                    this.Close();
                }
            }

        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

        private bool validInput()
        {
            if( _addedSymptoms.Count > 0)
            {
                return true;
            }
            else
            {
                ShowError(INPUT_ERROR_SYMPTOMS);
                return false;
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void addPotentialDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            if ((Symptom)cbAddPotentialDiagnosis.SelectedValue != null)
            {
                Symptom simptom = (Symptom)cbAddPotentialDiagnosis.SelectedValue;
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
                    _addedSymptoms.Add(simptom);
                listBoxSymptoms.Items.Refresh();
            }
        }

        private void listBoxSymptoms_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                handleListBoxDeleteItem();
            }
        }
    }
}
