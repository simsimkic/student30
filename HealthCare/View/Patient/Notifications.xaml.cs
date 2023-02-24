using HealthCare;
using Model.Communication;
using Model.DataFiltration;
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
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Notifications : Page
    {

        public ObservableCollection<object> Collection { get; set; }


        public Notifications()
        {
            var app = Application.Current as App;
            Collection = new ObservableCollection<object>(app.notificationController.GetNotificationForUser(app._currentUser));
            foreach(Question q in app.questionController.GetAnsweredQuestionForPatient(app._currentUser))
            {
                Collection.Add(q);
            }

            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri(@"/View/Patient/Survey.xaml", UriKind.Relative));
        }
    }
}
