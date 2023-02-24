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

namespace Sekretar.Zahtevi
{
    /// <summary>
    /// Interaction logic for ZahteviDetaljnije.xaml
    /// </summary>
    public partial class ZahteviDetaljnije : Window
    {
        private ZahteviDetaljnije()
        {
            InitializeComponent();
        }
        private static ZahteviDetaljnije instance = null;
        public static ZahteviDetaljnije Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ZahteviDetaljnije();
                }
                return instance;
            }
        }

        private void Window_close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
