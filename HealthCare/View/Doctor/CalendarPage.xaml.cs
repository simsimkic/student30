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

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            InitializeComponent();
            calendars.Focus();
        }

        private void btnCreateAbsence_Click(object sender, RoutedEventArgs e)
        {
            AbsenceAdd dialog = new AbsenceAdd();
            dialog.ShowDialog();
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                btnCreateAbsence_Click(sender, e);
            }
        }
    }
}
