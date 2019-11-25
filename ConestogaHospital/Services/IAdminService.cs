using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConestogaHospital.Models;
using System.Web.Mvc;
using ConestogaHospital.Services;

namespace ConestogaHospital.Services
{
    public interface IAdminService
    {
        List<SelectListItem> GetRoleListForDropdown();
        List<SelectListItem> GetSpecialityListForDropdown();
        bool IsUserNameAvailable(string UserName);
        string AddUser(User model);
        User GetUserById(int id);
        string GetRoleNameById(int id);
    }
}
