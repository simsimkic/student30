using Controller.UserControllers;
using HealthCare;
using HealthCareClinic.View;
using Model.Users;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace HealthCareClinic.HCI
{
    /// <summary>
    /// Interaction logic for MyProfilePage.xaml
    /// </summary>
    public partial class MyProfilePage : Page
    {
        private readonly IUserController _userController;
        private String _image;
        private Doctor _doctorUser;

        public MyProfilePage()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;

            _doctorUser = (Doctor)app._currentUser;
            _userController = app.userController;
            _image  = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _doctorUser.Picture);


        }

        public String Image
        {
            get { return _image; }
            set
            {
                _image = value;
            }
        }

        public Doctor DoctorUser
        {
            get { return _doctorUser; }
            set { _doctorUser = value;
                OnPropertyChanged();
            }
        }

        private void btnEditMyProfile_Click(object sender, RoutedEventArgs e)
        {

                EditMyProfile editDialog = new EditMyProfile(_doctorUser);
                if (editDialog.ShowDialog() == true)
                {
                    lblDoctorContactValue.Content = _doctorUser.Contact.Number;
                    lblDoctorContactEmailValue.Content = _doctorUser.Contact.Email;
                    lblDoctorResidenceCityValue.Content = _doctorUser.Residence.City.Name;
                    lblDoctorResidenceStreetAndNumber.Content = _doctorUser.Residence.Street + " " + _doctorUser.Residence.Number;
                    _userController.Update(_doctorUser);
                }
        }


        private void btnEditMyProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Dispay OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                _image = filename;
                imageBrush.ImageSource = new BitmapImage(new Uri(filename, UriKind.Relative));

                _doctorUser.Picture = filename;
                _userController.Update(_doctorUser);
            }
        }


        private void btnEditMyProfileBiography_Click(object sender, RoutedEventArgs e)
        {
            EditMyProfileBiography editDialog = new EditMyProfileBiography(_doctorUser);
            if (editDialog.ShowDialog() == true)
            {
                tbBiography.Text = _doctorUser.Biography;
                _userController.Update(_doctorUser);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
