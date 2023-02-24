using Controller.MedicalRecordControllers;
using HealthCare;
using Model.MedicalRecords;
using Model.Rooms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace Sekretar.Sale
{
    /// <summary>
    /// Interaction logic for UvidDanSoba.xaml
    /// </summary>
    public partial class UvidDanSoba : Window
    {
        IRefferalController iRefferalController;
        public ObservableCollection<HospitalTreatment> Sobe
        {
            get;
            set;
        }
        public HospitalTreatment hospitalTreatmentZaPremestanje;
        private UvidDanSoba()
        {
            InitializeComponent();
            HospitalTreatment ht = new HospitalTreatment();
            var app = Application.Current as App;
            Sobe = new ObservableCollection<HospitalTreatment>();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            iRefferalController = app.refferalControler;
            IEnumerable<Refferal> refferals = iRefferalController.GetAllRefferals().ToList();
           
            this.DataContext = this;
        }
        public void ResetComponents()
        {
            HospitalTreatment ht = new HospitalTreatment();
            var app = Application.Current as App;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            iRefferalController = app.refferalControler;
            Sobe.Clear();
            IEnumerable<Refferal> refferals = iRefferalController.GetAllRefferals().ToList();
            foreach (Refferal r in refferals)
            {
                if (r.GetType().Equals(ht.GetType()))
                {
                    ht = (HospitalTreatment)r;

                    if (ht.Completed)
                    {
                        Console.WriteLine(ht.DateTo.ToString() + "AAAAA" + this.DATUM.Content);
                        DateTime backupdate = Convert.ToDateTime(DATUM.Content.ToString());

                        if (ht.Room.RoomNumber.Equals(Sale.Instance.room.RoomNumber) &&
                            ht.Room.RoomSector.Name.Equals(Sale.Instance.room.RoomSector.Name) &&
                            (backupdate.Ticks >= ht.Date.Date.Ticks) &&
                            (backupdate.Ticks <= ht.DateTo.Date.Ticks))
                        {
                            Sobe.Add(ht);
                        }
                    }
                }
            }
            this.DataContext = this;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static UvidDanSoba instance = null;
        public static UvidDanSoba Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UvidDanSoba();
                }
                return instance;
            }
        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        public static IEnumerable<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("endDate must be greater than or equal to startDate");

            while (startDate <= endDate)
            {
                yield return startDate;
                startDate = startDate.AddDays(1);
            }
        }
        private void ButtonClickPremesti(object sender, RoutedEventArgs e)
        {
            Sale sale = Sale.Instance;
            PremestanjePacijenta.Instance.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            HospitalTreatment dataRowView = (HospitalTreatment)((Button)e.Source).DataContext;
            hospitalTreatmentZaPremestanje = dataRowView;
            Console.WriteLine(hospitalTreatmentZaPremestanje.Patient.Id + " ID PACIJENTA");
            String pacijent = dataRowView.Patient.Name + " " + dataRowView.Patient.Surname + "\n" + dataRowView.Date.ToShortDateString() + "-" + dataRowView.DateTo.ToShortDateString();
            PremestanjePacijenta premestanje = PremestanjePacijenta.Instance;
            premestanje.Pacijent.Content = pacijent;
            premestanje.DatumDo.Content = dataRowView.Date.ToShortDateString();
            premestanje.DatumOd.Content = dataRowView.DateTo.ToShortDateString();
            premestanje.SektorStarei.Content = dataRowView.Sector;
            PremestanjePacijenta.Instance.Pacijent.Content = pacijent;
            this.Visibility = Visibility.Collapsed;
            Sale.Instance.Visibility = Visibility.Collapsed;
            PremestanjePacijenta.Instance.ResetComponents();
            PremestanjePacijenta.Instance.ShowDialog();
        }

    }
}
