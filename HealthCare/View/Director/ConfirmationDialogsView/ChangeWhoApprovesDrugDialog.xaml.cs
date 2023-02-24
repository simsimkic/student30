using Controller.DrugController;
using Controller.RoomControllers;
using Controller.UserControllers;
using HealthCare;
using Model.Drug;
using Model.Rooms;
using Model.Users;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HCIProjekat.View.ConfirmationDialogsView
{
    /// <summary>
    /// Interaction logic for ConfirmDialog.xaml
    /// </summary>
    public partial class ChangeWhoApprovesDrugDialog : Page
    {
        private readonly IDrugController _drugController;
        private readonly IUserController _userController;
        public static ObservableCollection<User> Staff { get; set; }

        private Model.Users.Doctor Doctor;
        public ChangeWhoApprovesDrugDialog(RoutedEventHandler potvrdiAkcija, User user)
        {
            var app = Application.Current as App;
            _userController = app.userController;
            _drugController = app.drugControler;

            Doctor = (Doctor)user;
            Staff = new ObservableCollection<User>(_userController.GetAllStaff().Where(x => x.GetType().Equals(typeof(Doctor))));
            InitializeComponent();

            odustanak.AddHandler(Button.ClickEvent, potvrdiAkcija);
            DataContext = this;
        }

        private void potvrda_Click(object sender, RoutedEventArgs e)
        {
            if (isDoctorSelected())
            {
                List<Drug> drugs = _drugController.GetDrugByDoctorWhoApprovesIt(Doctor).ToList();
                foreach(Drug d in drugs)
                {
                    d.whoApprovesDrug = new Doctor() { Id = ((Staff)doctor.SelectedItem).Id };
                    _drugController.Update(d);
                }
                odustanak.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
        private bool isDoctorSelected()
        {
            return doctor.SelectedItem != null;
        }
    }
}
