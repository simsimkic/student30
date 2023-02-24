// File:    FeedBackFilter.cs
// Created: Friday, May 8, 2020 23:40:02
// Purpose: Definition of Class FeedBackFilter

using System;

namespace Model.DataFiltration
{
   public class FeedBackFilter
   {
      private int id;
      private double averageGradeFrom;
      private double averageGradeTo;

        public double AverageGradeFrom
        {
            get { return averageGradeFrom; }
            set
            {
                if (averageGradeFrom != value)
                {
                    averageGradeFrom = value;
                }
            }
        }
        public double AverageGradeTo
        {
            get { return averageGradeTo; }
            set
            {
                if (averageGradeTo != value)
                {
                    averageGradeTo = value;
                }
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                }
            }
        }

    }
}