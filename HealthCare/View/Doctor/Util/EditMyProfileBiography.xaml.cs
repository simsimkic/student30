using HealthCareClinic.View.Doctor;
using Model.Users;
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
    /// Interaction logic for EditMyProfileBiography.xaml
    /// </summary>
    public partial class EditMyProfileBiography : Window
    {
        Doctor _doctorForEdit;
        public EditMyProfileBiography(Doctor doctorForEdit)
        {
            InitializeComponent();
            this.DataContext = this;
            
            _doctorForEdit = doctorForEdit;
            tbBiography.Text = _doctorForEdit.Biography;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void btnSaveEditProfileBiography_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                _doctorForEdit.Biography = tbBiography.Text;

                this.DialogResult = true;
                this.Close();
            }
        }

        private bool isValidInputData()
        {

            return true;

        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();
    }
}
