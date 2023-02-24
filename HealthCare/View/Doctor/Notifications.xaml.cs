using HealthCareClinic.View.Model;
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
using System.Windows.Shapes;

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for NotificationList.xaml
    /// </summary>
    public partial class Notifications : Window
    {
        public Notifications()
        {
            InitializeComponent();
            DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }
        public NotificationDataView GetDataView() => DataViewer;

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
