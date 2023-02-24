using System;

namespace Model.Users.UserBuilder
{
    public class SecretaryBuilder : IUserBuilder
    {
        private Secretary Secretary;

        public SecretaryBuilder()
        {
            this.Secretary = new Secretary();
        }
        public void Reset()
        {
            this.Secretary = new Secretary();
        }

        public Secretary getResult()
            => this.Secretary;

        public void SetPicture(string picture)
            => Secretary.Picture = picture;

        public void SetEducationLevel(Educationlevel educationlevel)
            => Secretary.EducationLevel = educationlevel;

        public void SetBirthDate(DateTime birthDate)
            => Secretary.BirthDate = birthDate;

        public void SetBirthPlace(City birthPlace)
            => Secretary.BirthPlace = birthPlace;

        public void SetContact(Contact contact)
            => Secretary.Contact = contact;

        public void SetGender(Gender gender)
            => Secretary.Gender = gender;

        public void SetJMBG(string jmbg)
            => Secretary.JMBG = jmbg;

        public void SetName(string name)
            => Secretary.Name = name;

        public void SetPassword(string password)
            => Secretary.Password = password;

        public void SetResidence(Adress residence)
            => Secretary.Residence = residence;

        public void SetSurname(string surname)
            => Secretary.Surname = surname;

        public void SetUserType(Usertype userType)
            => Secretary.UserType = userType;
    }
}
