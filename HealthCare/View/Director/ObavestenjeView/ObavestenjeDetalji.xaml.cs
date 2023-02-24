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

namespace HCIProjekat.View.ObavestenjeView
{
    /// <summary>
    /// Interaction logic for ObavestenjeDetalji.xaml
    /// </summary>
    public partial class ObavestenjeDetalji : Page
    {
        public UserControl Notification { get; set; }

        public ObavestenjeDetalji(UserControl Notification)
        {
            this.Notification = Notification;
            InitializeComponent();

            DataContext = this;
        }
    }
}
