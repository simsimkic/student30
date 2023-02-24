using Controller.CommunicationControllers;
using Controller.UserControllers;
using HCIProjekat.View.EmployeesFeedbackView.DataView;
using HealthCare;
using HealthCare.View.Director.EmployeesFeedbackView.Converter;
using Model.Communication;
using Model.DataFiltration;
using Model.Users;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.View.EmployeesFeedbackView
{
    /// <summary>
    /// Interaction logic for EmployeesFeedback.xaml
    /// </summary>
    public partial class EmployeesFeedback : Page
    {
        public ObservableCollection<UserControl> EmployeeFeedbacks { get; set; }
        private IUserController userController;
        private IFeedBackController feedBackController;
        public EmployeesFeedback(bool filterDissmis = true, FeedBackFilter feedBackFilter = null)
        {
            var app = Application.Current as App;
            feedBackController = app.feedBackController;
            userController = app.userController;

            InitializeComponent();

            if (!filterDissmis)
            {
                List<FeedBack> feedBacks = feedBackController.GetFeedBackFiltered(feedBackFilter).ToList();
                foreach (FeedBack feedBack in feedBacks)
                    feedBack.ForDoctor = (Doctor)userController.GetFromID(feedBack.ForDoctor.Id);
                disableFilter.Visibility = Visibility.Visible;
                EmployeeFeedbacks = new ObservableCollection<UserControl>(EmployeeFeedbackItemConverter.ConvertFeedBackListToFeedBackViewList(feedBacks));
            }
            else
            {
                List<FeedBack> feedBacks = feedBackController.GetAll().ToList();
                foreach (FeedBack feedBack in feedBacks)
                    feedBack.ForDoctor = (Doctor)userController.GetFromID(feedBack.ForDoctor.Id);
                EmployeeFeedbacks = new ObservableCollection<UserControl>(EmployeeFeedbackItemConverter.ConvertFeedBackListToFeedBackViewList(feedBacks));
            }

            DataContext = this;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new EmployeesFeedbackFilter(), false);
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                MainWindow.AppWindow.navigateWithTitleChange(new EmployeeFeedbackDetail((EmployeeFeedbackItem)listaOcena.SelectedItem), false);
            }
        }

        private void details_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new EmployeeFeedbackDetail((EmployeeFeedbackItem)listaOcena.SelectedItem), false);
        }

        private void disableFilter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new EmployeesFeedback(), false);
        }
    }
}
