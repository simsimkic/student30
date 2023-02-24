/***********************************************************************
 * Module:  Staff.cs
 * Purpose: Definition of the Class Staff
 ***********************************************************************/

using System;

namespace Model.Users
{
   public class Staff : User
   {
        private Educationlevel educationLevel;
        private string picture;
        private int pictureNumber=0;

        public int PictureNumber
        {
            get { return pictureNumber; }
            set
            {
                if (pictureNumber != value)
                {
                    pictureNumber = value;
                }
            }
        }
        public Educationlevel EducationLevel
        {
            get { return educationLevel; }
            set
            {
                if (educationLevel != value)
                {
                    educationLevel = value;
                }
            }
        }

        public string Picture
        {
            get { return picture; }
            set
            {
                if (picture != value)
                {
                    picture = value;
                }
            }
        }

    }
}