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
    /// Interaction logic for DrugApprovalRejectReason.xaml
    /// </summary>
    public partial class DrugApprovalRejectReason : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Razlog mora biti naveden";

        private Drug _drugForApproval;
        public DrugApprovalRejectReason(Drug drugForApproval)
        {
            InitializeComponent();
            this.DataContext = this;
            _drugForApproval = drugForApproval;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void btnRejectReasonDrug_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                _drugForApproval.RejectReason = tbRejectDrugReason.Text;
                _drugForApproval.Status = DrugStatus.Rejected;
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
            if (tbRejectDrugReason.Text != "" && tbRejectDrugReason.Text.Length>=3)
            {
                return true;
            }
            return false;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
