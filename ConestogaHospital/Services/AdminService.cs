using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConestogaHospital.Models;
using System.Data.Entity.Migrations;
using System.Configuration;

namespace ConestogaHospital.Services
{
    public class AdminService : IAdminService
    {
        dbContextHMS context = new dbContextHMS();


        public List<SelectListItem> GetRoleListForDropdown()
        {
            var list = from role in context.Roles
                       select new SelectListItem()
                       {
                           Value = role.RoleId.ToString(),
                           Text = role.RoleName.ToString()
                       };

            return list.ToList();
        }

        public List<SelectListItem> GetSpecialityListForDropdown()
        {
            var list = from speciality in context.Specialites
                       select new SelectListItem()
                       {
                           Value = speciality.SpecialityId.ToString(),
                           Text = speciality.SpecialityName.ToString()
                       };

            return list.ToList();
        }
        public bool IsUserNameAvailable(string UserName)
        {
            bool isexist = context.Users.ToList().Exists(x => x.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
            if (isexist)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string AddUser(User model)
        {
            context.Addresses.Add(model.Address);
            context.SaveChanges();
            model.AddressId = model.Address.AddressId;
            context.Users.Add(model);
            context.SaveChanges();

            //var teacher=new Teacher();
            if (model.Doctor != null)
            {
                model.Doctor.UserId = model.UserId;
                context.Doctors.Add(model.Doctor);
                context.SaveChanges();
            }

            return "Added";
        }
        public User GetUserById(int id)
        {
            return context.Users.Where(x => x.UserId == id).FirstOrDefault();
        }
        public string GetRoleNameById(int id)
        {
            var role = context.Roles.Where(x => x.RoleId == id).FirstOrDefault().RoleName;
            return role;
        }
    }      
}