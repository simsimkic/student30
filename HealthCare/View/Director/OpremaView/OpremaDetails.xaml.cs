using HCIProjekat.OpremaView;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.OpremaView
{
    /// <summary>
    /// Interaction logic for OpremaDetails.xaml
    /// </summary>
    public partial class OpremaDetails : Page
    {
        public UserControl Inventory { get; set; }
        public OpremaDetails(UserControl Inventory)
        {
            this.Inventory = Inventory;
            InitializeComponent();

            DataContext = this;
        }

        private void editEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new OpremaEdit(Inventory), false);
        }

        private void withdraw_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
        }
    }
}
