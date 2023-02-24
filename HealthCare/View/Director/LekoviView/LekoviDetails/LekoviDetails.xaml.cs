using HCIProjekat.LekoviView;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.LekoviView.DataView;
using Model.Drug;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.LekoviView
{
    /// <summary>
    /// Interaction logic for LekoviDetails.xaml
    /// </summary>
    public partial class LekoviDetails : Page
    {
        private UserControl _drug;
        public LekoviDetails(UserControl Drug)
        {
            _drug = Drug;
            InitializeComponent();

            DataContext = this;
        }

        public UserControl Drug
        {
            get { return _drug; }
            set { _drug = value; }
        }

        private void withdraw_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
        }
        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private void change_Click(object sender, RoutedEventArgs e)
        {
            if ((int)(((DrugItem)Drug).Status) == (int)DrugStatus.Waiting)
            {
                ShowError("Lek jos uvek nije odobren!");
            }
            else
            {
                MainWindow.AppWindow.navigateWithTitleChange(new LekoviEditFirst(Drug), false);
            }
        }

        private void viewNext_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new LekoviDetailsNext(Drug), false);
        }
    }
}
