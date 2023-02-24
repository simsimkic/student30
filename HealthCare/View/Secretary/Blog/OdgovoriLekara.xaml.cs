using HealthCare;
using HealthCare.Controller.CommunicationControllers;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OdgovoriLekara.xaml
    /// </summary>
    public partial class OdgovoriLekara : Window
    {
        public Question zaBrisaje;
        public Question zaFaq;
        private IQuestionController iQuestionControler;
        public ObservableCollection<Question> odgovori
        {
            get;
            set;
        }
        private OdgovoriLekara()
        {
            InitializeComponent();
            this.DataContext = this;
            odgovori = new ObservableCollection<Question>();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }


        public void ResetComponents()
        {
            var app = Application.Current as App;
            iQuestionControler = app.questionController;
            odgovori.Clear();
            List<Question> pitanja = iQuestionControler.GetAll().ToList();
            foreach (Question q in pitanja)
            {
                if(q.ForFAQ)
                    odgovori.Add(q);
            }
            this.DataContext = this;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static OdgovoriLekara instance = null;
        public static OdgovoriLekara Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OdgovoriLekara();
                }
                return instance;
            }
        }
        private void ButtonClickProcitaj(object sender, RoutedEventArgs e)
        {
            try
            {
                zaBrisaje = (Question)((Button)e.Source).DataContext;
                zaFaq = (Question)((Button)e.Source).DataContext;

                Question dataRowView = (Question)((Button)e.Source).DataContext;
                String ProductName = dataRowView.Questions;
                Odgovor odgovor = Odgovor.Instance;
                odgovor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                odgovor.Naslov.Text = ProductName;
                odgovor.OdgovorTekst.Text = dataRowView.Answer;
                odgovor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
