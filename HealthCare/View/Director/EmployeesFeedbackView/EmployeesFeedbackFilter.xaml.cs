using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.DataFiltration;
using Model.Users;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.View.EmployeesFeedbackView
{
    /// <summary>
    /// Interaction logic for EmployeesFeedbackFilter.xaml
    /// </summary>
    public partial class EmployeesFeedbackFilter : Page
    {
        private const string INVALID_GRADE_ERROR_MESSAGE = "Uneta prosecna ocena nije validna!";
        private const string INVALID_GRADE_RANGE_ERROR_MESSAGE = "Prosecna ocena od mora biti manja od prosecne ocene do!";
        private static readonly Regex _decimalRegex = new Regex(@"[^0-9,]+$");

        private readonly IUserController userController;
        public static ObservableCollection<User> Staff { get; set; }

        private double _gradeFrom;
        private double _gradeTo;

        public EmployeesFeedbackFilter()
        {
            var app = Application.Current as App;
            userController = app.userController;

            Staff = new ObservableCollection<User>(userController.GetAllStaff().Where(x => x.GetType().Equals(typeof(Doctor))));

            InitializeComponent();

            DataContext = this;
        }

        private void applyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!anythingFulFilled())
            {
                MainWindow.AppWindow.navigateToRootPage(new EmployeesFeedback(), false);
                return;
            }
            if (isGradeFromFulFilled())
            {
                if (!isGradeFromValid())
                {
                    ShowError(INVALID_GRADE_ERROR_MESSAGE);
                    return;
                }
            }
            if (isGradeToFulFilled())
            {
                if (!isGradeToValid())
                {
                    ShowError(INVALID_GRADE_ERROR_MESSAGE);
                    return;
                }
                if (isGradeFromFulFilled())
                {
                    if (_gradeFrom > _gradeTo)
                    {
                        ShowError(INVALID_GRADE_RANGE_ERROR_MESSAGE);
                        return;
                    }
                }
            }
            MainWindow.AppWindow.navigateToRootPage(new EmployeesFeedback(false, createFilter()), false);
        }

        private FeedBackFilter createFilter()
        {
            if (!isGradeFromFulFilled())
                _gradeFrom = 0;
            if (!isGradeToFulFilled())
                _gradeTo = 0;
            return new FeedBackFilter() { AverageGradeFrom = _gradeFrom, AverageGradeTo = _gradeTo, Id = (doctor.SelectedItem != null) ? ((Staff)doctor.SelectedItem).Id : -1 };
        }

        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawPurchaseEvent));
        }

        private void withdrawPurchaseEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }
        private bool isGradeToValid()
        {
            if (!double.TryParse(gradeTo.Text, out double grade))
            {
                return false;
            }
            else
            {
                _gradeTo = grade;
                return true;
            }
        }

        private bool isGradeToFulFilled()
        {
            return gradeTo.Text != "" && gradeTo.Text != null;
        }

        private bool isGradeFromValid()
        {
            if (!double.TryParse(gradeFrom.Text, out double grade))
            {
                return false;
            }
            else
            {
                _gradeFrom = grade;
                return true;
            }
        }

        private bool isGradeFromFulFilled()
        {
            return gradeFrom.Text != "" && gradeFrom.Text != null;
        }

        private bool anythingFulFilled()
        {
            return isGradeFromFulFilled() || isGradeToFulFilled() || doctor.SelectedItem != null;
        }

        private bool isTextDouble(string input) => !_decimalRegex.IsMatch(input);

        private void gradeFrom_PreviewTextInput(object sender, TextCompositionEventArgs e)
              => e.Handled = !isTextDouble(e.Text);

        private void gradeFrom_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string input = (string)e.DataObject.GetData(typeof(string));
                if (!isTextDouble(input)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
