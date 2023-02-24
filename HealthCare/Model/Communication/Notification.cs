// File:    Notification.cs
// Created: Wednesday, April 15, 2020 23:21:33
// Purpose: Definition of Class Notification

using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.Communication
{
   public class Notification
   {
        private string _title;
        private string _text;
        private DateTime _dateTime;

        public List<User> users;
        public Staff createdBy;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
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