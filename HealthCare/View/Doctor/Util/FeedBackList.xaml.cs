using Controller.CommunicationControllers;
using HealthCare;
using HealthCare.View.Patient;
using Model.Communication;
using Model.Users;
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

namespace HealthCareClinic.HCI
{
    /// <summary>
    /// Interaction logic for FeedBackList.xaml
    /// </summary>
    public partial class FeedBackList : Window
    {
        private List<FeedBack> _feedBack = new List<FeedBack>();
        private readonly IFeedBackController _feedBackController;
        public FeedBackList()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            _feedBackController = (Application.Current as App).feedBackController;

            _feedBack = _feedBackController.GetFeedbackForDoctor((Doctor)(Application.Current as App)._currentUser).ToList();

            initAverageFeedBack();
        }

        private void initAverageFeedBack()
        {
            double averageKidness = 0;
            double averageCommunication = 0;
            double averageOrganisation = 0;
            double averageExpertise = 0;

            foreach(FeedBack item in _feedBack)
            {
                averageKidness += intFormat(item.Kindness);
                averageCommunication += intFormat(item.Communication);
                averageOrganisation += intFormat(item.Organization);
                averageExpertise += intFormat(item.Expertise);
            }

            lbLjubaznost.Content = Math.Round(averageKidness / _feedBack.Count(), 2);
            lbKomunikacija.Content = Math.Round(averageCommunication / _feedBack.Count(), 2);
            lbOrganizacija.Content = Math.Round(averageOrganisation / _feedBack.Count(), 2);
            lbStrucnost.Content = Math.Round(averageExpertise / _feedBack.Count(), 2);

        }

        public int intFormat(Grades item)
        {
            if (item == Grades.I)
                return 1;
            else if (item == Grades.II)
                return 2;
            else if (item == Grades.III)
                return 3;
            else if (item == Grades.IV)
                return 4;
            else
                return 5;
        }

        public List<FeedBack> FeedBack
        {
            get { return _feedBack; }
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private void listBoxFeedBack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FeedBack _selectedFeedBack = (FeedBack)listBoxFeedBack.SelectedValue;

            lbLjubaznostValue.Content = _selectedFeedBack.Kindness;
            lbKomunikacijaValue.Content = _selectedFeedBack.Communication;
            lbOrganizacijaValue.Content = _selectedFeedBack.Organization;
            lbStrucnostValue.Content = _selectedFeedBack.Expertise;
            lbDateValue.Content = _selectedFeedBack.Date.ToString("dd MMM yyyy");

            tbNote.Text = _selectedFeedBack.Note;
        }

    }
}
