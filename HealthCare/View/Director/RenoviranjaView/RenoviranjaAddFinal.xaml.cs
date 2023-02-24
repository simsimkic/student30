using Controller.RoomControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.ProstorijeView.DataView;
using HealthCare;
using Model.Rooms;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.RenoviranjaView
{
    /// <summary>
    /// Interaction logic for RenoviranjaAddFinal.xaml
    /// </summary>
    public partial class RenoviranjaAddFinal : Page
    {
        private const string MANDATORY_DESCRIPTION_FIELD = "Opis renoviranja se mora uneti!";

        private DateTime dateFrom;
        private DateTime dateTo;
        public UserControl Room { get; set; }
        private IRenovateController renovateController;

        private string _note;
        public RenoviranjaAddFinal(DateTime dateFrom, DateTime dateTo, UserControl roomItem)
        {
            var app = Application.Current as App;
            renovateController = app.renovateController;

            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            this.Room = roomItem;
            InitializeComponent();

            datumOd.Text = dateFrom.ToShortDateString();
            datumDo.Text = dateTo.ToShortDateString();

            DataContext = this;
        }
        public string Note
        {
            get { return _note; }
            set
            {
                if (_note != value)
                {
                    _note = value;
                    OnPropertyChanged();
                }
            }
        }
        private void addRenovation_Click(object sender, RoutedEventArgs e)
        {
            if (!isRenovationDescriptionFilled())
            {
                ShowError(MANDATORY_DESCRIPTION_FIELD);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Zakazivanje renoviranja", "Da li ste sigurni da zelite da zakazete renoviranje?",
                                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmAddingEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private bool isRenovationDescriptionFilled()
        {
            return _note != "" && _note != null;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private void confirmAddingEvent(object sender, RoutedEventArgs e)
        {
            if (renovateController.Create(createRenovation()) == null)
            {
                ShowError("Za unete datume postoje zakazani pregledi u sobi ili je vec dodato renoviranje!");
            }
            else
            {
                MainWindow.AppWindow.navigateToRootPage(new Renoviranja(), false);
            }
        }

        private Renovate createRenovation()
        {
            return new Renovate() { DateStart = dateFrom.Date, DateEnd = dateTo.Date, Note = _note, room = new Room() { RoomNumber = ((RoomItem)Room).RoomNumber, RoomSector = ((RoomItem)Room).RoomSector, RoomType = ((RoomItem)Room).RoomType } };
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od zakazivanja", "Da li ste sigurni da zelite da odustanete od zakazivanja renoviranja?",
                                                     new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawAddingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawAddingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Renoviranja(), false);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
