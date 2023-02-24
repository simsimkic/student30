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

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for AppointmentControl.xaml
    /// </summary>
    public partial class AppointmentControl : UserControl
    {

        public string TreatmentType
        {
            get { return (string)GetValue(TreatmentTypeProperty); }
            set { SetValue(TreatmentTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TreatmentTypeProperty =
            DependencyProperty.Register("TreatmentType", typeof(string), typeof(AppointmentControl), new PropertyMetadata(""));

        public string Doctor
        {
            get { return (string)GetValue(DoctorProperty); }
            set { SetValue(DoctorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DoctorProperty =
            DependencyProperty.Register("Doctor", typeof(string), typeof(AppointmentControl), new PropertyMetadata(""));

        public string Room
        {
            get { return (string)GetValue(RoomProperty); }
            set { SetValue(RoomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoomProperty =
            DependencyProperty.Register("Room", typeof(string), typeof(AppointmentControl), new PropertyMetadata(""));

        public string Period
        {
            get { return (string)GetValue(PeriodProperty); }
            set { SetValue(PeriodProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PeriodProperty =
            DependencyProperty.Register("Period", typeof(string), typeof(AppointmentControl), new PropertyMetadata(""));


        public AppointmentControl()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
        }
    }
}
