using Controller.DrugController;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.LekoviView;
using HCIProjekat.View.LekoviView.DataView;
using HCIProjekat.View.LekoviView.LekoviCreate;
using HealthCare;
using HealthCare.View.Director.LekoviView.Converter;
using Model.DataFiltration;
using Model.Drug;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.LekoviView
{
    /// <summary>
    /// Interaction logic for Lekovi.xaml
    /// </summary>
    public partial class Lekovi : Page
    {
        public static ObservableCollection<UserControl> Drugs { get; set; }
        private readonly IDrugController drugController;


        public Lekovi(bool filterDissmis = true, DrugFilter drugFilter = null)
        {


            InitializeComponent();

            var app = Application.Current as App;
            drugController = app.drugControler;


            if (!filterDissmis)
            {
                disableFilter.Visibility = Visibility.Visible;
                Drugs = new ObservableCollection<UserControl>(DrugConverter.ConvertDrugListToDrugViewList(drugController.GetFilteredDrugs(drugFilter).ToList()));
            }
            else
            {
                Drugs = new ObservableCollection<UserControl>(DrugConverter.ConvertDrugListToDrugViewList(drugController.GetAll().ToList()));
            }

            DataContext = this;

        }

        private void purchaseDrug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            if ((int)((DrugItem)listaLekova.SelectedItem).Status == (int)DrugStatus.Waiting || (int)((DrugItem)listaLekova.SelectedItem).Status == (int)DrugStatus.Rejected)
            {
                ShowError("Lek jos uvek nije odobren!");
            }
            else
            {
                MainWindow.AppWindow.navigateWithTitleChange(new LekoviNabavka((DrugItem)listaLekova.SelectedItem), false);
            }
        }

        private void addDrug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new LekoviAddFirst(), false);
        }

        private void listaLekova_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaLekova.SelectedItem != null)
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
            editDrug.Visibility = state;
            deleteDrug.Visibility = state;
            detailsDrug.Visibility = state;
            purchaseDrug.Visibility = state;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new LekoviFilter(), false);
        }

        private void deleteDrug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;

            dialog.Children.Add(new ConfirmDialog("Brisanje leka", "Da li ste sigurni da zelite da obrisete izabrani lek?",
                                                    new RoutedEventHandler(withdrawDeleteDrugEvent), new RoutedEventHandler(deleteDrugEvent)));
            dialog.Visibility = Visibility.Visible;
        }

        private void deleteDrugEvent(object sender, RoutedEventArgs e)
        {
            drugController.Delete(drugController.GetFromID(((DrugItem)listaLekova.SelectedItem).Id));
            dialog.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
        }

        private void withdrawDeleteDrugEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;

                if((int)((DrugItem)listaLekova.SelectedItem).Status == (int)DrugStatus.Rejected)
                    MainWindow.AppWindow.navigateWithTitleChange(new LekoviDetailsOdbijeni((DrugItem)listaLekova.SelectedItem), false);
                else
                    MainWindow.AppWindow.navigateWithTitleChange(new LekoviDetails((DrugItem)listaLekova.SelectedItem), false);
                //MainWindow.AppWindow.navigateWithTitleChange(new BlogDetails((BlogItem)listaClanaka.SelectedItem), false);
            }

        }

        private void detailsDrug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            if ((int)((DrugItem)listaLekova.SelectedItem).Status == (int)DrugStatus.Rejected)
                MainWindow.AppWindow.navigateWithTitleChange(new LekoviDetailsOdbijeni((DrugItem)listaLekova.SelectedItem), false);
            else
                MainWindow.AppWindow.navigateWithTitleChange(new LekoviDetails((DrugItem)listaLekova.SelectedItem), false);
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawDeleteDrugEvent));
        }
        private void editDrug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            if ((int)((DrugItem)listaLekova.SelectedItem).Status == (int)DrugStatus.Waiting)
            {
                ShowError("Lek jos uvek nije odobren!");
            }
            else{
                MainWindow.AppWindow.navigateWithTitleChange(new LekoviEditFirst((DrugItem)listaLekova.SelectedItem), false);
            }
        }

        private void disableFilter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Lekovi(),false);
        }
    }
}
