using HealthCareClinic.View.Doctor;
using HealthCareClinic.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Model.MedicalRecords;
using Controller.MedicalRecordControllers;
using HealthCare;
using Model.Users;
using Model.Diagnosis;
using Controller;
using System.IO;
using HealthCareClinic.HCI;
using Controller.DiagnosisController;
using Controller.UserControllers;
using Controller.AppointmenController;

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for Record.xaml
    /// </summary>
    public partial class Record : Window
    {
        private const string SELECTED_ERROR_MESSAGE = "Nije selektovan nijedan podatak";
        private const string DELETED_QUESTION_MESSAGE = "Da li zelite da obrisete?";
        private const string PATIENT_HASNOT_APPOINTMENT = "Ne mozete otvoriti pregled jer pacijent nema zakazan termin";

        private readonly IRecordController _medicalRecordController;
        private readonly IUserController _userController;

        private readonly ICUDController<MedicalHistory> _medicalHistoryController;
        private readonly ICUDController<FamilyRiskFactor> _familyMedicalHistoryController;
        private readonly ICUDController<RiskFactor> _riskFactorController;
        private readonly ICUDController<RiskAllergies> _riskAllergiesController;
        private readonly ICUDController<Immunization> _immunizationController;
        private readonly ITreatmentController _treatmentController;
        private readonly IDiagnosisController _diagnosisController;
        private readonly IAppointmentController _appointmentController;

        private MedicalRecord _record;
        private IList<MedicalHistory> _medicalHistory;
        private IList<FamilyRiskFactor> _familyMedicalHistory;
        private IList<RiskFactor> _riskFactor;
        private IList<RiskAllergies> _riskAllergies;
        private IList<Immunization> _immunization;
        private IList<Treatment> _treatment;
        private Patient _patient;
        private string _note;


        public Record(int patientId)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;

            _medicalRecordController = app.medicalRecordController;
            _medicalHistoryController = app.medicalHistoryController;
            _familyMedicalHistoryController = app.familyMedicalHistoryController;
            _riskFactorController = app.riskFactorController;
            _riskAllergiesController = app.riskAllergiesController;
            _immunizationController = app.immunizationController;
            _treatmentController = app.treatmentController;
            _diagnosisController = app.diagnosisController;
            _userController = app.userController;
            _appointmentController = app.appointmentController;

            _record = _medicalRecordController.GetRecordForPatient(patientId);
            _patient = (Patient)_userController.GetFromID(patientId);

            _medicalHistory = _record.MedicalHistory;
            
            

            _familyMedicalHistory = _record.FamilyRiskFactor;
            _riskFactor = _record.RiskFactor;
            _riskAllergies = _record.RiskAllergies;
            _note = _record.Note;
            _immunization = _record.Immunization;
            _treatment = _record.Treatment;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        public Patient CurrentPatient
        {
            get { return _patient; } 
        }
        public String Note
        {
            get { return _note; }
            set
            {
                _note = value;
            }
        }


        public IList<Treatment> Treatments
        {
            get { return _treatment; }
            set
            {
                _treatment = value;
            }
        }


        public IList<MedicalHistory> MedicalHistory
        {
            get { return _medicalHistory; }
            set
            {
                _medicalHistory = value;
            }
        }
        public IList<FamilyRiskFactor> FamilyMedicalHistory
        {
            get { return _familyMedicalHistory; }
            set
            {
                _familyMedicalHistory = value;
            }
        }

        public IList<RiskFactor> RiskFactor
        {
            get { return _riskFactor; }
            set
            {
                _riskFactor = value;
            }
        }

        public IList<RiskAllergies> RiskAllergies
        {
            get { return _riskAllergies; }
            set
            {
                _riskAllergies = value;
            }
        }

        public IList<Immunization> Immunization
        {
            get { return _immunization; }
            set
            {
                _immunization = value;
            }
        }

        private void btnEditPatientInformation_Click(object sender, RoutedEventArgs e)
        {
            EditPatientInformation editDialog = new EditPatientInformation(_patient,_record);
            if (editDialog.ShowDialog() == true)
            {
                lblRHFactor.Content = _patient.RhFactor.ToString();
                lblBloodType.Content = _patient.BloodType.ToString();
                tbNapomena.Text = _record.Note;
                _userController.Update(_patient);
                _medicalRecordController.Update(_record);
            }
        }

        private void btnReviewTreatments_Click(object sender, RoutedEventArgs e)
        {
            TreatmentReview dialog = new TreatmentReview(_treatment,_patient);
            dialog.ShowDialog();
        }

        private void btnCreateNewTreatment_Click(object sender, RoutedEventArgs e)
        {
            if (patientHaveTreatment())
            {
                Treatment _newTreatment;
                NewTreatment inputDialog = new NewTreatment(_record, _patient);
                if (inputDialog.ShowDialog() == true)
                {
                    _newTreatment = inputDialog.newTreatment;
                    _treatmentController.AddTreatmentToRecord(_record, _newTreatment);
                    dgTreatment.Items.Refresh();
                }
            }
            else
            {
                ShowError(PATIENT_HASNOT_APPOINTMENT);
            }
        }

        private bool patientHaveTreatment()
        {
            return _appointmentController.HasPatientAppointment(_patient);
        }

        private void btnAddMedicalHistory_Click(object sender, RoutedEventArgs e)
        {
            MedicalHistory _newMedicalHistory;
            MedicalHistoryAdd inputDialog = new MedicalHistoryAdd();
            if (inputDialog.ShowDialog() == true)
            {
                _newMedicalHistory = inputDialog.newMedicalHistory;
                _medicalHistoryController.Create(_record, _newMedicalHistory);
                dgMedicalHistory.Items.Refresh();
            }
        }

        private void btnEditMedicalHistory_Click(object sender, RoutedEventArgs e)
        {
            MedicalHistory _medicalHistoryForEdit = (MedicalHistory)dgMedicalHistory.SelectedItem;
            
            if ((MedicalHistory)dgMedicalHistory.SelectedItem == null)
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
            else
            {
                MedicalHistoryEdit editDialog = new MedicalHistoryEdit(_medicalHistoryForEdit);
                if (editDialog.ShowDialog() == true)
                {
                    _medicalHistoryController.Update(_record);
                    dgMedicalHistory.Items.Refresh();
                }
            }
        }

        private void btnDeleteMedicalHistory_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedicalHistory.SelectedItem != null)
            {
                if (ShowYesNoDialog(DELETED_QUESTION_MESSAGE))
                {
                    MedicalHistory _medicalHistoryForDelete = (MedicalHistory)dgMedicalHistory.SelectedItem;
                    _medicalHistoryController.Delete(_record, _medicalHistoryForDelete);
                    dgMedicalHistory.Items.Refresh();
                }
            }
            else
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
            
        }

        

        private void btnAddFMedicalHistory_Click(object sender, RoutedEventArgs e)
        {
            FamilyRiskFactor _newFamilyMedicalHistory;
            FamilyMedicalHistoryAdd inputDialog = new FamilyMedicalHistoryAdd();
            if (inputDialog.ShowDialog() == true)
            {
                _newFamilyMedicalHistory = inputDialog.NewFamilyRiskFactor;
                _familyMedicalHistoryController.Create(_record, _newFamilyMedicalHistory);
                dgFamilyMedicalHistory.Items.Refresh();
            }

        }

        private void btnEditFMedicalHistory_Click(object sender, RoutedEventArgs e)
        {
            FamilyRiskFactor _familyMedicalHistoryForEdit = (FamilyRiskFactor)dgFamilyMedicalHistory.SelectedItem;

            if ((FamilyRiskFactor)dgFamilyMedicalHistory.SelectedItem == null)
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
            else
            {
                FamilyMedicalHistoryEdit editDialog = new FamilyMedicalHistoryEdit(_familyMedicalHistoryForEdit);
                if (editDialog.ShowDialog() == true)
                {
                    _familyMedicalHistoryController.Update(_record);
                    dgFamilyMedicalHistory.Items.Refresh();
                }
            }
        }

        private void btnDeleteFMedicalHistory_Click(object sender, RoutedEventArgs e)
        {
            if (dgFamilyMedicalHistory.SelectedItem != null)
            {
                if (ShowYesNoDialog(DELETED_QUESTION_MESSAGE))
                {
                    FamilyRiskFactor _familyMedicalHistoryForDelete = (FamilyRiskFactor)dgFamilyMedicalHistory.SelectedItem;
                    _familyMedicalHistoryController.Delete(_record, _familyMedicalHistoryForDelete);
                    dgFamilyMedicalHistory.Items.Refresh();
                }
            }
            else
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
        }

        private void btnAddRiskFactor_Click(object sender, RoutedEventArgs e)
        {
            RiskFactor _newRiskFactor;
            RiskFactorAdd inputDialog = new RiskFactorAdd();
            if (inputDialog.ShowDialog() == true)
            {
                _newRiskFactor = inputDialog.NewRiskFactor;
                _riskFactorController.Create(_record, _newRiskFactor);
                dgRiskFactor.Items.Refresh();
            }
        }

        private void btnEditRiskFactor_Click(object sender, RoutedEventArgs e)
        {
            RiskFactor _riskFactorForEdit = (RiskFactor)dgRiskFactor.SelectedItem;

            if ((RiskFactor)dgRiskFactor.SelectedItem == null)
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
            else
            {
                RiskFactorEdit editDialog = new RiskFactorEdit(_riskFactorForEdit);
                if (editDialog.ShowDialog() == true)
                {
                    _riskFactorController.Update(_record);
                    dgRiskFactor.Items.Refresh();
                }
            }
        }

        private void btnDeleteRiskFactor_Click(object sender, RoutedEventArgs e)
        {
            if (dgRiskFactor.SelectedItem != null)
            {
                if (ShowYesNoDialog(DELETED_QUESTION_MESSAGE))
                {
                    RiskFactor _riskFactorForDelete = (RiskFactor)dgRiskFactor.SelectedItem;
                    _riskFactorController.Delete(_record, _riskFactorForDelete);
                    dgRiskFactor.Items.Refresh();
                }
            }
            else
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
        }

        private void btnAddAllergy_Click(object sender, RoutedEventArgs e)
        {
            RiskAllergies _newRiskAllergy;
            AllergyAdd inputDialog = new AllergyAdd();
            if (inputDialog.ShowDialog() == true)
            {
                _newRiskAllergy = inputDialog.NewRiskAllergy;
                _riskAllergiesController.Create(_record, _newRiskAllergy);
                dgRiskAllergies.Items.Refresh();
            }
        }

        private void btnEditAllergy_Click(object sender, RoutedEventArgs e)
        {
            RiskAllergies _riskAllergyForEdit = (RiskAllergies)dgRiskAllergies.SelectedItem;

            if ((RiskAllergies)dgRiskAllergies.SelectedItem == null)
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
            else
            {
                AllergyEdit editDialog = new AllergyEdit(_riskAllergyForEdit);
                if (editDialog.ShowDialog() == true)
                {
                    _riskAllergiesController.Update(_record);
                    dgRiskAllergies.Items.Refresh();
                }
            }
        }

        private void btnDeleteAllergy_Click(object sender, RoutedEventArgs e)
        {
            if (dgRiskAllergies.SelectedItem != null)
            {
                if (ShowYesNoDialog(DELETED_QUESTION_MESSAGE))
                {
                    RiskAllergies _riskAllergyForDelete = (RiskAllergies)dgRiskAllergies.SelectedItem;
                    _riskAllergiesController.Delete(_record, _riskAllergyForDelete);
                    dgRiskAllergies.Items.Refresh();
                }
            }
            else
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
        }

        private void btnAddImmunization_Click(object sender, RoutedEventArgs e)
        {
            Immunization _newImmunization;
            ImmunizationAdd inputDialog = new ImmunizationAdd();
            if (inputDialog.ShowDialog() == true)
            {
                _newImmunization = inputDialog.NewImmunization;
                _immunizationController.Create(_record, _newImmunization);
                dgImmunization.Items.Refresh();
            }
        }

        private void btnEditImmunization_Click(object sender, RoutedEventArgs e)
        {
            Immunization _immunizationForEdit = (Immunization)dgImmunization.SelectedItem;

            if ((Immunization)dgImmunization.SelectedItem == null)
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
            else
            {
                ImmunizationEdit editDialog = new ImmunizationEdit(_immunizationForEdit);
                if (editDialog.ShowDialog() == true)
                {
                    _immunizationController.Update(_record);
                    dgImmunization.Items.Refresh();
                }
            }
        }

        private void btnDeleteImmunization_Click(object sender, RoutedEventArgs e)
        {
            if (dgImmunization.SelectedItem != null)
            {
                if (ShowYesNoDialog(DELETED_QUESTION_MESSAGE))
                {
                    Immunization _immunizationForDelete = (Immunization)dgImmunization.SelectedItem;
                    _immunizationController.Delete(_record, _immunizationForDelete);
                    dgImmunization.Items.Refresh();
                }
            }
            else
            {
                ShowError(SELECTED_ERROR_MESSAGE);
            }
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }


        private void ShowError(string message) => new MessageDialog(message, this).ShowDialog();

        private bool ShowYesNoDialog(string YesNoquestion)
        {
            return new SubmitCancelDialog(YesNoquestion, this).ShowDialog() ?? true;
        }


        private void gbMedicalHistory_PreviewKeyDown(object sender, KeyEventArgs e)
        {

                if (e.Key == Key.A && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
                {
                    btnAddMedicalHistory_Click(sender, e);
                }

                if (e.Key == Key.E && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
                {
                    btnEditMedicalHistory_Click(sender, e);
                }

                if (e.Key == Key.D && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
                {
                    btnDeleteMedicalHistory_Click(sender, e);
                }
                if (e.Key == Key.R && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
                {
                    if (dgFamilyMedicalHistory.Items.Count != 0)
                    {


                    dgFamilyMedicalHistory.SelectedItem=dgFamilyMedicalHistory.Items[0];

                    dgFamilyMedicalHistory.Focus();
                    //dgFamilyMedicalHistory.Items.Refresh();
                }

            }

        }

        private void dgFamilyMedicalHistory_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnAddFMedicalHistory_Click(sender, e);
            }

            if (e.Key == Key.E && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnEditFMedicalHistory_Click(sender, e);
            }

            if (e.Key == Key.D && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnDeleteFMedicalHistory_Click(sender, e);
            }
            if (e.Key == Key.R && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                dgMedicalHistory.SelectedIndex = 0;
                dgMedicalHistory.SelectedItem = dgMedicalHistory.Items[0];
                dgMedicalHistory.Focus();
            }
        }

        private void dgTreatment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Treatment _selectedTreatment= (Treatment)dgTreatment.SelectedValue;
            tbAnamneza.Text = _selectedTreatment.TreatmentReport.Anamnesis;
            tbDijagnoza.Text = _selectedTreatment.TreatmentReport.Diagnosis;
            tbNapomenaZaPregled.Text = _selectedTreatment.TreatmentReport.Note;
            tbTerapija.Text = _selectedTreatment.TreatmentReport.Theraphy;
            lblDateTime.Content = _selectedTreatment.Date.ToString("dd MMM yyyy - HH:mm");
            lblTreatmentType.Content = _selectedTreatment.TreatmentType;
            lblTreatmentNumber.Content = _selectedTreatment.Id;

        }

        private void TabItem_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.E && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnEditPatientInformation_Click(sender, e);
            }
        }

        private void TabItem_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnCreateNewTreatment_Click(sender, e);
            }
            if (e.Key == Key.P && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnReviewTreatments_Click(sender, e);
            }
        }

        private void dgRiskFactor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnAddRiskFactor_Click(sender, e);
            }

            if (e.Key == Key.E && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnEditRiskFactor_Click(sender, e);
            }

            if (e.Key == Key.D && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnDeleteRiskFactor_Click(sender, e);
            }
        }

        private void dgRiskAllergies_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnAddAllergy_Click(sender, e);
            }

            if (e.Key == Key.E && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnEditAllergy_Click(sender, e);
            }

            if (e.Key == Key.D && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnDeleteAllergy_Click(sender, e);
            }
        }

        private void dgImmunization_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnAddImmunization_Click(sender, e);
            }

            if (e.Key == Key.E && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnEditImmunization_Click(sender, e);
            }

            if (e.Key == Key.D && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnDeleteImmunization_Click(sender, e);
            }
        }
    }
    
}
