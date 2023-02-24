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
using HealthCare;
using Model.Communication;
using Model.Users;

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for survey.xaml
    /// </summary>
    public partial class Survey : Page
    {
        private Grades kindness;
        private Grades expertise;
        private Grades communication;
        private Grades organization;
        private String note;
        public Doctor Doctor { get; set; }

        public Survey(Doctor doctor)
        {
            Doctor = doctor;
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            note = NoteTextBox.Text;
            var app = Application.Current as App;
            double averageGrade = ((double)kindness + (double)expertise + (double)communication + (double)organization) / 4;
            FeedBack feedBack = new FeedBack() { Kindness = kindness, Expertise = expertise, Communication = communication, Organization = organization, ForDoctor = new Doctor() { Id = Doctor.Id }, Date = DateTime.Now, Note = note, PatientFullName = "anoniman", Average = averageGrade };
            app.feedBackController.Create(feedBack);
            
            this.NavigationService.Navigate(new Uri(@"/View/Patient/HomePage.xaml", UriKind.Relative));
        }

        private void Kindness1_Click(object sender, RoutedEventArgs e)
        {
            kindness = Grades.I;
        }
        private void Kindness2_Click(object sender, RoutedEventArgs e)
        {
            kindness = Grades.II;
        }
        private void Kindness3_Click(object sender, RoutedEventArgs e)
        {
            kindness = Grades.IV;
        }
        private void Kindness4_Click(object sender, RoutedEventArgs e)
        {
            kindness = Grades.V;
        }
        private void Expertise1_Click(object sender, RoutedEventArgs e)
        {
            expertise = Grades.I;
        }
        private void Expertise2_Click(object sender, RoutedEventArgs e)
        {
            expertise = Grades.II;
        }
        private void Expertise3_Click(object sender, RoutedEventArgs e)
        {
            expertise = Grades.IV;
        }
        private void Expertise4_Click(object sender, RoutedEventArgs e)
        {
            expertise = Grades.V;
        }
        private void Communication1_Click(object sender, RoutedEventArgs e)
        {
            communication = Grades.I;
        }
        private void Communication2_Click(object sender, RoutedEventArgs e)
        {
            communication = Grades.II;
        }
        private void Communication3_Click(object sender, RoutedEventArgs e)
        {
            communication = Grades.IV;
        }
        private void Communication4_Click(object sender, RoutedEventArgs e)
        {
            communication = Grades.V;
        }
        private void Organization1_Click(object sender, RoutedEventArgs e)
        {
            organization = Grades.I;
        }
        private void Organization2_Click(object sender, RoutedEventArgs e)
        {
            organization = Grades.II;
        }
        private void Organization3_Click(object sender, RoutedEventArgs e)
        {
            organization = Grades.IV;
        }
        private void Organization4_Click(object sender, RoutedEventArgs e)
        {
            organization = Grades.V;
        }
    }
}
