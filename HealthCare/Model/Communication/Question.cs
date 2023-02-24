// File:    Question.cs
// Created: Wednesday, April 15, 2020 22:59:47
// Purpose: Definition of Class Question

using Model.Users;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.Communication
{
   public class Question
   {
        private string _question;
        private string _title;
        private string _answer;
        private bool _isAnswered = false;
        private bool _isFAQ = false;
        private bool _forFAQ = false;
        private Doctor _doctor;
        private Patient _patient;
        private int _id;
        private string _patientName;

        public String PatientName
        {
            get { return _patientName; }
            set
            {
                _patientName = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public Patient Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                OnPropertyChanged();
            }
        }

        public Doctor Doctor
        {
            get { return _doctor; }
            set
            {
                _doctor = value;
                OnPropertyChanged();
            }
        }

        public bool IsAnswered
        {
            get { return _isAnswered; }
            set
            {
                _isAnswered = value;
                OnPropertyChanged();
            }
        }

        public bool IsFAQ
        {
            get { return _isFAQ; }
            set
            {
                _isFAQ = value;
                OnPropertyChanged();
            }
        }

        public bool ForFAQ
        {
            get { return _forFAQ; }
            set
            {
                _forFAQ = value;
                OnPropertyChanged();
            }
        }

        public String Questions
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged();
            }
        }

        public String Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}