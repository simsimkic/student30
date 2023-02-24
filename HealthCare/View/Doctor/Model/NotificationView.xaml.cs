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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.Users;
using Controller.UserControllers;
using HealthCare;

namespace HealthCareClinic.View.Model
{
    /// <summary>
    /// Interaction logic for NotificationView.xaml
    /// </summary>
    public partial class NotificationView : UserControl
    {
        private readonly IUserController _userController;

        private long _id;
        private string _title;
        private string _text;
        private DateTime _date;

        private Staff _createdBy;

       

        public NotificationView()
        {
            InitializeComponent();
            DataContext = this;
            _userController = (Application.Current as App).userController;
        }


        public Staff CreatedBy
        {
            get { return _createdBy; }
            set
            {
                if (_createdBy != value)
                {
                    _createdBy = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }
        public long Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }   

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void btnNotificationDetails_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as NotificationView;

            NotificationDetails window = new NotificationDetails();
            window.lblDate.Content = dataObject.Date.ToString("dd MMM yyyy");
            window.lblTitle.Content = dataObject.Title;
            window.tbMessage.Text = dataObject.Text.ToString();

            User doctor = _userController.GetFromID(dataObject.CreatedBy.Id);

            window.lblDoctorName.Content = doctor.Name+" "+ doctor.Surname;
            window.ShowDialog();
        }
    }
}
