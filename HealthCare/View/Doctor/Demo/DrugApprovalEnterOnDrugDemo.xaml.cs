using HealthCareClinic.View;
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
    /// Interaction logic for DrugApprovalEnterOnDrugDemo.xaml
    /// </summary>
    public partial class DrugApprovalEnterOnDrugDemo : Window
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

        public DrugApprovalEnterOnDrugDemo(Drug drugForAprroval)
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            _drugForApproval = drugForAprroval;
            btnDrugAprrovalEdit.IsEnabled = false;
            btnDrugAprrovalReject.IsEnabled = false;
            btnDrugAprrovalSubmit.IsEnabled = false;
            _ = StartDemo(this, new EventArgs());

        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DrugApprovalDemo.demoOn = false;
                this.Close();
            }
        }

        private async Task StartDemo(object sender, EventArgs e)
        {
            lblText.Content = "U ovom trenutku mozemo izabrati sledeci korak";
            await Task.Delay(3000);
            lblText.Content = "Izabracemo izmeni jer je ime pogresno uneto";
            await Task.Delay(4000);

            btnDrugAprrovalEdit.Focus();
            btnDrugAprrovalEdit.Background = Brushes.LightBlue;
            await Task.Delay(200);
            if(source.IsCancellationRequested)
            {
                return;
            }
            btnDrugAprrovalEdit_Click(sender, e);

        }

        private void btnDrugAprrovalEdit_Click(object sender, EventArgs e)
        {
            DrugApprovalEditDemo window = new DrugApprovalEditDemo(_drugForApproval);
            if (window.ShowDialog() == true)
            {
                this.DialogResult = true;
                this.Close();
            }
        }


        private void btnPrekiniDemo_Click(object sender, RoutedEventArgs e)
        {
            DrugApprovalDemo.demoOn = false;
            source.Cancel();
            this.Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
