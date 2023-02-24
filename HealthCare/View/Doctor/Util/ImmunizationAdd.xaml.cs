using Controller.DrugController;
using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.DataFiltration;
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
    /// Interaction logic for ImmunizationAdd.xaml
    /// </summary>
    public partial class ImmunizationAdd : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";
        private IList<Drug> _vaccine;
        private readonly IDrugController _drugController;

        private Immunization _newImmunization;

        public ImmunizationAdd()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbAddGiveType.KeyDown += new KeyEventHandler(HandleCB);
            this.cbAddVaccine.KeyDown += new KeyEventHandler(HandleCB);
            this.cbAddVaccineType.KeyDown += new KeyEventHandler(HandleCB);
            this.dPickerGiveVaccine.PreviewKeyDown += new KeyEventHandler(HandleDP);

            _drugController = (Application.Current as App).drugControler;

            List<Drug> lista = _drugController.GetApprovedVaccine().ToList();
            _vaccine = lista;
        }

        public IList<Drug> Vaccine
        {
            get { return _vaccine; }
            set
            {
                _vaccine = value;
            }
        }

        public Immunization NewImmunization
        {
            get { return _newImmunization; }
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

        private void btnAddImmunization_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                DateTime date = (DateTime)dPickerGiveVaccine.SelectedDate;
                Drug vaccine = (Drug)cbAddVaccine.SelectedValue;
                VaccineType type = (VaccineType)cbAddVaccineType.SelectedValue;
                //GivingType givingType = (GivingType)cbAddGiveType.SelectedValue;

                _newImmunization = new Immunization { Date = date, Vaccine = vaccine, VacineType = type };
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
            if (cbAddGiveType.SelectedItem != null && cbAddVaccine.SelectedItem != null  && cbAddVaccineType.SelectedItem != null && dPickerGiveVaccine.SelectedDate != null)
            {
                return true;
            }
            return false;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

    }
}
