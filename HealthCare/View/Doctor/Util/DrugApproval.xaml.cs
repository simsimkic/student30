using Controller.DrugController;
using HealthCare;
using HealthCare.Model;
using Model.Drug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for DrugApproval.xaml
    /// </summary>
    public partial class DrugApproval : Window
    {
        private List<Drug> _drugsForApproval = new List<Drug>();
        private readonly IDrugController _drugController;

        public DrugApproval()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            var app = Application.Current as App;

            _drugController = app.drugControler;

            //staviti samo get za odobravanje!
            _drugsForApproval = _drugController.GetDrugByDoctorWhoApprovesIt(app._currentUser).Where(x => x.Status == DrugStatus.Waiting).ToList();
        }
        public List<Drug> DrugsForApproval
        {
            get { return _drugsForApproval; }
            set
            {
                _drugsForApproval = value;
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void listBoxDrugForApprov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Drug _selectedDrug = (Drug)listBoxDrugForApprov.SelectedValue;

            if (_selectedDrug != null)
            {
                lblDrugNameValue.Content = _selectedDrug.Name;
                lblDrugIDValue.Content = _selectedDrug.Id;
                lblProducerValue.Content = _selectedDrug.Manufacturer;

                lblDrugFormatValue.Content = _selectedDrug.Format.ToString();
                lblDrugSizeValue.Content = _selectedDrug.Quantity;
                lblDrugSizeValue.Content += "mg";
                string allergens = "";
                foreach (Allergen item in _selectedDrug.allergens)
                {
                    allergens += item.Name + " \n";
                }
                tbDrugAllergens.Text = allergens;

                tbDrugDescription.Text = _selectedDrug.Instruction;
                tbDrugDescription.Text += "\n\nSastojci: \n";
                foreach (Ingredient item in _selectedDrug.ingredients)
                {
                    tbDrugDescription.Text += item.Name + " \n";
                }
                tbDrugSideEffect.Text = _selectedDrug.SideEffect;
            }
            
        }

        private void listBoxDrugForApprov_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Drug _selectedDrug = (Drug)listBoxDrugForApprov.SelectedValue;

            if (e.Key == Key.Space || e.Key == Key.Return) 
            {

                DrugAprrovalEnterOnDrug dialog = new DrugAprrovalEnterOnDrug(_selectedDrug);
                if (dialog.ShowDialog() == true)
                {
                    if (_selectedDrug.Status == DrugStatus.Approved)
                    {
                        _drugController.DrugAccept(_selectedDrug);
                        listBoxDrugForApprov.ItemsSource = null;
                        listBoxDrugForApprov.ItemsSource = _drugsForApproval = _drugController.GetNonApprovedDrugs().ToList();
                        listBoxDrugForApprov.Items.Refresh();
                    }
                    else
                    {
                        _drugController.DrugReject(_selectedDrug);
                        listBoxDrugForApprov.ItemsSource = null;
                        listBoxDrugForApprov.ItemsSource = _drugsForApproval = _drugController.GetNonApprovedDrugs().ToList();
                        listBoxDrugForApprov.Items.Refresh();
                    }
                }
            }
        }

    }
}
