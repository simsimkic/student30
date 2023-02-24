using System;

namespace Model.Users.UserBuilder
{
    public interface IUserBuilder
    {
        void SetName(string name);
        void SetSurname(string surname);
        void SetJMBG(string jmbg);
        void SetPassword(string password);
        void SetUserType(Usertype userType);
        void SetBirthDate(DateTime birthDate);
        void SetGender(Gender gender);
        void SetContact(Contact contact);
        void SetBirthPlace(City birthPlace);
        void SetResidence(Adress residence);
        void Reset();
    }
}
