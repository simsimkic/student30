using Model.Drug;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.LekoviView.DataView
{
    /// <summary>
    /// Interaction logic for DrugItem.xaml
    /// </summary>
    public partial class DrugItem : UserControl, INotifyPropertyChanged
    {
        private int _id;
        private string _drugName;
        private string _manufacturer;
        private double _drugQuantity;
        private string _purpose;
        private string _sideEffect;
        private Formatdrug _format;
        private string _instruction;
        private int _amount;
        private DrugStatus status;
        private string rejectReason;
        private List<Ingredient> _ingredients;
        private List<Allergen> _allergens;
        public DrugItem()
        {
            InitializeComponent();
            DataContext = this;
        }
        public List<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set
            {
                if (_ingredients != value)
                {
                    _ingredients = value;
                    OnPropertyChanged();
                }
            }
        }
        public DrugStatus Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<Allergen> Allergens
        {
            get { return _allergens; }
            set
            {
                if (_allergens != value)
                {
                    _allergens = value;
                    OnPropertyChanged();
                }
            }
        }

        public Formatdrug Format
        {
            get { return _format; }
            set
            {
                if (_format != value)
                {
                    _format = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged();
                }
            }
        }
        public string RejectReason
        {
            get { return rejectReason; }
            set
            {
                if (rejectReason != value)
                {
                    rejectReason = value;
                    OnPropertyChanged();
                }
            }
        }
        public string SideEffect
        {
            get { return _sideEffect; }
            set
            {
                if (_sideEffect != value)
                {
                    _sideEffect = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Purpose
        {
            get { return _purpose; }
            set
            {
                if (_purpose != value)
                {
                    _purpose = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Instruction
        {
            get { return _instruction; }
            set
            {
                if (_instruction != value)
                {
                    _instruction = value;
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

        public double DrugQuantity
        {
            get { return _drugQuantity; }
            set
            {
                if (_drugQuantity != value)
                {
                    _drugQuantity = value;
                    OnPropertyChanged();
                }
            }
        }


        public string DrugName
        {
            get { return _drugName; }
            set
            {
                if (_drugName != value)
                {
                    _drugName = value;
                    OnPropertyChanged();
                }
            }
        }


        public string Manufacturer
        {
            get { return _manufacturer; }
            set
            {
                if (_manufacturer != value)
                {
                    _manufacturer = value;
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
