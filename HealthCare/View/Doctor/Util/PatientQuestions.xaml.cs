using Controller.UserControllers;
using HealthCare;
using HealthCare.Controller.CommunicationControllers;
using HealthCareClinic.View.Doctor;
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

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for PatientQuestions.xaml
    /// </summary>
    public partial class PatientQuestions : Window
    {
        private List<Question> _questions = new List<Question>();
        private readonly IQuestionController _questionController;
        private readonly IUserController _userController;
        public PatientQuestions()
        {

            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            var app = Application.Current as App;

            _questionController = app.questionController;
            _userController = app.userController;

            _questions = _questionController.GetQuestionForDoctor(app._currentUser).ToList() ;

            listBoxPatientQuestions.SelectedIndex = 0;
        }

        public List<Question> Questions
        {
            get { return _questions; }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }


        private void listBoxPatientQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Question _selectedQuestion = (Question)listBoxPatientQuestions.SelectedValue;

            if (_selectedQuestion != null)
            {
                if (_selectedQuestion.IsAnswered)
                {
                    btnPatientQuestionAnswer.IsEnabled = false;
                    btnDeletePatientQuestion.IsEnabled = true;
                    if(!_selectedQuestion.ForFAQ)
                        btnPatientQuestionFAQ.IsEnabled = true;
                    else
                        btnPatientQuestionFAQ.IsEnabled = false;

                    tbAnswer.Focusable= false;
                    tbAnswer.IsReadOnly = true;

                    tbQuestion.Text = _selectedQuestion.Questions;
                    tbAnswer.Text = _selectedQuestion.Answer;
                    tbQuestionTitle.Text = _selectedQuestion.Title;
                }
                else
                {
                    btnPatientQuestionAnswer.IsEnabled = true;
                    btnDeletePatientQuestion.IsEnabled = true;
                    btnPatientQuestionFAQ.IsEnabled = false;

                    tbAnswer.IsReadOnly = false;
                    tbAnswer.Focusable = true;

                    tbQuestion.Text = _selectedQuestion.Questions;
                    tbAnswer.Text = _selectedQuestion.Answer;
                    tbQuestionTitle.Text = _selectedQuestion.Title;
                }
            }
        }

        private void btnDeletePatientQuestion_Click(object sender, RoutedEventArgs e)
        {
            Question _selectedQuestion = (Question)listBoxPatientQuestions.SelectedValue;

            if (_selectedQuestion != null)
            {
                if (listBoxPatientQuestions.SelectedIndex == 0)
                    listBoxPatientQuestions.SelectedIndex = listBoxPatientQuestions.SelectedIndex + 1;
                else
                    listBoxPatientQuestions.SelectedIndex = listBoxPatientQuestions.SelectedIndex - 1;

                _questions.Remove(_selectedQuestion);
                
                listBoxPatientQuestions.Items.Refresh();
            }

            if (listBoxPatientQuestions.Items.Count ==0)
            {
                
                tbQuestion.Text = "";
                tbAnswer.Text = "";
                tbQuestionTitle.Text = "";
            }
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();





        private void btnPatientQuestionAnswer_Click(object sender, RoutedEventArgs e)
        {
            Question _selectedQuestion = (Question)listBoxPatientQuestions.SelectedValue;

            if (_selectedQuestion != null)
            {
                if (validInput())
                {
                    _selectedQuestion.Answer = tbAnswer.Text;
                    _selectedQuestion.IsAnswered = true;
                    _questionController.Update(_selectedQuestion);
                    btnPatientQuestionFAQ.IsEnabled = true;
                    btnPatientQuestionAnswer.IsEnabled = false;
                    tbAnswer.IsReadOnly = true;
                    tbAnswer.Focusable = false;
                }
            }

        }

        private bool validInput()
        {

            if (tbAnswer.Text.Length < 2)
            {
                ShowError("Morate uneti odgovor");
                return false;
            }
            return true;
        }

        private void btnPatientQuestionFAQ_Click(object sender, RoutedEventArgs e)
        {
            Question _selectedQuestion = (Question)listBoxPatientQuestions.SelectedValue;

            if (_selectedQuestion != null)
                ShowError("Uspesno poslat predlog za FAQ");

            _selectedQuestion.ForFAQ = true;
            _questionController.Update(_selectedQuestion);

        }

        private void listBoxPatientQuestions_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                Question _selectedQuestion = (Question)listBoxPatientQuestions.SelectedValue;

                if (_selectedQuestion != null)
                    btnDeletePatientQuestion_Click(sender, e);
            }

            if (e.Key == Key.F && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                Question _selectedQuestion = (Question)listBoxPatientQuestions.SelectedValue;

                if (_selectedQuestion != null && _selectedQuestion.IsAnswered)
                    btnPatientQuestionFAQ_Click(sender, e);
            }
        }
    }
}
