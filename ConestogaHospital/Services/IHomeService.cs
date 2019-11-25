using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConestogaHospital.Models;
using ConestogaHospital.Services;
using System.Data.Entity.Migrations;

namespace ConestogaHospital.Services
{
    public interface IHomeService
    {
        int Login(string uname, string password);
        List<SelectListItem> GetDoctorListFOrDropDown();
    }
}