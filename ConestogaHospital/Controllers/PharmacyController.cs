using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConestogaHospital.Services;
using ConestogaHospital.Models;

namespace ConestogaHospital.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly IHomeService _homeService = new HomeService();
        private readonly IAdminService _adminService = new AdminService();
        private readonly IUserService _userService = new UserService();
        private readonly IDoctorService _doctorService = new DoctorService();
        private readonly ITokenService _tokenService = new TokenService();
        private readonly IPatientService _patientService = new PatientService();
        private readonly IDiagnosisService _diagnosisService = new DiagnosisService();
        // GET: Pharmacy
        public ActionResult Prescriptions()
        {
            if (Convert.ToString(Session["Userid"]) == "")
            {
                return RedirectToAction("Login", "Home");
            }
            var user = _adminService.GetUserById(Convert.ToInt32(Session["UserId"].ToString()));
            var role = _adminService.GetRoleNameById(user.RoleId);
            if (role != "Pharmacy")
            {
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public JsonResult GetDiagnosisesByPatientNo(string patientNo)
        {
            var list = _diagnosisService.GetDiagnosisByPatientNo(patientNo.Split('(')[0]);

            if (list == null || list.Count == 0)
            {
                return new JsonResult { Data = new { Status = "Fail", msg = "No diagnosis for this patient" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                foreach(var diag in list)
                {
                    diag.DiagnosisDate = diag.DiagnosisDateDT.ToString("dd/MM/yyyy");
                }
                return new JsonResult { Data = new { Status = "Success", data = list }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public JsonResult GetPresSymByDiagnosisId(string diagnosisId)
        {
            var model = _diagnosisService.GetPreSymByDiagnosisId(Convert.ToInt32(diagnosisId));

            if (model == null)
            {
                return new JsonResult { Data = new { Status = "Fail", msg = "No prescriptions for this diagnosis." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = new { Status = "Success", data = model }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}