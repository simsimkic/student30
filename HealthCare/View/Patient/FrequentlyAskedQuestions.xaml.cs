using HealthCare;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for FrequentlyAskedQuestions.xaml
    /// </summary>

    public partial class FrequentlyAskedQuestions : Page
    {
        public ObservableCollection<Question> FAQList { get; set; }
        public FrequentlyAskedQuestions()
        
        
        {
            var app = Application.Current as App;
            FAQList = new ObservableCollection<Question>(app.questionController.GetFAQ());
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
