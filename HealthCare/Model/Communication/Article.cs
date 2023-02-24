// File:    Article.cs
// Created: Wednesday, April 15, 2020 22:53:20
// Purpose: Definition of Class Article

using Model.Users;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.Communication
{
   public class Article
   {
      private int _id;
      private string _title;
      private string _text;
      private DateTime _date;
      private string _image;
      
      private Model.Users.Staff _createdBy;
   
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public int Id {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
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

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public Staff CreatedBy
        {
            get { return _createdBy; }
            set
            {
                _createdBy = value;
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