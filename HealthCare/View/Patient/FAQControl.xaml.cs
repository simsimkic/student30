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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HealthCare.View.Patient
{
    /// <summary>
    /// Interaction logic for FAQControl.xaml
    /// </summary>
    public partial class FAQControl : UserControl
    {



        public string Question
        {
            get { return (string)GetValue(QuestionProperty); }
            set { SetValue(QuestionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Question.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QuestionProperty =
            DependencyProperty.Register("Question", typeof(string), typeof(FAQControl));



        public string Answer
        {
            get { return (string)GetValue(AnswerProperty); }
            set { SetValue(AnswerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Answer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnswerProperty =
            DependencyProperty.Register("Answer", typeof(string), typeof(FAQControl));



        public FAQControl()
        {
            InitializeComponent();
        }
    }
}
