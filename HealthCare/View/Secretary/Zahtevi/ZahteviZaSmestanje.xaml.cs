using Controller.MedicalRecordControllers;
using Controller.UserControllers;
using HealthCare;
using Model.MedicalRecords;
using Model.Users;
using Model.Users.UserBuilder;
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

namespace Sekretar.Zahtevi
{
    /// <summary>
    /// Interaction logic for ZahteviZaSmestanje.xaml
    /// </summary>
    public partial class ZahteviZaSmestanje : Window
    {
        IRefferalController iRefferalController;
        public Patient pacijentTreatment;
        public Patient pacijentSurgery;
        public UrgentSurgeryRequest urgentSurgeryKojiSmestamo;
        public HospitalTreatment hospitalTreatmentKojiSmestamo;
        private IUserController iUserController;
        public ObservableCollection<HospitalTreatment> zahtevi
        {
            get;
            set;
        }
        
        public ObservableCollection<UrgentSurgeryRequest> urgentSurgery
        {
            get;
            set;
        }
        private ZahteviZaSmestanje()
        {
            HospitalTreatment ht = new HospitalTreatment();
            UrgentSurgeryRequest usr = new UrgentSurgeryRequest();
            InitializeComponent();
            var app = Application.Current as App;
            iUserController = app.userController;
            zahtevi = new ObservableCollection<HospitalTreatment>();
            urgentSurgery = new ObservableCollection<UrgentSurgeryRequest>();
            iRefferalController = app.refferalControler;
            IEnumerable<Refferal> refferals = iRefferalController.GetAllRefferals().ToList();
            foreach (Refferal r in refferals)
            {
                if (r.GetType().Equals(ht.GetType()))
                {
                    ht = (HospitalTreatment)r;
                    if(ht.Completed == false)  
                        zahtevi.Add(ht);
                }
                else if(r.GetType().Equals(typeof(UrgentSurgeryRequest)))
                {
                    usr = (UrgentSurgeryRequest)r;
                    if(usr.Approved == false)
                        urgentSurgery.Add(usr);
                }
            }

            this.DataContext = this;
            
        }
        private static ZahteviZaSmestanje instance = null;
        public static ZahteviZaSmestanje Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ZahteviZaSmestanje();
                }
                return instance;
            }
        }

        private void ButtonClickPocetna(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Application.Current.MainWindow.ShowDialog();
        }
        private void ButtonClickDetaljnije(object sender, RoutedEventArgs e)
        {
            HospitalTreatment dataRowView = (HospitalTreatment)((Button)e.Source).DataContext;
            dataRowView.Patient = (Patient)(Application.Current as App).userController.GetFromID(dataRowView.Patient.Id);
            ZahteviDetaljnije detaljnije = ZahteviDetaljnije.Instance;
            detaljnije.Note.Text = dataRowView.Note;
            detaljnije.Pacijent.Content = dataRowView.Patient.Name + " " + dataRowView.Patient.Surname;
            detaljnije.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            detaljnije.Sektor.Content = dataRowView.Sector.Name;
            detaljnije.ShowDialog();
        }

        internal void ResetComponents()
        {
            HospitalTreatment ht = new HospitalTreatment();
            UrgentSurgeryRequest usr = new UrgentSurgeryRequest();
            InitializeComponent();
            var app = Application.Current as App;
            zahtevi.Clear();
            urgentSurgery.Clear();
            iRefferalController = app.refferalControler;
            IEnumerable<Refferal> refferals = iRefferalController.GetAllRefferals().ToList();
            foreach (Refferal r in refferals)
            {
                if (r.GetType().Equals(ht.GetType()))
                {
                    ht = (HospitalTreatment)r;
                    if (ht.Completed == false)
                        zahtevi.Add(ht);
                }
                else if(r.GetType().Equals(typeof(UrgentSurgeryRequest)))
                {
                    usr = (UrgentSurgeryRequest)r;
                    if (usr.Approved == false)
                        urgentSurgery.Add(usr);
                }
            }

            this.DataContext = this;
        }

        private void ButtonClickDetaljnijeSurgery(object sender, RoutedEventArgs e)
        {
            UrgentSurgeryRequest dataRowView = (UrgentSurgeryRequest)((Button)e.Source).DataContext;
            dataRowView.Patient = (Patient)(Application.Current as App).userController.GetFromID(dataRowView.Patient.Id);
            ZahteviDetaljnije detaljnije = ZahteviDetaljnije.Instance;
            detaljnije.Note.Text = dataRowView.Note;
            detaljnije.Sektor.Content = dataRowView.Specialization.Name;
            detaljnije.Pacijent.Content = dataRowView.Patient.Name + " " + dataRowView.Patient.Surname;
            detaljnije.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            detaljnije.ShowDialog();
        }
        private void ButtonClickSmestiSurgery(object sender, RoutedEventArgs e)
        {

            UrgentSurgeryRequest dataRowView = (UrgentSurgeryRequest)((Button)e.Source).DataContext;
            dataRowView.Patient = (Patient)(Application.Current as App).userController.GetFromID(dataRowView.Patient.Id);
            String ProductName = dataRowView.Patient.Name + " " + dataRowView.Patient.Surname;
            urgentSurgeryKojiSmestamo = (UrgentSurgeryRequest)((Button)e.Source).DataContext;

            pacijentSurgery = dataRowView.Patient;

            if (pacijentSurgery == null)
                return;


            SmestiOperacija operacija = SmestiOperacija.Instance;
            operacija.Pacijent.Content = ProductName;
            operacija.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Visibility = Visibility.Collapsed;
            operacija.ResetComponents();
            operacija.ShowDialog();
        }
        private void ButtonClickSmesti(object sender, RoutedEventArgs e)
        {
            HospitalTreatment dataRowView = (HospitalTreatment)((Button)e.Source).DataContext;
            dataRowView.Patient = (Patient)(Application.Current as App).userController.GetFromID(dataRowView.Patient.Id);

            pacijentTreatment = dataRowView.Patient;


            //pacijentTreatment = (Patient)iUserController.Create(pacijentTreatment);
            if (pacijentTreatment == null)
                return;

            SmestiSoba soba = SmestiSoba.Instance;
            hospitalTreatmentKojiSmestamo = (HospitalTreatment)((Button)e.Source).DataContext;
            String ProductName = dataRowView.Patient.Name + " " + dataRowView.Patient.Surname;
            soba.Pacijent.Content = ProductName;
            soba.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Visibility = Visibility.Collapsed;
            soba.ResetComponents();
            soba.ShowDialog();
                
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
