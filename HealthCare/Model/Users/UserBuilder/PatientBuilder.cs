using System;

namespace Model.Users.UserBuilder
{
    public class PatientBuilder : IUserBuilder
    {
        private Patient Patient;

        public PatientBuilder()
        {
            this.Patient = new Patient();
        }
        public void Reset()
        {
            this.Patient = new Patient();
        }

        public Patient getResult()
        {
            Patient retVal = this.Patient;
            Reset();
            return retVal;
        }

        public void SetGuest(bool guest)
            => Patient.Guest = guest;
        public void SetRhFactor(Rhfaktor rhfaktor)
            => Patient.RhFactor = rhfaktor;

        public void SetBloodType(Bloodtype bloodtype)
            => Patient.BloodType = bloodtype;

        public void SetBirthDate(DateTime birthDate)
            => Patient.BirthDate = birthDate;

        public void SetBirthPlace(City birthPlace)
            => Patient.BirthPlace = birthPlace;

        public void SetContact(Contact contact)
            => Patient.Contact = contact;

        public void SetGender(Gender gender)
            => Patient.Gender = gender;

        public void SetJMBG(string jmbg)
            => Patient.JMBG = jmbg;

        public void SetName(string name)
            => Patient.Name = name;

        public void SetPassword(string password)
            => Patient.Password = password;

        public void SetResidence(Adress residence)
            => Patient.Residence = residence;

        public void SetSurname(string surname)
            => Patient.Surname = surname;

        public void SetUserType(Usertype userType)
            => Patient.UserType = userType;
    }
}
