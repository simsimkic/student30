using Controller.DrugController;
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
    /// Interaction logic for ImmunizationEdit.xaml
    /// </summary>
    public partial class ImmunizationEdit : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";
        private IList<Drug> _vaccine;
        private readonly IDrugController _drugController;

        private Immunization _immunizationForEdit;
        public ImmunizationEdit(Immunization immunizationForEdit)
        {
            InitializeComponent();
            this.DataContext = this;
            _immunizationForEdit = immunizationForEdit;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbEditGiveType.KeyDown += new KeyEventHandler(HandleCB);
            this.cbEditVaccine.KeyDown += new KeyEventHandler(HandleCB);
            this.cbEditVaccineType.KeyDown += new KeyEventHandler(HandleCB);
            this.dPickerGiveVaccine.PreviewKeyDown += new KeyEventHandler(HandleDP);

            this.cbEditGiveType.SelectedItem = _immunizationForEdit.GivingType;
            this.cbEditVaccineType.SelectedItem = _immunizationForEdit.VacineType;
            this.dPickerGiveVaccine.SelectedDate = _immunizationForEdit.Date;

            _drugController = (Application.Current as App).drugControler;

            List<Drug> lista = _drugController.GetApprovedVaccine().ToList();

            _vaccine = lista;

            int i = 0;
            foreach (Drug item in lista)
            {
                if (item.Name.Equals(_immunizationForEdit.Vaccine.Name))
                    break;
                i++;
            }

            cbEditVaccine.SelectedIndex = i;
        }

        public IList<Drug> Vaccine
        {
            get { return _vaccine; }
            set
            {
                _vaccine = value;
            }
        }

        private void HandleDP(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                this.dPickerGiveVaccine.IsDropDownOpen = true;
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
        private void btnEditImmunization_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                GivingType givingType = (GivingType)cbEditGiveType.SelectedValue;
                VaccineType type = (VaccineType)cbEditVaccineType.SelectedValue;
                DateTime date = (DateTime)dPickerGiveVaccine.SelectedDate;
                Drug vaccine = (Drug)cbEditVaccine.SelectedValue;

                _immunizationForEdit.Date = date;
                _immunizationForEdit.GivingType = givingType;
                _immunizationForEdit.VacineType = type;
                _immunizationForEdit.Vaccine = vaccine;

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
            if (cbEditGiveType.SelectedItem != null && cbEditVaccine.SelectedItem != null && cbEditVaccineType.SelectedItem != null && dPickerGiveVaccine.SelectedDate != null)
            {
                return true;
            }
            return false;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();
    }
}
