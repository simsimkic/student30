using Controller.CommunicationControllers;
using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.ObavestenjeView
{
    /// <summary>
    /// Interaction logic for ObavestenjeAdd.xaml
    /// </summary>
    public partial class ObavestenjeAdd : Page
    {
        private const string MANDATORY_TITLE_FIELD = "Naslov obavestenja je obavezno polje!";
        private const string MANDATORY_RECIEVERS_FIELD = "Primaoci obavestenja se moraju izabrati!";
        private const string MANDATORY_TEXT_FIELD = "Tekst obavestenja je obavezno polje!";

        private string _title;
        private string _text;

        private readonly INotificationController _notificationController;
        private readonly IUserController _userController;

        public ObavestenjeAdd()
        {
            var app = Application.Current as App;
            _notificationController = app.notificationController;
            _userController = app.userController;

            InitializeComponent();
            DataContext = this;

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
        public string NotificationTitle
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
        private void addConfirmation_Click(object sender, RoutedEventArgs e)
        {
            if (!isTitleFulfilled())
            {
                ShowError(MANDATORY_TITLE_FIELD);
            }
            else if (!areRecieversSelected())
            {
                ShowError(MANDATORY_RECIEVERS_FIELD);
            }
            else if (!isTextFulfilled())
            {
                ShowError(MANDATORY_TEXT_FIELD);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Objavljivanje obavestenja", "Da li ste sigurni da zelite da objavite obavestenje?",
                                                        new RoutedEventHandler(withdrawAddingEvent), new RoutedEventHandler(addNotificationEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private bool isTextFulfilled()
        {
            return _text != "" && _text != null;
        }

        private bool areRecieversSelected()
        {
            return Primaoci.SelectedItem != null;
        }

        private bool isTitleFulfilled()
        {
            return _title != "" && _title != null;
        }

        private void addNotificationEvent(object sender, RoutedEventArgs e)
        {
            _notificationController.Create(createNotification());
            MainWindow.AppWindow.navigateToRootPage(new Obavestenje(), false);
        }

        private Notification createNotification()
        {
            Notification ret = new Notification() { Date = DateTime.Now, Text = _text, Title = _title, users = new List<User>() };
            ret.createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id };
            List<User> lista = addRecievers();
            foreach (var v in lista)
            {
                if (v.GetType().Equals(typeof(Doctor)))
                    ret.users.Add(new Model.Users.Doctor() { Id = v.Id });
                else if (v.GetType().Equals(typeof(Model.Users.Secretary)))
                    ret.users.Add(new Model.Users.Secretary() { Id = v.Id });
                else if (v.GetType().Equals(typeof(Patient)))
                    ret.users.Add(new Model.Users.Patient() { Id = v.Id });
                else
                    ret.users.Add(new Model.Users.Director() { Id = v.Id });
            }

            return ret;
        }

        private List<User> addRecievers()
        {
            List<User> lista = new List<User>();
            if (Primaoci.SelectedIndex == 0)
                lista = _userController.GetAll().ToList();
            else if (Primaoci.SelectedIndex == 1)
                lista = _userController.GetAllStaff().ToList();
            else if (Primaoci.SelectedIndex == 2)
                lista = _userController.GetAllStaff().Where(x => x.GetType().Equals(typeof(Doctor))).ToList();
            else if (Primaoci.SelectedIndex == 4)
                lista = _userController.GetAllStaff().Where(x => x.GetType().Equals(typeof(Model.Users.Secretary))).ToList();
            else
                lista = _userController.GetAll().Where(x => x.GetType().Equals(typeof(Patient))).ToList();
            return lista;
        }

        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawAddingEvent));
        }
        private void withdrawAddingEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od slanja obavestenja", "Da li ste sigurni da zelite da odustanete od slanja obavestenja?",
                                                   new RoutedEventHandler(withdrawAddingEvent), new RoutedEventHandler(withdrawNitifyEvent),
                                                   "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void withdrawNitifyEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new HCIProjekat.View.ObavestenjeView.Obavestenje(), false);
        }
    }
}
