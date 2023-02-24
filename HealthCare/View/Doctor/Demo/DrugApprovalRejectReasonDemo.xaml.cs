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
    /// Interaction logic for DrugApprovalRejectReasonDemo.xaml
    /// </summary>
    public partial class DrugApprovalRejectReasonDemo : Window
    {
        private Drug _drugForApproval;
        CancellationTokenSource source = new CancellationTokenSource();
        /////////////////////////////////////////////////////// uklanjanjeX
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        ///////////////////////////////////////////////////////
        public DrugApprovalRejectReasonDemo(Drug item)
        {
            InitializeComponent();
            _drugForApproval = item;

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            _ = StartDemo(this, new EventArgs());

        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DrugApprovalDemo.demoOn = false;
                this.DialogResult = true;
                this.Close();
            }
        }

        private async Task StartDemo(object sender, EventArgs e)
        {
            lblText.Content = "Unosimo razlog odbijanja leka";

            await Task.Delay(3000);
            // Pretpostavicemo da je pogresno napisan naziv leka
            // 
            string razlog = "Sastojci nisu uneti";
            tbRejectDrugReason.Focus();

            foreach (char karakter in razlog)
            {
                tbRejectDrugReason.Text += karakter;
                this.tbRejectDrugReason.SelectionStart = tbRejectDrugReason.Text.Length;
                tbRejectDrugReason.Focus();
                await Task.Delay(200);
            }
            await Task.Delay(1000);
            lblText.Content = "Potvrdjujemo unos na 'Potvrdi'";
            await Task.Delay(3000);

            btnRejectReasonDrug.Focus();
            btnRejectReasonDrug.Background = Brushes.LightBlue;
            await Task.Delay(200);
            if(source.IsCancellationRequested)
            {
                return;
            }
            btnRejectDrugApproved_Click();
            DrugApprovalDemo._drugsForApproval.Remove(_drugForApproval);


        }

        private void btnRejectDrugApproved_Click()
        {
            this.DialogResult = true;
            this.Close();
        }




        private void btnPrekiniDemo_Click(object sender, RoutedEventArgs e)
        {
            DrugApprovalDemo.demoOn = false;
            this.DialogResult = true;
            this.Close();
        }

        private void btnRejectReasonDrug_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
