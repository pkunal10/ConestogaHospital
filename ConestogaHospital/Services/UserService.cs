using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConestogaHospital.Models;
using ConestogaHospital.Services;

namespace ConestogaHospital.Services
{
    public class UserService : IUserService
    {
        dbContextHMS context = new dbContextHMS();
        public User GetUserById(int id)
        {
            return context.Users.Where(x => x.UserId == id).FirstOrDefault();
        }
        public Doctor GetDoctorByUserId(int id)
        {
            return context.Doctors.Where(x => x.UserId == id).FirstOrDefault();
        }
        public Doctor GetDoctorByUserIdForDisplay(int id)
        {
            Doctor doctor = new Doctor();
            var doc=context.Doctors.Where(x => x.UserId == id).FirstOrDefault();
            var usr = context.Users.Where(x => x.UserId == id).FirstOrDefault();
            var spec = context.Specialites.Where(x => x.SpecialityId == doc.SpecialityId).FirstOrDefault();

            doctor.DoctorId = doc.DoctorId;
            doctor.DoctorName = usr.FName + " " + usr.LName;
            doctor.Speciality = spec.SpecialityName;
            doctor.RoomNo = doc.RoomNo;

            return doctor;
        }
        public string GetSpecialityById(int id)
        {
            return context.Specialites.Where(x => x.SpecialityId == id).FirstOrDefault().SpecialityName;
        }
    }
}