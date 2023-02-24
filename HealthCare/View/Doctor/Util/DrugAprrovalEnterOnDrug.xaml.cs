using HealthCare.Model;
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
    /// Interaction logic for DrugAprrovalEnterOnDrug.xaml
    /// </summary>
    public partial class DrugAprrovalEnterOnDrug : Window
    {
        private Drug _drugForApproval;
        private bool _accepted;
        public DrugAprrovalEnterOnDrug(Drug drugForAprroval)
        {
            InitializeComponent();
            this.DataContext = this;
            _drugForApproval = drugForAprroval;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

        }

        private void btnDrugAprrovalEdit_Click(object sender, RoutedEventArgs e)
        {
            DrugApprovalEdit window = new DrugApprovalEdit(_drugForApproval);
            if (window.ShowDialog() == true)
            {
                this.DialogResult = true;
                this.Close();
            }
        }

         private void btnDrugAprrovalReject_Click(object sender, RoutedEventArgs e)
        {
            DrugApprovalRejectReason window = new DrugApprovalRejectReason(_drugForApproval);
            if (window.ShowDialog() == true)
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void btnDrugAprrovalSubmit_Click(object sender, RoutedEventArgs e)
        {
            _drugForApproval.Status = DrugStatus.Approved;
            this.DialogResult = true;
            this.Close();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
