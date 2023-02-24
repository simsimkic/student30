using Controller.DiagnosisController;
using Controller.OtherDataController;
using HealthCare;
using HealthCare.View.Patient;
using HealthCareClinic.View.Doctor;
using Model.Diagnosis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for DiagnosisList.xaml
    /// </summary>
    public partial class DiagnosisList : Window
    {
        private const string SELECTED_ERROR_MESSAGE = "Nije selektovan nijedan podatak";
        private const string DELETED_QUESTION_MESSAGE = "Da li zelite da obrisete?";
        private const string NOT_UNIQUES_MESSAGE = "Dijagnoza sa datim kodom vec postoji";

        private readonly IDiagnosisController _diagnosisController;
        private Boolean filtered = false;
        private IList<Diagnosis> _diagnosis;


        public DiagnosisList()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;

             _diagnosisController = app.diagnosisController;

             _diagnosis = _diagnosisController.GetAll().ToList();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }


        public IList<Diagnosis> Diagnosis
        {
            get { return _diagnosis; }
            set
            {
                _diagnosis = value;
            }
        }


        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void btnEditDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            Diagnosis diagnosisForEdit = (Diagnosis)dgDiagnosis.SelectedItem;

            if ((Diagnosis)dgDiagnosis.SelectedItem == null)
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
            else
            {
                DiagnosisEdit editDialog = new DiagnosisEdit(diagnosisForEdit);
                if (editDialog.ShowDialog() == true)
                {
                    if (_diagnosisController.Update(diagnosisForEdit) == null)
                    {
                        ShowError(NOT_UNIQUES_MESSAGE);
                    }
                    dgDiagnosis.Items.Refresh();
                }
            }
        }

        private void btnAddDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            Diagnosis _newDiagnosis;
            DiagnosisAdd inputDialog = new DiagnosisAdd();
            if (inputDialog.ShowDialog() == true)
            {
                _newDiagnosis = inputDialog.NewDiagnosis;
                if(_diagnosisController.Create(_newDiagnosis) != null)
                {
                    _diagnosis.Add(_newDiagnosis);
                }
                else
                {
                    ShowError(NOT_UNIQUES_MESSAGE);
                }
                dgDiagnosis.Items.Refresh();
            }

        }

        private void btnDeleteDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            if (dgDiagnosis.SelectedItem != null)
            {
                if (ShowYesNoDialog(DELETED_QUESTION_MESSAGE))
                {
                    Diagnosis _diagnosisForDelete = (Diagnosis)dgDiagnosis.SelectedItem;
                    _diagnosisController.Delete(_diagnosisForDelete);
                    _diagnosis.Remove(_diagnosisForDelete);
                    dgDiagnosis.ItemsSource = null;
                    dgDiagnosis.ItemsSource = _diagnosis;
                    dgDiagnosis.Items.Refresh();
                    if (filtered)
                    {
                        btnSearchDiagnosis_Click(sender, e);
                    }
                    
                }
            }
            else
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
        }

        private void ShowError(string message) => new MessageDialog(message, this).ShowDialog();


        private bool ShowYesNoDialog(string YesNoquestion)
        {
            return new SubmitCancelDialog(YesNoquestion, this).ShowDialog() ?? true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnAddDiagnosis_Click(sender, e);
            }

            if (e.Key == Key.E && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnEditDiagnosis_Click(sender, e);
            }

            if (e.Key == Key.D && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnDeleteDiagnosis_Click(sender, e);
            }

            if (e.Key == Key.S && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                tbSearchDiagnosis.Focus();
            }
        }

        private void btnSearchDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            if (isValidSearchInput(tbSearchDiagnosis.Text))
            {
                dgDiagnosis.ItemsSource = new List<Diagnosis>();
                dgDiagnosis.ItemsSource = _diagnosisController.GetFilteredDiagnosisByNameOrCode(tbSearchDiagnosis.Text).ToList(); 
                dgDiagnosis.Items.Refresh();
                filtered = true;
            }
            else
            {
                dgDiagnosis.ItemsSource = new List<Diagnosis>();
                dgDiagnosis.ItemsSource = _diagnosisController.GetAll().ToList();
                dgDiagnosis.Items.Refresh();
                filtered = false;
            }
        }

        bool isValidSearchInput(string input)
        {
            if(input!="" && input.Count() >= 3)
            {
                return true;
            }else if (input.Count() < 3 && input!="")
            {
                ShowError("Kriterijum pretrage mora imati vise od 2 slova");
                return false;
            }
                return false;
        }


    }
}
