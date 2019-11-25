using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConestogaHospital.Models;
using ConestogaHospital.Services;

namespace ConestogaHospital.Services
{
    public interface IUserService
    {
        User GetUserById(int id);
        Doctor GetDoctorByUserId(int id);
        string GetSpecialityById(int id);
        Doctor GetDoctorByUserIdForDisplay(int id);

    }
}