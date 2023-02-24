using Controller.CommunicationControllers;
using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.Communication;
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

namespace HealthCareClinic.HCI
{
    /// <summary>
    /// Interaction logic for SoftwareRating.xaml
    /// </summary>
    public partial class SoftwareRating : Window
    {
        private const string INPUT_ERROR_DESIGN = "Morate uneti vrstu odsustva";
        private const string INPUT_ERROR_FUNCIONALITY = "Morate uneti datum od kojeg zelite odsustvo";
        private const string INPUT_ERROR_SPEED = "Morate uneti datum do kojeg zelite odsustvo";

        private readonly ISoftwareRatingController _softwareRatingController;
        public SoftwareRating()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.cbDesign.KeyDown += new KeyEventHandler(HandleCB);
            this.cbFuncionality.KeyDown += new KeyEventHandler(HandleCB);
            this.cbSpeed.KeyDown += new KeyEventHandler(HandleCB);


            _softwareRatingController = (Application.Current as App).softwareRatingController;
        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {

                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }

        private void btnSendSoftwareRating_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInput())
            {
                _softwareRatingController.Create(new Model.Communication.SoftwareRating() { Note=(tbNote.Text==null)?tbNote.Text:"", Functionality = (Grades)cbFuncionality.SelectedItem, Speed= (Grades)cbSpeed.SelectedItem, Reliabillity=(Grades)cbDesign.SelectedItem });
          
                this.Close();
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private bool isValidInput()
        {
            if (cbDesign.SelectedItem == null)
            {
                ShowError(INPUT_ERROR_DESIGN);
                return false;
            }
            else if (cbFuncionality.SelectedItem == null)
            {
                ShowError(INPUT_ERROR_FUNCIONALITY);
                return false;
            }
            else if (cbSpeed.SelectedItem == null)
            {
                ShowError(INPUT_ERROR_SPEED);
                return false;
            }
            return true;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

    }
}
