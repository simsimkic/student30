using HealthCare;
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

namespace HealthCareClinic.View.Model
{
    /// <summary>
    /// Interaction logic for RecordTreatmentInformation.xaml
    /// </summary>

    public partial class RecordTreatmentInformationView : UserControl
    {
        //Fale informacije pacijenta geteri seteri itd..
        //private readonly NotificationController _recordController;
        public ObservableCollection<UserControl> Data { get; set; }
        public RecordTreatmentInformationView()
        {
            InitializeComponent();

            DataContext = this;
            var app = Application.Current as App;

            //_recordController = app.RecordController;

            //Data = new ObservableCollection<UserControl>
        }
    }
}
