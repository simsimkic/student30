using Controller.AppointmenController;
using Controller.DiagnosisController;
using Controller.DrugController;
using Controller.MedicalRecordControllers;
using HealthCare;
using HealthCare.View.Doctor.Util;
using HealthCareClinic.HCI;
using HealthCareClinic.View;
using HealthCareClinic.View.Doctor;
using Model.Appointment;
using Model.DataReport;
using Model.Diagnosis;
using Model.Drug;
using Model.MedicalRecords;
using Model.Users;
using Repository.UserRepositories;
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

namespace HealthCareClinic.HCI
{
    /// <summary>
    /// Interaction logic for NewTreatment.xaml
    /// </summary>
    public partial class NewTreatment : Window
    {
        private const string INPUT_ERROR_ANAMNEZA = "Potrebno je uneti anamnezu";
        private const string INPUT_ERROR_DIJAGNOZA = "Potrebno je uneti dijagnozu";

        private const string INPUT_ERROR_TERAPIJA = "Potrebno je uneti terapiju";

        private const string INPUT_ERROR_NAPOMENA = "Potrebno je uneti napomenu";

        private readonly IDiagnosisController _diagnosisController;
        private readonly IDrugController _drugController;
        private readonly IRefferalController _refferalController;
        private readonly IRecordController _recordController;
        private readonly IAppointmentController _appointmentController;

        private Treatment _newTreatment;

        private IList<Diagnosis> _diagnosis;
        private IList<Drug> _drugs;
        private List<Refferal> _addedRefferals = new List<Refferal>();
        private List<Appointment> _appointments = new List<Appointment>();
        private List<Drug> _addedDrugForPrescription = new List<Drug>();

        private MedicalRecord _record;
        private Patient _patient;

        public NewTreatment(MedicalRecord record,Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            _record = record;
            _patient = patient;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbAddTheraphy.KeyDown += new KeyEventHandler(HandleCB);
            this.cbDiagnosisList.KeyDown += new KeyEventHandler(HandleCB);

            var app = Application.Current as App;

            _diagnosisController = app.diagnosisController;
            _drugController = app.drugControler;
            _refferalController = app.refferalControler;
            _recordController = app.medicalRecordController;
            _diagnosis = _diagnosisController.GetAll().ToList();
            _drugs = _drugController.GetAll().ToList();
            _appointmentController = app.appointmentController;
        }

        public IList<Diagnosis> Diagnosis
        {
            get { return _diagnosis; }
            set
            {
                _diagnosis = value;
            }
        }

        public IList<Drug> Drugs
        {
            get { return _drugs; }
            set
            {
                _drugs = value;
            }
        }

        public Treatment newTreatment
        {
            get { return _newTreatment; }
        }

        private List<Refferal> AddedRefferals
        {
            get { return _addedRefferals; }
            set
            {
                _addedRefferals = value;
            }
        }

        private List<Drug> AddedDrugForPrescription
        {
            get { return _addedDrugForPrescription; }
            set
            {
                _addedDrugForPrescription = value;
            }
        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {

                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }

        private void printPatientTreatment_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                PatientTreatmentReport ptr = new PatientTreatmentReport();
                ptr.DateTo = DateTime.Now;
                ptr.DateFrom = DateTime.Now;
                

                string anamnesis = tbAnamnesis.Text;
                string diagnosis = tbDiagnosis.Text;
                string theraphy = tbTheraphy.Text;
                string note = tbNote.Text;

                DateTime dateFrom = DateTime.Now;
                DateTime dateTo = DateTime.Now.AddDays(30);

                List<Drug> theraphyDrug = new List<Drug>();
                foreach(Drug item in _addedDrugForPrescription)
                {
                    theraphyDrug.Add(new Drug() { Id=item.Id});
                }
                Prescription prescription = new Prescription { Reserved = false, ReservedFrom = dateFrom, ReservedTo = dateTo, Theraphy = theraphyDrug };
                
                Model.MedicalRecords.TreatmentReport newTreatmentReport = new Model.MedicalRecords.TreatmentReport { Anamnesis = anamnesis, Diagnosis = diagnosis, Note = note, Theraphy = theraphy, Refferal = _addedRefferals, Prescription = prescription };

                DateTime treatmentDate = DateTime.Now;
                var app = Application.Current as App;
                var doctor = app._currentUser;

                string creator = doctor.Name + " " + doctor.Surname;
                _newTreatment = new Treatment { Date = treatmentDate, TreatmentReport = newTreatmentReport, Creator = creator  };

                List<Treatment> lista = new List<Treatment>();
                lista.Add(_newTreatment);

                ptr.Id = _newTreatment.Id;
                ptr.treatment = lista;
                ReviewPDFGenerator.ExportAsPDF(ptr);
            }

        }

