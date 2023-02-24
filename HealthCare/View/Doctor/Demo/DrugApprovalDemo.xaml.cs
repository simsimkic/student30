using Controller.DrugController;
using HealthCareClinic.View;
using Model.Drug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
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
using System.Windows.Threading;

namespace HealthCare.View.Doctor.Demo
{
    /// <summary>
    /// Interaction logic for DrugApprovalDemo.xaml
    /// </summary>
    public partial class DrugApprovalDemo : Window
    {
        public static List<Drug> _drugsForApproval = new List<Drug>();
        public static Boolean demoOn = false;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        CancellationTokenSource source = new CancellationTokenSource();
        /////////////////////////////////////////////////////// uklanjanjeX
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        ///////////////////////////////////////////////////////

        public DrugApprovalDemo()
        {

            InitializeComponent();
            this.DataContext = this;
            _drugsForApproval.Clear();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);


            List<Allergen> alergeni = new List<Allergen>();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            alergeni.Add(new Allergen() { Name = "Erytromycin" });

            List<Ingredient> sastojci = new List<Ingredient>();
            sastojci.Add(new Ingredient() { Name = "Acetilsalicilna kiselina" });
            sastojci.Add(new Ingredient() { Name = "Baktrim" });
            //staviti samo get za odobravanje!
            Drug d1 = new Drug() { Id=0, ingredients=sastojci, allergens=alergeni, Format = Formatdrug.Capsule, Instruction = "Uzimati 2x 8h pre obroka", Quantity = 500, Manufacturer = "Galenika", Name = "Defrijol", SideEffect = "Upozoranje:\n Alergije", Status = DrugStatus.Waiting };
            Drug d2 = new Drug() { Id = 1, ingredients = sastojci, allergens = alergeni, Format = Formatdrug.Capsule, Instruction = "Uzimati 1x na 12h ", Quantity = 1000, Manufacturer = "Hemofarm", Name = "Dovicin", SideEffect = "Upozoranje:\n Alergije", Status = DrugStatus.Waiting };
            Drug d3 = new Drug() {Id=2, ingredients = new List<Ingredient>(), allergens = alergeni, Format = Formatdrug.Capsule, Instruction = "1x6h ", Quantity = 500, Manufacturer = "Galenika", Name = "Brufen", SideEffect = "Upozoranje:\n Alergije", Status = DrugStatus.Waiting };

            _drugsForApproval.Add(d1);
            _drugsForApproval.Add(d2);
            _drugsForApproval.Add(d3);
            demoOn = true;
            dispatcherTimer.Start();
            _ = StartDemo(this,new EventArgs());

        }


        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DrugApprovalDemo.demoOn = false;
                this.Close();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!demoOn)
            {
                source.Cancel();
                this.Close();
            }
        }

   

        private async Task StartDemo(object sender, EventArgs e  )
        {

            lblText.Content = "Dobrodosli u DEMO mode, za nekoliko sekundi ce krenuti objasnjenje";
            await Task.Delay(3000);
            lblText.Content = "U svakom trenutku mozete prekinuti demo mod klikom na dugme 'Prekini Demo'";
            await Task.Delay(4000);
            lblText.Content = "ili pritiskom ESC dugmeta na tastaturi";
            await Task.Delay(2000);
            lblText.Content = "Klikom na tab selektovacete prvi lek";
            await Task.Delay(2000);
            listBoxDrugForApprov.SelectedIndex = 0;
            lblText.Content = "Nakon toga upotrebom UP i DOWN dugmeta mozete izabrati zeljeni lek";
            await Task.Delay(3000);
            listBoxDrugForApprov.SelectedIndex = 1;
            await Task.Delay(2000);
            listBoxDrugForApprov.SelectedIndex = 0;
            await Task.Delay(2000);
            lblText.Content = "Nakon sto izaberemo lek mozemo procitati informacije sa desne strane";
            await Task.Delay(5000);
            lblText.Content = "Za prvi lek mozemo zakljuciti da je uneto pogresno ime";
            await Task.Delay(3000);
            lblText.Content = "Klikom na enter ili space otvorice nam se prozor koji ce nam ponuditi sledece korake";
            await Task.Delay(5000);
            if (source.IsCancellationRequested)
            {
                return;
            }
            listBoxDrugForApprov_PreviewKeyDown();
            listBoxDrugForApprov.SelectedIndex = 0;
            lblText.Content = "Lek smo uspesno izmenili i odobrili";
            await Task.Delay(3000);
            lblText.Content = "Izabracemo sledeci lek";
            await Task.Delay(4000);
            listBoxDrugForApprov.SelectedIndex = 1;
            lblText.Content = "Za dati lek vidimo da nedostaju sastojci";
            await Task.Delay(4000);
            lblText.Content = "Pritiskanjem tastera enter ili space cemo pristupiti sledecim koracima";
            await Task.Delay(3000);
            if (source.IsCancellationRequested)
            {
                return;
            }
            listBoxDrugForApprov_PreviewKeyDown2();
            lblText.Content = "Izabracemo sledeci lek"; 
            await Task.Delay(4000);
            listBoxDrugForApprov.SelectedIndex = 0;
            lblText.Content = "Mozemo primetiti da je lek korektno unet";
            await Task.Delay(4000);
            lblText.Content = "Pritiskanjem tastera enter ili space cemo pristupiti sledecim koracima";
            await Task.Delay(3000);
            if (source.IsCancellationRequested)
            {
                return;
            }
            listBoxDrugForApprov_PreviewKeyDown3();

            lblText.Content = "Demo je zavrsen, ukoliko ne prekinete ponovo ce se pokrenuti";
            await Task.Delay(3000);

            DrugApprovalDemo demo = new DrugApprovalDemo();
            this.Close();
            demo.ShowDialog();

        }

        private void listBoxDrugForApprov_PreviewKeyDown()
        {
            Drug _selectedDrug = (Drug)listBoxDrugForApprov.SelectedValue;

            DrugApprovalEnterOnDrugDemo dialog = new DrugApprovalEnterOnDrugDemo(_selectedDrug);
            if (dialog.ShowDialog() == true)
            {
                listBoxDrugForApprov.Items.Refresh();
            }
        }

        private void listBoxDrugForApprov_PreviewKeyDown2()
        {
            Drug _selectedDrug = (Drug)listBoxDrugForApprov.SelectedValue;

            DrugApprovalEnterOnDrugDemo2 dialog = new DrugApprovalEnterOnDrugDemo2(_selectedDrug);
            if (dialog.ShowDialog() == true)
            {
                listBoxDrugForApprov.Items.Refresh();
            }
        }

        private void listBoxDrugForApprov_PreviewKeyDown3()
        {
            Drug _selectedDrug = (Drug)listBoxDrugForApprov.SelectedValue;

            DrugApprovalEnterOnDrugDemo3 dialog = new DrugApprovalEnterOnDrugDemo3(_selectedDrug);
            if (dialog.ShowDialog() == true)
            {
                listBoxDrugForApprov.Items.Refresh();
            }
        }

        public List<Drug> DrugsForApproval
        {
            get { return _drugsForApproval; }
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


        private void btnPrekiniDemo_Click(object sender, RoutedEventArgs e)
        {
            demoOn = false;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
