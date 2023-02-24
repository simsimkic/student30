using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HealthCare;
using Model.Appointment;
using Model.Rooms;
using Model.Users;

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for Appointment.xaml
    /// </summary>
    public partial class Appointments : Page, INotifyPropertyChanged
    {



        public ObservableCollection<Appointment> ScheduledAppointments { get; set; }
 
 
        public Appointments()
        {
            ScheduledAppointments = new ObservableCollection<Appointment>();
            var app = Application.Current as App;
            User currentUser = app._currentUser;
            var appointments = app.appointmentController.GetAppointmentForPatient(currentUser);
            foreach(Appointment appointment in appointments)
            {
                Doctor doctorObject = (Doctor)app.userController.GetFromID(appointment.Doctor.Id);
                appointment.Doctor = doctorObject;
                Room roomObject = app.roomController.GetFromID(appointment.Room.RoomNumber);
                appointment.Room = roomObject;
                ScheduledAppointments.Add(appointment);
            }
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            Appointment dataContext = menuItem.DataContext as Appointment;
            var app = Application.Current as App;
            app.appointmentController.Delete(dataContext);
            ScheduledAppointments.Remove(dataContext);
        }
    }
}
