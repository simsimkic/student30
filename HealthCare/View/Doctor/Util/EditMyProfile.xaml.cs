using HealthCareClinic.View;
using HealthCareClinic.View.Doctor;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EditMyProfile.xaml
    /// </summary>
    public partial class EditMyProfile : Window
    {
        private const string INPUT_ERROR_CITY = "Naziv grada mora imati minimalno 3 slova";
        private const string INPUT_ERROR_STREET = "Naziv ulice mora imati minimalno 5 slova";
        private const string INPUT_ERROR_STREETNUMBER = "Broj ulice mora sadrzati broj";
        private const string INPUT_ERROR_TELEPHONE = "Broj telefona mora biti u formatu: AAA@YYY.ZZ";
        private const string INPUT_ERROR_EMAIL = "Email adresa mora biti u formatu: ";

        private Doctor _doctorForEdit;
        public EditMyProfile(Doctor doctorForEdit)
        {
            InitializeComponent();
            this.DataContext = this;
            _doctorForEdit = doctorForEdit;
            tbKontaktTelefon.Text = _doctorForEdit.Contact.Number;
            tbEmailAdresa.Text = _doctorForEdit.Contact.Email;
            tbGrad.Text = _doctorForEdit.Residence.City.Name;
            tbUlica.Text = _doctorForEdit.Residence.Street;
            tbBroj.Text = _doctorForEdit.Residence.Number.ToString();

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }


        private void btnEditProfilePassword_Click(object sender, RoutedEventArgs e)
        {
            EditMyProfilePassword dialog = new EditMyProfilePassword(_doctorForEdit);
            this.Close();
            dialog.ShowDialog();
        }

        private void btnSaveEditProfile_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                _doctorForEdit.Contact.Number = tbKontaktTelefon.Text;
                _doctorForEdit.Contact.Email = tbEmailAdresa.Text;
                _doctorForEdit.Residence.City.Name = tbGrad.Text;
                _doctorForEdit.Residence.Street = tbUlica.Text;
                _doctorForEdit.Residence.Number = int.Parse(tbBroj.Text);
                tbBroj.Text = _doctorForEdit.Residence.Number.ToString();

                

                this.DialogResult = true;
                this.Close();
            }
        }

        private bool isValidInputData()
        {
            string email = tbEmailAdresa.Text;
            if (tbGrad.Text.Length < 3)
            {
                ShowError(INPUT_ERROR_CITY);
                return false;
            }
            else if (tbUlica.Text.Length < 5)
            {
                ShowError(INPUT_ERROR_STREET);
                return false;
            } 
            else if (!int.TryParse(tbBroj.Text, out int i) || ( tbBroj.Text.Count()<9 && tbBroj.Text.Count() > 14))
            {
                ShowError(INPUT_ERROR_STREETNUMBER);
                return false;
            }
            else if (!IsValidEmail(tbEmailAdresa.Text))
            {
                ShowError(INPUT_ERROR_EMAIL);
                return false;
            } else if (!isPhoneNumberValid(tbKontaktTelefon.Text))
            {
                ShowError(INPUT_ERROR_TELEPHONE);
                return false;
            }
            return true;
        }
        private bool IsValidEmail(string emailaddress)
        { 
            return Regex.IsMatch(emailaddress, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        private bool isPhoneNumberValid(string phoneNumber)
        {
            return true;

        }


        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();
    }
}