        private void savePatientTreatment_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                string anamnesis = tbAnamnesis.Text;
                string diagnosis = tbDiagnosis.Text;
                string theraphy = tbTheraphy.Text;
                string note = tbNote.Text;

                DateTime dateFrom = DateTime.Now;
                DateTime dateTo = DateTime.Now.AddDays(30);

                List<Drug> drugsForPrescription = new List<Drug>();

                foreach(Drug item in _addedDrugForPrescription)
                {
                    drugsForPrescription.Add(new Drug() { Id = item.Id });
                }
                
                Prescription prescription = new Prescription { Reserved = false, ReservedFrom = dateFrom, ReservedTo = dateTo ,Theraphy= drugsForPrescription };
                   
                Model.MedicalRecords.TreatmentReport newTreatmentReport = new Model.MedicalRecords.TreatmentReport { PatientId=_patient.Id, Anamnesis = anamnesis, Diagnosis = diagnosis, Note = note, Theraphy = theraphy, Refferal = _addedRefferals, Prescription = prescription };

                DateTime treatmentDate = DateTime.Now;
                var app = Application.Current as App;
                var doctor = app._currentUser;

                string creator = doctor.Name+" "+doctor.Surname;
                _newTreatment = new Treatment { Date = treatmentDate, TreatmentReport = newTreatmentReport, Creator = creator, TreatmentType=TreatmentType.Examination };
                
                foreach(Refferal item in _addedRefferals)
                {
                    _refferalController.Create(item);
                }

                foreach(Appointment item in _appointments)
                {
                    _appointmentController.Create(item);
                }

                _drugController.ReduceAmountByPrescription(prescription);

