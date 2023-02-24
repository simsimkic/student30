using HCIProjekat.View.ReportView.DataView;
using System.Windows.Controls;

namespace HCIProjekat.View.ReportView
{
    /// <summary>
    /// Interaction logic for DetaljiFiltriranihLekara.xaml
    /// </summary>
    public partial class DetaljiFiltriranihLekara : Page
    {
        public UserControl Doctor { get; set; }
        public DetaljiFiltriranihLekara(UserControl Doctor)
        {
            this.Doctor = Doctor;
            InitializeComponent();


            DataContext = this;
            setChartBarWidth(((FilteredDoctorItem)Doctor).WorkTime, ((FilteredDoctorItem)Doctor).AppointmentTime);

        }

        private void setChartBarWidth(double ukupnoVreme, double zauzetoVreme)
        {
            double slobodnoVreme = ukupnoVreme - zauzetoVreme;
            if (ukupnoVreme > 0)
            {
                slobodno.Text = ((slobodnoVreme / ukupnoVreme) * 100).ToString("0.##");
                zauzeto.Text = ((zauzetoVreme / ukupnoVreme) * 100).ToString("0.##");

                slobodnoBar.Width = (slobodnoVreme / ukupnoVreme) * 300;
                zauzetoBar.Width = (zauzetoVreme / ukupnoVreme) * 300;
            }
            else
            {
                slobodno.Text = "0";
                zauzeto.Text = "0";

                slobodnoBar.Width = 0;
                zauzetoBar.Width = 0;
            }
        }
    }
}
