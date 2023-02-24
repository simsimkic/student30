using HealthCareClinic.View.Doctor;
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
    /// Interaction logic for InstructionForLaboratory.xaml
    /// </summary>
    /// 

    public partial class InstructionForLaboratory : Window
    {
        private const string INPUT_ERROR_MESSAGE = "Nisu popunjena sva polja";

        private LaboratoryInstruction _refferal;

        private bool confirm = false;
        public InstructionForLaboratory()
        {
            InitializeComponent();
            this.DataContext = this;

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

        }

        
        public LaboratoryInstruction NewRefferal
        {
            get { return _refferal; }
        }

        private void btnSendLaboratoryInstruction_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                LaboratoryInstruction refferal =new LaboratoryInstruction();
                refferal.Id = 1;
                refferal.Note = tbReason.Text;
                _refferal = refferal;
                this.DialogResult = true;
                this.confirm = true;
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
            if (tbReason.Text != "")
            {
                return true;
            }
            return false;
        }

        public bool isConfirmed()
        {
            return confirm;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
