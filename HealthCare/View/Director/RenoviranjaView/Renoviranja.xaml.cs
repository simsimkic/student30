using Controller.RoomControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.RenoviranjaView.DataView;
using HealthCare;
using HealthCare.View.Director.RenoviranjaView.Converter;
using Model.DataFiltration;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.RenoviranjaView
{
    /// <summary>
    /// Interaction logic for Renoviranja.xaml
    /// </summary>
    public partial class Renoviranja : Page
    {
        public ObservableCollection<UserControl> Renovations { get; set; }
        private IRenovateController renovateController;

        public Renoviranja(bool filterDissmis = true, RenovateFilter renovateFilter = null)
        {
            var app = Application.Current as App;
            renovateController = app.renovateController;

            InitializeComponent();

            if (!filterDissmis)
            {
                disableFilter.Visibility = Visibility.Visible;
                Renovations = new ObservableCollection<UserControl>(RenovationItemConverter.ConvertRenovateListToRenovateViewList(renovateController.GetFilteredRenovations(renovateFilter).ToList()));
            }
            else
            {
                Renovations = new ObservableCollection<UserControl>(RenovationItemConverter.ConvertRenovateListToRenovateViewList(renovateController.GetAll().ToList()));
            }

            DataContext = this;
        }

        private void listaRenoviranja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaRenoviranja.SelectedItem != null)
            {
                setButtonVisibility(Visibility.Visible);
            }
            else
            {
                setButtonVisibility(Visibility.Collapsed);
            }
        }

        private void setButtonVisibility(Visibility state)
        {
            details.Visibility = state;
            deleteRenovation.Visibility = state;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new RenoviranjaFilter(), false);
        }

        private void addRenovation_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new RenoviranjaAddFirst(), false);
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawDeleteRenovationEvent));
        }
        private void deleteRenovation_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            if (((RenovationItem)listaRenoviranja.SelectedItem).StartDate.Date <= DateTime.Now.Date)
            {
                ShowError("Renoviranje nije moguce obrisati jer je vec zvrseno ili je u toku!");
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Brisanje renoviranja", "Da li ste sigurni da zelite da obrisete izabrano renoviranje?",
                                                        new RoutedEventHandler(withdrawDeleteRenovationEvent), new RoutedEventHandler(deleteRenovationEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private void deleteRenovationEvent(object sender, RoutedEventArgs e)
        {

            renovateController.Delete(renovateController.GetFromID(((RenovationItem)listaRenoviranja.SelectedItem).Id));
            dialog.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Renoviranja(), false);
        }

        private void withdrawDeleteRenovationEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void disableFilter_Click(object sender, RoutedEventArgs e)
        {
            disableFilter.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Renoviranja(), false);
        }
    }
}
