using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConestogaHospital.Models;
using ConestogaHospital.Services;
using System.Speech.Synthesis;

namespace ConestogaHospital.Controllers
{
    public class DiagnosisController : Controller
    {
        //
        // GET: /Diagnosis/           
        private readonly IHomeService _homeService = new HomeService();
        private readonly IAdminService _adminService = new AdminService();
        private readonly IUserService _userService = new UserService();
        private readonly IDoctorService _doctorService = new DoctorService();
        private readonly ITokenService _tokenService = new TokenService();
        private readonly IDiagnosisService _diagnosisService = new DiagnosisService();
        private readonly IRoomService _roomService = new RoomService();
        public ActionResult Diagnosis()
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
        public JsonResult GetTokenDetailsByDoctorId()
        {
            var user = _adminService.GetUserById(Convert.ToInt32(Session["UserId"].ToString()));
            var role = _adminService.GetRoleNameById(user.RoleId);
            Doctor doctor = new Doctor();
            if (role == "Doctor")
            {
                doctor = _userService.GetDoctorByUserId(user.UserId);
            }
            var list = _tokenService.GetAllPendingTokensByDoctorId(doctor.DoctorId);

            if (list == null || list.Count == 0)
            {
                return new JsonResult { Data = new { Status = "NoToken", msg = "No pending tokens" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                foreach (var curToken in list)
                {
                    curToken.GeneratedTimeForDisplay = curToken.GeneratedTime.ToString("HH:mm:ss tt");
                }
                var data = new { tokenList = list, today = DateTime.Now.ToString("dd/M/yyyy") };
                return new JsonResult { Data = new { Status = "Success", data = data }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult MakeUnderDiagnosis(string tokenId)
        {
            Token oldToken = new Token();
            oldToken = _tokenService.GetTokenById(tokenId);
            oldToken.StatusId = 2;
            _tokenService.UpdateToken(oldToken);

            var token = _tokenService.GetTokenForDisplayById(tokenId);

            return new JsonResult { Data = new { Status = "Success", data = token }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult AnnounceToken(string tokenId)
        {
            Session["tokenToAnnounce"] = tokenId;
            return new JsonResult { Data = new { Status = "Success" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetPatientLastDiagnosisAndAlergies(string patientId)
        {
            var alergies = _diagnosisService.GetPatientAlergies(Convert.ToInt32(patientId));
            var lastDiagnosis = _diagnosisService.GetLastDiagnosisOfPatient(Convert.ToInt32(patientId), _userService.GetDoctorByUserId(Convert.ToInt32(Session["Userid"].ToString())).DoctorId);

            var data = new { alergies, lastDiagnosis };
            return new JsonResult { Data = new { Status = "Success", data = data }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
        public JsonResult FetchRoomNos()
        {
            var list = _roomService.getAllRooms();
            foreach (var room in list)
            {
                if (room.AdmitDate != null)
                {
                    room.AdmitDateDisplay = room.AdmitDate.ToString("dd/MM/yyyy");
                }
            }
            var floors = list.Select(x => x.FloorNo).Distinct();
            var data = new { list, floors };

            return new JsonResult { Data = new { Status = "Success", data = data }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult SaveDiagnosis(DiagnosisViewModel model)
        {
            Doctor doctor = new Doctor();
            doctor = _userService.GetDoctorByUserId(Convert.ToInt32(Session["UserId"]));
            model.Diagnosis.DoctorId = doctor.DoctorId;
            if (model.IsAdmit != false)
                model.PatientAdmitDetail.DoctorId = doctor.DoctorId;
            _diagnosisService.SaveDiagnosis(model);
            Token oldToken = _tokenService.GetTokenById(Convert.ToString(model.Token.TokenId));
            oldToken.StatusId = 3;
            _tokenService.UpdateToken(oldToken);
            return new JsonResult { Data = new { Status = "Success", msg = "Diagnosis completed." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}