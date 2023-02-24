using HealthCareClinic.View.Doctor;
using Model.MedicalRecords;
using Model.Users;
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
    /// Interaction logic for editPatientInformation.xaml
    /// </summary>
    public partial class EditPatientInformation : Window
    {
        Patient _patient;
        private MedicalRecord _record;
        public EditPatientInformation(Patient patient, MedicalRecord record)
        {
            InitializeComponent();
            _patient = patient;
            _record = record;
            cbBloodGroup.SelectedItem = _patient.BloodType;
            cbRHFactor.SelectedItem = _patient.RhFactor;
            tbNote.Text = _record.Note;
            
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbBloodGroup.KeyDown += new KeyEventHandler(HandleCB);
            this.cbRHFactor.KeyDown += new KeyEventHandler(HandleCB);
        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {

                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }

        private void savePatientInformation_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInput())
            {
                _patient.BloodType = (Bloodtype)cbBloodGroup.SelectedItem;
                _patient.RhFactor = (Rhfaktor)cbRHFactor.SelectedItem;
                _record.Note = tbNote.Text;

                this.DialogResult = true;
                this.Close();
            }
        }

        private bool isValidInput()
        {
            return true;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();


        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
