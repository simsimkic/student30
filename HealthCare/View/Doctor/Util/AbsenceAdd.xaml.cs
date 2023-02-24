using Controller.UserControllers;
using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for AbsenceAdd.xaml
    /// </summary>
    public partial class AbsenceAdd : Window, INotifyPropertyChanged
    {
        private const string INPUT_ERROR_TYPE = "Morate uneti vrstu odsustva";
        private const string INPUT_ERROR_DATEFROM = "Morate uneti datum od kojeg zelite odsustvo";
        private const string INPUT_ERROR_DATETO = "Morate uneti datum do kojeg zelite odsustvo";

        private readonly IAbsenceController _absenceController;
        public AbsenceAdd()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.dPickerReviewTreatmentsFrom.PreviewKeyDown += new KeyEventHandler(HandleDP);
            this.dPickerReviewTreatmentsTo.PreviewKeyDown += new KeyEventHandler(HandleDP);
            this.cbAbsenceType.KeyDown += new KeyEventHandler(HandleCB);

            var app = Application.Current as App;

            _absenceController = app.absenceController;

        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {

                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }

        private void HandleDP(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {

                ((DatePicker)sender).IsDropDownOpen = true;
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private DateTime _dateFrom = DateTime.Now.AddDays(1);
        public DateTime dateFrom
        {
            get { return _dateFrom; }
            set
            {
                this._dateFrom = value;
                OnPropertyChanged("dateFrom");
            }
        }

        private DateTime _dateEnd = DateTime.Now.AddDays(90);
        public DateTime dateEnd
        {
            get { return _dateEnd; }
            set
            {
                this._dateEnd = value;
                OnPropertyChanged("dateEnd");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void btnSendAbsence_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInput())
            {
                Absence absence = new Absence();
                absence.AbsenceType = (AbsenceType)cbAbsenceType.SelectedItem;
                absence.StartDate = (DateTime)dPickerReviewTreatmentsFrom.SelectedDate;
                absence.EndDate = (DateTime)dPickerReviewTreatmentsTo.SelectedDate;
                absence.staff = (Staff)(Application.Current as App)._currentUser;
                if (tbNote.Text != null)
                    absence.Reason = tbNote.Text;
                else
                    absence.Reason = "";
                if (_absenceController.Create(absence) == null)
                {
                    ShowError("U datom periodu vec imate zakazane preglede");

                }
                this.Close();
            }
        }

        private bool isValidInput()
        {
            if (cbAbsenceType.SelectedItem == null)
            {
                ShowError(INPUT_ERROR_TYPE);
                return false;
            }else if (dPickerReviewTreatmentsFrom.SelectedDate == null)
            {
                ShowError(INPUT_ERROR_DATEFROM);
                return false;
            }
            else if (dPickerReviewTreatmentsTo.SelectedDate == null)
            {
                ShowError(INPUT_ERROR_DATETO);
                return false;
            }
            return true;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

    }
}
