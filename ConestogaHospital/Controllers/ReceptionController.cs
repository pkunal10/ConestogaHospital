using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConestogaHospital.Models;
using ConestogaHospital.Services;

namespace ConestogaHospital.Controllers
{
    public class ReceptionController : Controller
    {
        //
        // GET: /Reception/
        private readonly IHomeService _homeService = new HomeService();
        private readonly IAdminService _adminService = new AdminService();
        private readonly IUserService _userService = new UserService();
        private readonly IDoctorService _doctorService = new DoctorService();
        private readonly ITokenService _tokenService = new TokenService();
        private readonly IPatientService _patientService = new PatientService();
        public ActionResult TokenDisplay()
        {
            if (Convert.ToString(Session["Userid"]) == "")
            {
                return RedirectToAction("Login", "Home");
            }
            var user = _adminService.GetUserById(Convert.ToInt32(Session["UserId"].ToString()));
            var role = _adminService.GetRoleNameById(user.RoleId);
            if (role != "Receptionist")
            {
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public JsonResult GetTokenList()
        {
            var listNewToken = _tokenService.getTodaysNewToken();
            var listUnderDiagnosisToken = _tokenService.getTodaysUnderDiagnosisToken();

            var data = new { listNewToken, listUnderDiagnosisToken };

            return new JsonResult { Data = new { Status = "Success", data = data }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult GetTokenListNotAnnounced()
        {
            var list = _tokenService.getTodaysUnderDiagnosisTokenNotAnnounced();
            if (list != null && list.Count != 0)
            {
                return new JsonResult { Data = new { Status = "Success", data = list }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = new { Status = "No Token" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public JsonResult UpdateTokenToAnnounced(string tokenId)
        {
            Token oldToken = new Token();
            oldToken = _tokenService.GetTokenById(tokenId);
            oldToken.IsAnnounced = true;
            _tokenService.UpdateToken(oldToken);
            return new JsonResult { Data = new { Status = "Success" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetTokenToAnnounce()
        {
            TokenViewModel token = new TokenViewModel();
            if (Session["tokenToAnnounce"] != null && Session["tokenToAnnounce"].ToString() != "")
            {
                token = _tokenService.GetTokenForDisplayById(Session["tokenToAnnounce"].ToString());
                return new JsonResult { Data = new { Status = "Success", data = token }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = new { Status = "Fail" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult RemoveTokenToAnnounce()
        {
            Session["tokenToAnnounce"] = "";
            return new JsonResult { Data = new { Status = "Success" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult GenerateTokenNo()
        {
            if (Convert.ToString(Session["Userid"]) == "")
            {
                return RedirectToAction("Login", "Home");
            }
            var user = _adminService.GetUserById(Convert.ToInt32(Session["UserId"].ToString()));
            var role = _adminService.GetRoleNameById(user.RoleId);
            if (role != "Receptionist")
            {
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public JsonResult GetDoctorListBySpeciality(string specialityId)
        {
            var list = _doctorService.GetDoctorListBySpeciality(Convert.ToInt32(specialityId));
            if (list != null && list.Count != 0)
            {
                return new JsonResult { Data = new { Status = "Success", data = list }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = new { Status = "Fail", msg = "No Doctors" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public JsonResult GetTokenNo(string specialityId)
        {
            var tokenNo = _tokenService.generateTokenNo(Convert.ToInt32(specialityId));
            return new JsonResult { Data = new { Status = "Success", data = tokenNo }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SaveToken(Token modal)
        {
            Patient patient = new Patient();
            patient = _patientService.GetPatientByPatientNo(modal.PatientNo.Split('(')[0]);
            if (patient == null)
            {
                return new JsonResult { Data = new { Status = "No Patient" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                modal.PatientId = patient.PatientId;
                modal.GeneratedTime = DateTime.Now;
                modal.IsAnnounced = false;
                modal.StatusId = 1;
                _tokenService.AddToken(modal);
                return new JsonResult { Data = new { Status = "Success" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetPatientNoForAutoComplete()
        {
            var list = _patientService.GetPatientNoForAutoComplete();
            return new JsonResult { Data = new { Status = "Success", data = list }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetPatientNo()
        {
            var patientNo = _patientService.generatePatientNo();
            return new JsonResult { Data = new { Status = "Success", data = patientNo }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult PatientRegister(PatientViewModel model)
        {
            if (model.Patient.MobileNo.Length > 10 || model.Patient.MobileNo.Length < 10)
            {
                return new JsonResult { Data = new { Status = "Error", msg = "Enter Correct MobileNo" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                _patientService.RegisterPatient(model);
                return new JsonResult { Data = new { Status = "Success", msg = "Patient Registered." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult TokenList()
        {
            if (Convert.ToString(Session["Userid"]) == "")
            {
                return RedirectToAction("Login", "Home");
            }
            var user = _adminService.GetUserById(Convert.ToInt32(Session["UserId"].ToString()));
            var role = _adminService.GetRoleNameById(user.RoleId);
            if (role != "Receptionist")
            {
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public JsonResult GetTodaysNewToken()
        {
            var list = _tokenService.getTodaysNewToken();
            if (list.Count == 0 || list == null)
            {
                return new JsonResult { Data = new { Status = "NoToken", msg = "No New token for today yet." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                foreach (var token in list)
                {
                    token.GeneratedTimeForDisplay = token.GeneratedTime.ToString("dd/MM/yyyy HH:mm:ss tt");
                }
                return new JsonResult { Data = new { Status = "Success", data = list }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public JsonResult DeleteToken(string tokenId)
        {
            _tokenService.DeleteToken(Convert.ToInt32(tokenId));
            return new JsonResult { Data = new { Status = "Success" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult AdmitPatientList()
        {
            if (Convert.ToString(Session["Userid"]) == "")
            {
                return RedirectToAction("Login", "Home");
            }
            var user = _adminService.GetUserById(Convert.ToInt32(Session["UserId"].ToString()));
            var role = _adminService.GetRoleNameById(user.RoleId);
            if (role != "Receptionist")
            {
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public JsonResult GetAdmitPatientList()
        {
            var list = _patientService.GetAdmitPatientsList();
            if (list.Count == 0 || list == null)
            {
                return new JsonResult { Data = new { Status = "NoPatientAdmited", msg = "No admit patients." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                foreach (var det in list)
                {
                    det.DisplayAdmitDate = det.AdmitDate.ToString("dd/MM/yyyy");
                }
                return new JsonResult { Data = new { Status = "Success", data = list }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public JsonResult DischargePatient(string detailsId)
        {
            PatientAdmitDetail oldDetails = new PatientAdmitDetail();
            oldDetails = _patientService.GetAdmitDetailById(Convert.ToInt32(detailsId));
            oldDetails.DischargeDate = DateTime.Now;
            _patientService.UpdateAdmitDetails(oldDetails);
            return new JsonResult { Data = new { Status = "Success" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}