using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Model.Users.UserBuilder
{
    public class DoctorBuilder : IUserBuilder
    {
        private Doctor Doctor;

        public DoctorBuilder()
        {
            this.Doctor = new Doctor();
        }
        public void Reset()
        {
            this.Doctor = new Doctor();
        }

        public Doctor getResult()
            => this.Doctor;

        public void SetPicture(string picture)
            => Doctor.Picture = picture;

        public void SetEducationLevel(Educationlevel educationlevel)
            => Doctor.EducationLevel = educationlevel;

        public void SetWorkRoom(Room room)
            => Doctor.WorkRoom = room;

        public void SetWorkPlace(WorkPlace workPlace)
            => Doctor.WorkPlace = workPlace;

        public void SetSpecialization(List<Specialization> specializations)
            => Doctor.Specialization = specializations;

        public void SetBiography(string biography)
            => Doctor.Biography = biography;

        public void SetBirthDate(DateTime birthDate)
            => Doctor.BirthDate = birthDate;

        public void SetBirthPlace(City birthPlace)
            => Doctor.BirthPlace = birthPlace;

        public void SetContact(Contact contact)
            => Doctor.Contact = contact;

        public void SetGender(Gender gender)
            => Doctor.Gender = gender;

        public void SetJMBG(string jmbg)
            => Doctor.JMBG = jmbg;

        public void SetName(string name)
            => Doctor.Name = name;

        public void SetPassword(string password)
            => Doctor.Password = password;

        public void SetResidence(Adress residence)
            => Doctor.Residence = residence;

        public void SetSurname(string surname)
            => Doctor.Surname = surname;

        public void SetUserType(Usertype userType)
            => Doctor.UserType = userType;
    }
}