                this.DialogResult = true;
                this.Close();
            }

        }

        private void btnShowPotentialDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            PotentialDiagnosis dialog = new PotentialDiagnosis();
            if(dialog.ShowDialog() == true)
            {
                string message = "Zelite da generisete izvestaj za dijagnozu: ";
                Diagnosis diagnosis = dialog.SelectedDiagnosis();
                message += diagnosis.Name;

                if (!proveriAlergene(diagnosis))
                {
                    if (ShowYesNoDialog(message))
                    {
                        _addedDrugForPrescription = new List<Drug>();
                        tbDiagnosis.Text = diagnosis.Code + " " + diagnosis.Name;
                        tbTheraphy.Text = diagnosis.Theraphy;
                        _addedDrugForPrescription.AddRange(diagnosis.TheraphyDrug);
                    }
                }
                else
                {
                    message = "Pacijent je alergican na jedan od sastojaka terapije predvidjene za ovu dijagnozu";
                    message += "\n Unece se dijagnoza bez terapije";
                    if (ShowYesNoDialog(message))
                    {
                        tbDiagnosis.Text = diagnosis.Code + " " + diagnosis.Name;
                    }
                }
            }

        }

        private bool proveriAlergene(Diagnosis _diagnosis)
        {
            return _recordController.IsPatientAllergic(_record, _diagnosis.TheraphyDrug);
        }

        private bool isValidInputData()
        {
            
            if (tbAnamnesis.Text.Length<5)
            {
                ShowError(INPUT_ERROR_ANAMNEZA);
                return false;
            }
            if (tbDiagnosis.Text.Length <5)
            {
                ShowError(INPUT_ERROR_DIJAGNOZA);
                return false;
            }
            if (tbTheraphy.Text.Length < 5)
            {
                ShowError(INPUT_ERROR_TERAPIJA);
                return false;
            }
            if (tbNote.Text.Length < 5)
            {
                ShowError(INPUT_ERROR_NAPOMENA);
                return false;
            }

            return true;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();


        private void cbSpecialistInstruction_Checked(object sender, RoutedEventArgs e)
        {
            var dialog = new InstructionForSpecialist(_patient);
            if (dialog.ShowDialog() == false)
            {
                if (!dialog.isConfirmed())
                {
                    cbSpecialistInstruction.IsChecked = false;
                }
            }
            else
            {
                _appointments.Add(dialog.NewAppointment);
            }
        }

        private void cbHospitalInstruction_Checked(object sender, RoutedEventArgs e)
        {
            var dialog = new InstructionForHospital(_patient);
            if (dialog.ShowDialog() == false)
            {
                if (!dialog.isConfirmed())
                {
                    cbHospitalInstruction.IsChecked = false;
                }
            }
            else
            {
                _addedRefferals.Add(dialog.NewRefferal);
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void cbLaboratoryInstruction_Checked(object sender, RoutedEventArgs e)
        {
             var dialog = new InstructionForLaboratory();
             if (dialog.ShowDialog() == false)
             {
                 if (!dialog.isConfirmed())
                 {
                    cbLaboratoryInstruction.IsChecked = false;
                 }
            }
            else
            {
                _addedRefferals.Add(dialog.NewRefferal);
            }
        }

        private void cbSurgeryInstruction_Checked(object sender, RoutedEventArgs e)
        {
            var dialog = new InstructionForOperation(_patient);
            if (dialog.ShowDialog() == false)
            {
                if (!dialog.isConfirmed())
                {
                    cbSurgeryInstruction.IsChecked = false;
                }
            }
            else
            {
                if (dialog.Status.Equals("refferal"))
                {
                    _addedRefferals.Add(dialog.NewRefferal);
                }else if (dialog.Status.Equals("appointment"))
                {
                    _appointments.Add(dialog.NewAppointment);
                }
            }
        }

        private void cbControlInstruction_Checked(object sender, RoutedEventArgs e)
        {
            var dialog = new InstructionForControl(_patient);
            if (dialog.ShowDialog() == false)
            {
                if (!dialog.isConfirmed())
                {
                    cbControlInstruction.IsChecked = false;
                }
            }
            else
            {
                _appointments.Add(dialog.NewAppointment);
            }
        }



        private bool ShowYesNoDialog(string YesNoquestion)
        {
            return new SubmitCancelDialog(YesNoquestion, this).ShowDialog() ?? true;
        }

        private void cbDiagnosisList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string message = "Zelite da generisete izvestaj za dijagnozu: ";
            Diagnosis diagnosis = (Diagnosis)cbDiagnosisList.SelectedValue;
            message += diagnosis.Name;

            if (!proveriAlergene(diagnosis))
            {
                if (ShowYesNoDialog(message))
                {
                    _addedDrugForPrescription = new List<Drug>();
                    tbDiagnosis.Text = diagnosis.Code + " " + diagnosis.Name;
                    tbTheraphy.Text = diagnosis.Theraphy;
                    _addedDrugForPrescription.AddRange(diagnosis.TheraphyDrug);
                }
            }
            else
            {
                message = "Pacijent je alergican na jedan od sastojaka terapije predvidjene za ovu dijagnozu";
                message += "\n Unece se dijagnoza bez terapije";
                if (ShowYesNoDialog(message))
                {
                    tbDiagnosis.Text = diagnosis.Code + " " + diagnosis.Name;
                }
            }
        }

        private void cbTheraphyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Drug theraphy = (Drug)cbAddTheraphy.SelectedValue;
            List<Drug> lista = new List<Drug>();
            lista.Add(theraphy);
            if (!isAddedTheraphy(theraphy.Id))
            {
                string message = "Zelite da dodate terapiju: ";
                message += theraphy.Name;


                if (!_recordController.IsPatientAllergic(_record, lista))
                {
                    if (ShowYesNoDialog(message))
                    {
                        _addedDrugForPrescription.Add(theraphy);
                        string theraphyString = "";
                        theraphyString = theraphy.Manufacturer + " - " + theraphy.Name + " - " + theraphy.Quantity + "mg  " + "\n";
                        tbTheraphy.Text += theraphyString;
                    }
                }
                else
                {
                    message = "PAZNJA: Pacijent je alergican na jedan od sastojaka leka";
                    message += "\n Da li ste sigurni da zelite da dodate ovaj lek?";

                    if (ShowYesNoDialog(message))
                    {

                        _addedDrugForPrescription.Add(theraphy);
                        string theraphyString = "";
                        theraphyString = theraphy.Manufacturer + " - " + theraphy.Name + " - " + theraphy.Quantity + "mg  " + "\n";
                        tbTheraphy.Text += theraphyString;
                    }
                }
            }
            else
            {
                string message = "Zelite da izbrisete terapiju: ";
                message += theraphy.Name;
                if (ShowYesNoDialog(message))
                {
                    foreach (Drug item in _addedDrugForPrescription)
                    {
                        if (item.Id == theraphy.Id)
                        {
                            _addedDrugForPrescription.Remove(item);
                            break;
                        }
                    }

                    string theraphyString = "";
                    foreach (Drug item in _addedDrugForPrescription)
                    {
                        theraphyString += item.Manufacturer + " - " + item.Name + " - " + item.Quantity + "mg  " + "\n";

                    }
                    tbTheraphy.Text = theraphyString;
                }
            }
        }

        private bool isPatientAllergic(List<Allergen> alergeni)
        {

            return false;
        }



        private bool isAddedTheraphy(int id)
        {
            foreach (Drug item in _addedDrugForPrescription)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
