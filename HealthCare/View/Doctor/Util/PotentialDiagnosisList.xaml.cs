using HealthCare;
using Model.Diagnosis;
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
using System.Windows.Shapes;

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for PotentialDiagnosis.xaml
    /// </summary>
    public partial class PotentialDiagnosisList : Window
    {
        private List<Symptom> _addedSymptoms = new List<Symptom>();
        private Diagnosis _selectedDiagnosis;

        private List<Diagnosis> _diagnosis = new List<Diagnosis>();

        public PotentialDiagnosisList(List<Symptom> addedSymptoms)
        {
            InitializeComponent();
            this.DataContext = this;
            _addedSymptoms = addedSymptoms;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);


            var app = Application.Current as App;
            _diagnosis = app.diagnosisController.GetPotentialDiagnosis(_addedSymptoms).ToList();
        }

        public List<Diagnosis> Diagnosis
        {
            get
            {
                return _diagnosis;
            }
            set
            {
                _diagnosis = value;
            }
        }

        public Diagnosis SelectedDiagnosis()
        {
            return _selectedDiagnosis;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void listBoxSymptoms_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key==Key.Space)
            {
                handleListBoxSelectedItem();
            }
        }

        private void handleListBoxSelectedItem()
        {
            int index = this.listBoxSymptoms.SelectedIndex;
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                _selectedDiagnosis = (Diagnosis)listBoxSymptoms.SelectedItem;
                this.DialogResult = true;
                this.Close();
            }
        }

        private void listBoxSymptoms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            handleListBoxSelectedItem();
        }
    }
}
