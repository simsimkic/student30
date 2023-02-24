using Controller.CommunicationControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.Communication;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.SoftwareFeedbackView
{
    /// <summary>
    /// Interaction logic for SoftwareFeedback.xaml
    /// </summary>
    public partial class SoftwareFeedback : Page
    {
        private readonly ISoftwareRatingController softwareRatingController;
        public SoftwareFeedback()
        {
            var app = Application.Current as App;
            softwareRatingController = app.softwareRatingController;
            InitializeComponent();
        }

        private void design1_Click(object sender, RoutedEventArgs e)
        {
            designHart1.Visibility = Visibility.Visible;
            designHart2.Visibility = Visibility.Collapsed;
            designHart3.Visibility = Visibility.Collapsed;
            designHart4.Visibility = Visibility.Collapsed;
            designHart5.Visibility = Visibility.Collapsed;
        }

        private void design2_Click(object sender, RoutedEventArgs e)
        {
            designHart1.Visibility = Visibility.Visible;
            designHart2.Visibility = Visibility.Visible;
            designHart3.Visibility = Visibility.Collapsed;
            designHart4.Visibility = Visibility.Collapsed;
            designHart5.Visibility = Visibility.Collapsed;
        }

        private void design3_Click(object sender, RoutedEventArgs e)
        {
            designHart1.Visibility = Visibility.Visible;
            designHart2.Visibility = Visibility.Visible;
            designHart3.Visibility = Visibility.Visible;
            designHart4.Visibility = Visibility.Collapsed;
            designHart5.Visibility = Visibility.Collapsed;
        }

        private void design4_Click(object sender, RoutedEventArgs e)
        {
            designHart1.Visibility = Visibility.Visible;
            designHart2.Visibility = Visibility.Visible;
            designHart3.Visibility = Visibility.Visible;
            designHart4.Visibility = Visibility.Visible;
            designHart5.Visibility = Visibility.Collapsed;
        }

        private void design5_Click(object sender, RoutedEventArgs e)
        {
            designHart1.Visibility = Visibility.Visible;
            designHart2.Visibility = Visibility.Visible;
            designHart3.Visibility = Visibility.Visible;
            designHart4.Visibility = Visibility.Visible;
            designHart5.Visibility = Visibility.Visible;
        }

        private void funcitionality1_Click(object sender, RoutedEventArgs e)
        {
            funcitionalityHart1.Visibility = Visibility.Visible;
            funcitionalityHart2.Visibility = Visibility.Collapsed;
            funcitionalityHart3.Visibility = Visibility.Collapsed;
            funcitionalityHart4.Visibility = Visibility.Collapsed;
            funcitionalityHart5.Visibility = Visibility.Collapsed;
        }

        private void funcitionality2_Click(object sender, RoutedEventArgs e)
        {
            funcitionalityHart1.Visibility = Visibility.Visible;
            funcitionalityHart2.Visibility = Visibility.Visible;
            funcitionalityHart3.Visibility = Visibility.Collapsed;
            funcitionalityHart4.Visibility = Visibility.Collapsed;
            funcitionalityHart5.Visibility = Visibility.Collapsed;
        }

        private void funcitionality3_Click(object sender, RoutedEventArgs e)
        {
            funcitionalityHart1.Visibility = Visibility.Visible;
            funcitionalityHart2.Visibility = Visibility.Visible;
            funcitionalityHart3.Visibility = Visibility.Visible;
            funcitionalityHart4.Visibility = Visibility.Collapsed;
            funcitionalityHart5.Visibility = Visibility.Collapsed;
        }

        private void funcitionality4_Click(object sender, RoutedEventArgs e)
        {
            funcitionalityHart1.Visibility = Visibility.Visible;
            funcitionalityHart2.Visibility = Visibility.Visible;
            funcitionalityHart3.Visibility = Visibility.Visible;
            funcitionalityHart4.Visibility = Visibility.Visible;
            funcitionalityHart5.Visibility = Visibility.Collapsed;
        }

        private void funcitionality5_Click(object sender, RoutedEventArgs e)
        {
            funcitionalityHart1.Visibility = Visibility.Visible;
            funcitionalityHart2.Visibility = Visibility.Visible;
            funcitionalityHart3.Visibility = Visibility.Visible;
            funcitionalityHart4.Visibility = Visibility.Visible;
            funcitionalityHart5.Visibility = Visibility.Visible;
        }

        private void speed5_Click(object sender, RoutedEventArgs e)
        {
            speedHart1.Visibility = Visibility.Visible;
            speedHart2.Visibility = Visibility.Visible;
            speedHart3.Visibility = Visibility.Visible;
            speedHart4.Visibility = Visibility.Visible;
            speedHart5.Visibility = Visibility.Visible;
        }

        private void speed1_Click(object sender, RoutedEventArgs e)
        {
            speedHart1.Visibility = Visibility.Visible;
            speedHart2.Visibility = Visibility.Collapsed;
            speedHart3.Visibility = Visibility.Collapsed;
            speedHart4.Visibility = Visibility.Collapsed;
            speedHart5.Visibility = Visibility.Collapsed;
        }

        private void speed2_Click(object sender, RoutedEventArgs e)
        {
            speedHart1.Visibility = Visibility.Visible;
            speedHart2.Visibility = Visibility.Visible;
            speedHart3.Visibility = Visibility.Collapsed;
            speedHart4.Visibility = Visibility.Collapsed;
            speedHart5.Visibility = Visibility.Collapsed;
        }

        private void speed3_Click(object sender, RoutedEventArgs e)
        {
            speedHart1.Visibility = Visibility.Visible;
            speedHart2.Visibility = Visibility.Visible;
            speedHart3.Visibility = Visibility.Visible;
            speedHart4.Visibility = Visibility.Collapsed;
            speedHart5.Visibility = Visibility.Collapsed;
        }

        private void speed4_Click(object sender, RoutedEventArgs e)
        {
            speedHart1.Visibility = Visibility.Visible;
            speedHart2.Visibility = Visibility.Visible;
            speedHart3.Visibility = Visibility.Visible;
            speedHart4.Visibility = Visibility.Visible;
            speedHart5.Visibility = Visibility.Collapsed;
        }

        private Grades getSpeedGrade()
        {
            if (speedHart5.Visibility == Visibility.Visible)
                return Grades.V;
            else if (speedHart4.Visibility == Visibility.Visible)
                return Grades.IV;
            else if (speedHart3.Visibility == Visibility.Visible)
                return Grades.III;
            else if (speedHart2.Visibility == Visibility.Visible)
                return Grades.II;
            else
                return Grades.I;
        }

        private Grades getFunctionalityGrade()
        {
            if (funcitionalityHart5.Visibility == Visibility.Visible)
                return Grades.V;
            else if (funcitionalityHart4.Visibility == Visibility.Visible)
                return Grades.IV;
            else if (funcitionalityHart3.Visibility == Visibility.Visible)
                return Grades.III;
            else if (funcitionalityHart2.Visibility == Visibility.Visible)
                return Grades.II;
            else
                return Grades.I;
        }

        private Grades getDesignGrade()
        {
            if (designHart5.Visibility == Visibility.Visible)
                return Grades.V;
            else if (designHart4.Visibility == Visibility.Visible)
                return Grades.IV;
            else if (designHart3.Visibility == Visibility.Visible)
                return Grades.III;
            else if (designHart2.Visibility == Visibility.Visible)
                return Grades.II;
            else
                return Grades.I;
        }

        private Grades getReliabillityGrade()
        {
            if (reliabillityHart5.Visibility == Visibility.Visible)
                return Grades.V;
            else if (reliabillityHart4.Visibility == Visibility.Visible)
                return Grades.IV;
            else if (reliabillityHart3.Visibility == Visibility.Visible)
                return Grades.III;
            else if (reliabillityHart2.Visibility == Visibility.Visible)
                return Grades.II;
            else
                return Grades.I;
        }

        private void feedbackEvent(object sender, RoutedEventArgs e)
        {
            softwareRatingController.Create(createSoftwareFeedback());
            MainWindow.AppWindow.navigateToRootPage(new mainPage(), false);
        }

        private SoftwareRating createSoftwareFeedback()
        {
            return new SoftwareRating()
            {
                Speed = getSpeedGrade(),
                Functionality = getFunctionalityGrade(),
                Reliabillity = getReliabillityGrade(),
                Design = getDesignGrade(),
                Note = feedbackDescription.Text
            };
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private void rateApp_Click(object sender, RoutedEventArgs e)
        {
            if (!areGradesFullFiled())
            {
                ShowError("Sve ocene se moraju uneti!");
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Ocena rada softvera", "Da li ste sigurni da zelite da ocenite softver?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(feedbackEvent),
                                                    "Odustani", "Oceni"));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private bool areGradesFullFiled()
        {
            return isDesignFullFilled() && isReliabillityFullFiled() && isFunctionalityFullFiled() && isSpeedFullFiled();
        }

        private bool isSpeedFullFiled()
        {
            return speedHart1.Visibility == Visibility.Visible;
        }

        private bool isFunctionalityFullFiled()
        {
            return funcitionalityHart1.Visibility == Visibility.Visible;
        }

        private bool isReliabillityFullFiled()
        {
            return reliabillityHart1.Visibility == Visibility.Visible;
        }

        private bool isDesignFullFilled()
        {
            return designHart1.Visibility == Visibility.Visible;
        }

        private void reliabillity1_Click(object sender, RoutedEventArgs e)
        {
            reliabillityHart1.Visibility = Visibility.Visible;
            reliabillityHart2.Visibility = Visibility.Collapsed;
            reliabillityHart3.Visibility = Visibility.Collapsed;
            reliabillityHart4.Visibility = Visibility.Collapsed;
            reliabillityHart5.Visibility = Visibility.Collapsed;
        }

        private void reliabillity2_Click(object sender, RoutedEventArgs e)
        {
            reliabillityHart1.Visibility = Visibility.Visible;
            reliabillityHart2.Visibility = Visibility.Visible;
            reliabillityHart3.Visibility = Visibility.Collapsed;
            reliabillityHart4.Visibility = Visibility.Collapsed;
            reliabillityHart5.Visibility = Visibility.Collapsed;
        }

        private void reliabillity3_Click(object sender, RoutedEventArgs e)
        {
            reliabillityHart1.Visibility = Visibility.Visible;
            reliabillityHart2.Visibility = Visibility.Visible;
            reliabillityHart3.Visibility = Visibility.Visible;
            reliabillityHart4.Visibility = Visibility.Collapsed;
            reliabillityHart5.Visibility = Visibility.Collapsed;
        }

        private void reliabillity4_Click(object sender, RoutedEventArgs e)
        {
            reliabillityHart1.Visibility = Visibility.Visible;
            reliabillityHart2.Visibility = Visibility.Visible;
            reliabillityHart3.Visibility = Visibility.Visible;
            reliabillityHart4.Visibility = Visibility.Visible;
            reliabillityHart5.Visibility = Visibility.Collapsed;
        }

        private void reliabillity5_Click(object sender, RoutedEventArgs e)
        {
            reliabillityHart1.Visibility = Visibility.Visible;
            reliabillityHart2.Visibility = Visibility.Visible;
            reliabillityHart3.Visibility = Visibility.Visible;
            reliabillityHart4.Visibility = Visibility.Visible;
            reliabillityHart5.Visibility = Visibility.Visible;
        }
    }
}
