using HealthCareClinic.View.Doctor;
using Model.Drug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare.View.Doctor.Demo
{
    /// <summary>
    /// Interaction logic for DrugApprovalEditDemo.xaml
    /// </summary>
    public partial class DrugApprovalEditDemo : Window
    {

        private Drug _drugForEditAndApproved;
        CancellationTokenSource source = new CancellationTokenSource();

        /////////////////////////////////////////////////////// uklanjanjeX
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        ///////////////////////////////////////////////////////
        public DrugApprovalEditDemo(Drug _drugForEdit)
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            btnEditedDrugApproved.IsEnabled = false;
           
            this.DataContext = this;
            _drugForEditAndApproved = _drugForEdit;

            tbDrugIDValue.IsEnabled = false;

            tbDrugNameValue.Text = _drugForEditAndApproved.Name;
            tbDrugIDValue.Text = _drugForEditAndApproved.Id.ToString();
            tbProducerValue.Text = _drugForEditAndApproved.Manufacturer;

            cbDrugFormatValue.SelectedItem = _drugForEditAndApproved.Format;
            tbDrugSizeValue.Text = _drugForEditAndApproved.Quantity.ToString();

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


            _ = StartDemo(this, new EventArgs());

        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DrugApprovalDemo.demoOn = false;
                source.Cancel();
                this.DialogResult = true;
                this.Close();
            }
        }

        private async Task StartDemo(object sender, EventArgs e)
        {
            lblText.Content = "Posto je greska u nazivu leka, promenicemo naziv";

            await Task.Delay(5000);
            // Pretpostavicemo da je pogresno napisan naziv leka
            // 
            string imeLeka = tbDrugNameValue.Text;
            this.tbDrugNameValue.SelectionStart = tbDrugNameValue.Text.Length;
            tbDrugNameValue.Focus();
            for (int i=0; i < 3; i++)
            {
                await Task.Delay(400);
               
                //tbDrugNameValue.SelectionStart = tbDrugNameValue.Text.Length;
               // tbDrugNameValue.SelectionLength = 0;
                imeLeka = imeLeka.Remove(imeLeka.Length - 1, 1);

                tbDrugNameValue.Text = imeLeka;
                this.tbDrugNameValue.SelectionStart = tbDrugNameValue.Text.Length;
                tbDrugNameValue.Focus();
            }

            await Task.Delay(1000);


            string dodatak = "nol";
            foreach(char karakter in dodatak)
            {
                tbDrugNameValue.Text += karakter;
                this.tbDrugNameValue.SelectionStart = tbDrugNameValue.Text.Length;
                tbDrugNameValue.Focus();
                await Task.Delay(200);
            }
            await Task.Delay(1000);
            lblText.Content = "Nakon sto smo promenili naziv pritisnucemo dugme 'Potvrdi'";
            await Task.Delay(5000);

            btnEditedDrugApproved.Focus();
            btnEditedDrugApproved.Background = Brushes.LightBlue;
            await Task.Delay(200);
            if (source.IsCancellationRequested)
            {
                return;
            }
            btnEditedDrugApproved_Click();
            DrugApprovalDemo._drugsForApproval.Remove(_drugForEditAndApproved);


        }

        private void btnEditedDrugApproved_Click()
        {
            _drugForEditAndApproved.Name = tbDrugNameValue.Text;
            this.DialogResult = true;
            this.Close();
        }

        private void btnPrekiniDemo_Click(object sender, RoutedEventArgs e)
        {
            DrugApprovalDemo.demoOn = false;
            source.Cancel();
            this.DialogResult = true;
            this.Close();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
