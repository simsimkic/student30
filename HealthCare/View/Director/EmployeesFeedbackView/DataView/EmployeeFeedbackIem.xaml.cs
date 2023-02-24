using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCIProjekat.View.EmployeesFeedbackView.DataView
{
    /// <summary>
    /// Interaction logic for EmployeeFeedbackIem.xaml
    /// </summary>
    public partial class EmployeeFeedbackItem : UserControl
    {

        private int _id;
        private int _kindness;
        private int _expertise;
        private int _communication;
        private int _organization;
        private double _average;
        private string _name;
        private string _surname;
        private string _workPlace;
        private string _picture;
        private string _comment;
        private DateTime _date;
        public EmployeeFeedbackItem()
        {
            InitializeComponent();
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Kindness
        {
            get { return _kindness; }
            set
            {
                if (_kindness != value)
                {
                    _kindness = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Average
        {
            get { return _average; }
            set
            {
                if (_average != value)
                {
                    _average = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Expertise
        {
            get { return _expertise; }
            set
            {
                if (_expertise != value)
                {
                    _expertise = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Communication
        {
            get { return _communication; }
            set
            {
                if (_communication != value)
                {
                    _communication = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Organization
        {
            get { return _organization; }
            set
            {
                if (_organization != value)
                {
                    _organization = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Picture
        {
            get { return _picture; }
            set
            {
                if (_picture != value)
                {
                    _picture = value;
                    OnPropertyChanged();
                }
            }
        }
        public string WorkPlace
        {
            get { return _workPlace; }
            set
            {
                if (_workPlace != value)
                {
                    _workPlace = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EmployeeName
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
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
