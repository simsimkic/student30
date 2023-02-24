using HealthCare.Model;
using HealthCareClinic.View.Doctor;
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
    /// <summary>
    /// Interaction logic for DrugApprovalEdit.xaml
    /// </summary>
    public partial class DrugApprovalEdit : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";

        private Drug _drugForEditAndApproved;
        public DrugApprovalEdit(Drug _drugForEdit)
        {
            InitializeComponent();
            this.DataContext = this;
            _drugForEditAndApproved = _drugForEdit;

            tbDrugIDValue.IsEnabled = false;

            tbDrugNameValue.Text = _drugForEditAndApproved.Name;
            tbDrugIDValue.Text = _drugForEditAndApproved.Id.ToString();
            tbProducerValue.Text = _drugForEditAndApproved.Manufacturer;

            cbDrugFormatValue.SelectedItem = _drugForEditAndApproved.Format;
            tbDrugSizeValue.Text = _drugForEditAndApproved.Quantity.ToString() ;

            string allergens = "";
            foreach (Allergen item in _drugForEditAndApproved.allergens)
            {
                allergens += item.Name + " \n";
            }
            tbDrugAllergies.Text = allergens;

            tbDrugDescription.Text = _drugForEditAndApproved.Instruction;
            tbDrugDescription.Text += "\n\nSastojci: \n";
            foreach (Ingredient item in _drugForEditAndApproved.ingredients)
            {
                tbDrugDescription.Text += item.Name + " \n";
            }

            tbDrugSideEffects.Text = _drugForEditAndApproved.SideEffect;

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

        }

        private void btnEditedDrugApproved_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                _drugForEditAndApproved.Name = tbDrugNameValue.Text;
                _drugForEditAndApproved.Manufacturer = tbProducerValue.Text;
                _drugForEditAndApproved.Quantity = Double.Parse(tbDrugSizeValue.Text);
                _drugForEditAndApproved.Format = (Formatdrug)cbDrugFormatValue.SelectedValue;
                _drugForEditAndApproved.Status = DrugStatus.Approved;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                ShowError(INPUT_ERROR_MESSAGE);
            }
        }
        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();


        private bool isValidInputData()
        {
            if (tbDrugNameValue.Text != "" && tbProducerValue.Text!="" && tbDrugSizeValue.Text != "" && tbDrugSideEffects.Text!="" && cbDrugFormatValue.SelectedValue!=null)
            {
                return true;
            }
            return false;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
