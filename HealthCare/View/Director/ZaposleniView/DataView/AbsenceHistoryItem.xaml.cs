using Model.Users;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.ZaposleniView.DataView
{
    /// <summary>
    /// Interaction logic for AbsenceHistoryItem.xaml
    /// </summary>
    public partial class AbsenceHistoryItem : UserControl
    {
        private AbsenceType _absenceKind;
        private DateTime _dateStartAbsence;
        private DateTime _dateEndAbsence;
        public AbsenceHistoryItem()
        {
            InitializeComponent();
            DataContext = this;
        }
        public AbsenceType AbsenceKind
        {
            get { return _absenceKind; }
            set
            {
                if (_absenceKind != value)
                {
                    _absenceKind = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateStartAbsence
        {
            get { return _dateStartAbsence; }
            set
            {
                if (_dateStartAbsence != value)
                {
                    _dateStartAbsence = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateEndAbsence
        {
            get { return _dateEndAbsence; }
            set
            {
                if (_dateEndAbsence != value)
                {
                    _dateEndAbsence = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
