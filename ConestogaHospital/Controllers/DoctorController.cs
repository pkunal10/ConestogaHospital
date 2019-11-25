using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConestogaHospital.Models;
using ConestogaHospital.Services;

namespace ConestogaHospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IHomeService _homeService = new HomeService();
        private readonly IAdminService _adminService = new AdminService();
        private readonly IUserService _userService = new UserService();
        private readonly IDoctorService _doctorService = new DoctorService();
        private readonly ITokenService _tokenService = new TokenService();
        private readonly IDiagnosisService _diagnosisService = new DiagnosisService();
        private readonly IRoomService _roomService = new RoomService();
        // GET: Doctor
        public ActionResult AppointmentSchedule()
        {
            if (Convert.ToString(Session["Userid"]) == "")
            {
                return RedirectToAction("Login", "Home");
            }
            var user = _adminService.GetUserById(Convert.ToInt32(Session["UserId"].ToString()));
            var role = _adminService.GetRoleNameById(user.RoleId);
            if (role != "Doctor")
            {
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public JsonResult GetDoctorDetails()
        {
            string todayDate = DateTime.Now.ToString("dd/MM/yyyy");
            var data = _userService.GetDoctorByUserIdForDisplay(Convert.ToInt32(Session["UserId"].ToString()));
            return new JsonResult { Data = new { Status = "Success", data = data, today = todayDate }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetDoctorSchedule()
        {
            Doctor doctor = new Doctor();
            doctor = _userService.GetDoctorByUserId(Convert.ToInt32(Session["UserId"].ToString()));
            var list = _doctorService.getAppointmentScheduleByOfDoctor(doctor.DoctorId);
            if (list.Count == 0)
            {
                return new JsonResult { Data = new { Status = "NoAppointment", msg = "No Appintments for today." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                foreach (var apt in list)
                {
                    apt.StartTime = apt.AptStartTime.ToString("hh:mm tt");
                    apt.EndTime = apt.AptEndTime.ToString("hh:mm tt");
                }
                return new JsonResult { Data = new { Status = "Success", data = list }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
    }
}