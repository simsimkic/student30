/***********************************************************************
 * Module:  Symptom.cs
 * Purpose: Definition of the Class Symptom
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Model.Diagnosis
{
   public class Symptom
   {
      private string _name;
      
      public string Name
      {
            get{  return _name;  }
            set{ _name = value; }
        }
   }
}