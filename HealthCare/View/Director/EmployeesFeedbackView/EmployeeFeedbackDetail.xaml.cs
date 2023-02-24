using System.Windows.Controls;

namespace HCIProjekat.View.EmployeesFeedbackView
{
    /// <summary>
    /// Interaction logic for EmployeeFeedbackDetail.xaml
    /// </summary>
    public partial class EmployeeFeedbackDetail : Page
    {
        public UserControl Feedback { get; set; }
        public EmployeeFeedbackDetail(UserControl Feedback)
        {
            this.Feedback = Feedback;
            InitializeComponent();

            DataContext = this;
        }
    }
}
