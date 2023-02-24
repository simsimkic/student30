using System;

namespace Model.Users.UserBuilder
{
    public class DirectorBuilder : IUserBuilder
    {
        private Director Director;

        public DirectorBuilder()
        {
            this.Director = new Director();
        }
        public void Reset()
        {
            this.Director = new Director();
        }

        public Director getResult()
            => this.Director;

        public void SetPicture(string picture)
            => Director.Picture = picture;

        public void SetEducationLevel(Educationlevel educationlevel)
            => Director.EducationLevel = educationlevel;

        public void SetBirthDate(DateTime birthDate)
            => Director.BirthDate = birthDate;

        public void SetBirthPlace(City birthPlace)
            => Director.BirthPlace = birthPlace;

        public void SetContact(Contact contact)
            => Director.Contact = contact;

        public void SetGender(Gender gender)
            => Director.Gender = gender;

        public void SetJMBG(string jmbg)
            => Director.JMBG = jmbg;

        public void SetName(string name)
            => Director.Name = name;

        public void SetPassword(string password)
            => Director.Password = password;

        public void SetResidence(Adress residence)
            => Director.Residence = residence;

        public void SetSurname(string surname)
            => Director.Surname = surname;

        public void SetUserType(Usertype userType)
            => Director.UserType = userType;
    }
}
