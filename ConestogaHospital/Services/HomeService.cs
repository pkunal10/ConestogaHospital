using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConestogaHospital.Models;
using System.Data.Entity.Migrations;

namespace ConestogaHospital.Services
{
    public class HomeService : IHomeService
    {
        dbContextHMS context = new dbContextHMS();
        public int Login(string uname, string password)
        {
            var q = context.Users.Where(x => x.UserName == uname && x.Password == password).FirstOrDefault();
            if (q == null)
            {
                return 0;
            }
            else
            {
                return q.UserId;
            }
        }
        public List<SelectListItem> GetDoctorListFOrDropDown()
        {
            var list = from doc in context.Doctors
                       join user in context.Users on doc.UserId equals user.UserId
                       join spec in context.Specialites on doc.SpecialityId equals spec.SpecialityId
                       select new SelectListItem()
                       {
                           Value = doc.DoctorId.ToString(),
                           Text = user.FName + " " + user.LName + " (" + spec.SpecialityName + ")"

                       };

            return list.ToList();
        }
    }
}