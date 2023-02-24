using HealthCare;
using HealthCare.Controller.CommunicationControllers;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for CestaPitanja.xaml
    /// </summary>
    public partial class CestaPitanja : Window
    {
        private IQuestionController iQuestionControler;
        public ObservableCollection<Question> cestaPitanja
        {
            get;
            set;
        }

        private CestaPitanja()
        {
            InitializeComponent();
            var app = Application.Current as App;
            iQuestionControler = app.questionController;
            cestaPitanja = new ObservableCollection<Question>(iQuestionControler.GetFAQ());
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        public void ResetComponents()
        {
            var app = Application.Current as App;
            iQuestionControler = app.questionController;
            cestaPitanja.Clear();
            List<Question> pitanja = iQuestionControler.GetFAQ().ToList();
            foreach (Question q in pitanja)
            {
                cestaPitanja.Add(q);
            }
            this.DataContext = this;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static CestaPitanja instance = null;
        public static CestaPitanja Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CestaPitanja();
                }
                return instance;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        private void DodajPitanja(object sender, RoutedEventArgs e)
        {
            DodajPitanje dodajPitanje = DodajPitanje.Instance;
            dodajPitanje.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dodajPitanje.ShowDialog();

        }

       

        private void ButtonClickProcitaj(object sender, RoutedEventArgs e)
        {
            try
            {
                Question dataRowView = (Question)((Button)e.Source).DataContext;
                String ProductName = dataRowView.Questions;
                Pitanje pitanje = Pitanje.Instance;
                pitanje.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pitanje.Naslov.Text = ProductName;
                pitanje.TekstPitanje.Text = dataRowView.Answer;
                pitanje.ShowDialog(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
