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

namespace Sekretar.Obavestenja
{
    /// <summary>
    /// Interaction logic for ObavestenjaDetaljnije.xaml
    /// </summary>
    public partial class ObavestenjaDetaljnije : Window
    {   
        private ObavestenjaDetaljnije()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static ObavestenjaDetaljnije instance = null;
        public static ObavestenjaDetaljnije Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new ObavestenjaDetaljnije();
                }
                return instance;
            }
        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void Naslov_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
