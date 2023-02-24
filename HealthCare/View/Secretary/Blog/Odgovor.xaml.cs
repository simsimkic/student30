using HealthCare;
using HealthCare.Controller.CommunicationControllers;
using Model.Communication;
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

namespace Sekretar.Blog
{
    /// <summary>
    /// Interaction logic for Odgovor.xaml
    /// </summary>
    public partial class Odgovor : Window
    {
        private IQuestionController iQuestionControler;
        private Odgovor()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static Odgovor instance = null;
        public static Odgovor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Odgovor();
                }
                return instance;
            }
        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void ButtonCestoPitanje(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            iQuestionControler = app.questionController;
            OdgovoriLekara odgovori = OdgovoriLekara.Instance;
            CestaPitanja cestaPitanja = CestaPitanja.Instance;
            odgovori.zaFaq.IsFAQ = true;
            odgovori.zaFaq.ForFAQ = false;
            iQuestionControler.Update(odgovori.zaFaq);
            odgovori.ResetComponents();
            this.Visibility = Visibility.Hidden;
        }

        private void ButtonClickIzbrisi(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            iQuestionControler = app.questionController;

            OdgovoriLekara odgovori = OdgovoriLekara.Instance;
            odgovori.zaBrisaje.ForFAQ = false;
            iQuestionControler.Update(odgovori.zaBrisaje);

            odgovori.ResetComponents();
            this.Visibility = Visibility.Hidden;
        }
    }
}
