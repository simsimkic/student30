using Controller.RoomControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.OpremaView;
using HCIProjekat.View.OpremaView.DataView;
using HealthCare;
using HealthCare.View.Director.OpremaView.Converter;
using Model.DataFiltration;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.OpremaView
{
    /// <summary>
    /// Interaction logic for Oprema.xaml
    /// </summary>
    public partial class Oprema : Page
    {
        public static ObservableCollection<UserControl> Equipment { get; set; }

        public readonly IInventoryController _inventoryController;
        public Oprema(bool filterDissmis = true, InventoryFilter inventoryFilter = null)
        {
            var app = Application.Current as App;
            _inventoryController = app.inventoryController;

            InitializeComponent();
            if (!filterDissmis)
            {
                disableFilter.Visibility = Visibility.Visible;
                Equipment = new ObservableCollection<UserControl>(InventoryConverter.ConvertInventoryListToInventoryViewList(_inventoryController.GetFilteredInventory(inventoryFilter).ToList()));
            }
            else
            {
                Equipment = new ObservableCollection<UserControl>(InventoryConverter.ConvertInventoryListToInventoryViewList(_inventoryController.GetAll().ToList()));
            }
            DataContext = this;
        }

        private void purchaseEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new OpremaNabavka((EquipmentItem)listaOpreme.SelectedItem), false);
        }

        private void movingEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new OpremaPremestanje((EquipmentItem)listaOpreme.SelectedItem), false);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new OpremaFilter(), false);
        }

        private void amountPerRoom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new OpremaPoProstorijama((EquipmentItem)listaOpreme.SelectedItem), false);
        }

        private void addEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new OpremaAdd(), false);
        }

        private void listaOpreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaOpreme.SelectedItem != null)
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
            movingEquipment.Visibility = state;
            amountPerRoom.Visibility = state;
            purchaseEquipment.Visibility = state;
            editEquipment.Visibility = state;
            deleteEquipment.Visibility = state;
            detailEquipment.Visibility = state;
        }

        private void deleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            dialog.Children.Add(new ConfirmDialog("Brisanje opreme", "Da li ste sigurni da zelite da obrisete izabranu opremu?",
                                                    new RoutedEventHandler(withdrawDeleteEquipmentEvent), new RoutedEventHandler(deleteEquipmentEvent)));
            dialog.Visibility = Visibility.Visible;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawDeleteEquipmentEvent));
        }
        private void deleteEquipmentEvent(object sender, RoutedEventArgs e)
        {
            if (_inventoryController.Delete(_inventoryController.GetFromID(((EquipmentItem)listaOpreme.SelectedItem).Id)) == null)
            {
                ShowError("Izabrana oprema se ne moze izbrisati jer se koristi u nekoj od prostorija!");
            }
            else
            {
                dialog.Visibility = Visibility.Collapsed;
                MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
            }
        }

        private void withdrawDeleteEquipmentEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
                MainWindow.AppWindow.navigateWithTitleChange(new OpremaDetails((EquipmentItem)listaOpreme.SelectedItem), false);
            }
        }

        private void editEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new OpremaEdit((EquipmentItem)listaOpreme.SelectedItem), false);
        }

        private void detailEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new OpremaDetails((EquipmentItem)listaOpreme.SelectedItem), false);
        }

        private void disableFilter_Click(object sender, RoutedEventArgs e)
        {
            disableFilter.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
        }
    }
}
