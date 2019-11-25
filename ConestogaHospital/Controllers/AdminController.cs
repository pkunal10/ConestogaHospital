using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConestogaHospital.Models;
using ConestogaHospital.Services;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace ConestogaHospital.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminServices = new AdminService();
        //
        // GET: /Admin/
        public ActionResult UserRegister()
        {
            var user = _adminServices.GetUserById(Convert.ToInt32(Session["UserId"].ToString()));
            var role = _adminServices.GetRoleNameById(user.RoleId);
            if (Convert.ToString(Session["Userid"]) == "")
            {
                return RedirectToAction("Login", "Home");
            }
            if (role != "Admin")
            {
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }

            return View();
        }
        public JsonResult GetRoleList()
        {
            var roleList = _adminServices.GetRoleListForDropdown();

            return new JsonResult { Data = new { Status = "Success", data = roleList }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetSpecialityList()
        {
            var specialityList = _adminServices.GetSpecialityListForDropdown();

            return new JsonResult { Data = new { Status = "Success", data = specialityList }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult CheckAvailability(string UserName)
        {
            bool isavailable = _adminServices.IsUserNameAvailable(UserName);
            if (isavailable)
            {
                return new JsonResult { Data = new { Status = "Success" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = new { Status = "NotAvailable", msg = "User Name NOT Available" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public JsonResult AddUser(string UserName, string RoleId, string Password, string FName, string LName, string DOB, string SpecialityId, string RoomNo, string Mobile, string EmailId, bool ISDoctor, string Line1, string City, string Province, string ZipCode)
        {

            //var file = Request.Files[0];

            if (Mobile.Length > 10 || Mobile.Length < 10)
            {
                return new JsonResult { Data = new { Status = "Error", msg = "Enter Correct MobileNo" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            //else if (!Regex.IsMatch(EmailId, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            //{
            //    return new JsonResult { Data = new { Status = "Error", msg = "Enter Correct Email Id" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //}
            else if (Request.Files.Count == 0)
            {
                return new JsonResult { Data = new { Status = "Error", msg = "Select Image" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                DirectoryInfo fileDirectory =
                                   new DirectoryInfo(
                                       HttpContext.Server.MapPath(
                                           System.Configuration.ConfigurationManager.AppSettings["UserImage"]));

                if (!fileDirectory.Exists)
                {
                    fileDirectory.Create();
                }
                string fileName = DateTime.Now.ToString("ddMMyyhhmmssfff.") +
                                                   Request.Files[0].FileName.Split('.').LastOrDefault();
                string savePath =
                    HttpContext.Server.MapPath(
                        System.Configuration.ConfigurationManager.AppSettings["UserImage"]);
                Request.Files[0].SaveAs(savePath + fileName);

                var doctorModal = new Doctor();
                if (ISDoctor == true)
                {
                    doctorModal.SpecialityId = Convert.ToInt32(SpecialityId);
                    doctorModal.RoomNo = RoomNo;

                }

                var addressModal = new Address();
                addressModal.Line1 = Line1;
                addressModal.City = City;
                addressModal.Province = Province;
                addressModal.ZipCode = ZipCode;

                var model = new User();
                model.UserName = UserName;
                model.FName = FName;
                model.LName = LName;
                model.DOB = Convert.ToDateTime(DOB);                
                model.RoleId = Convert.ToInt32(RoleId);
                model.Mobile = Mobile;
                model.EmailId = EmailId;
                model.Password = Password;
                model.Image = fileName;
                model.Doctor = ISDoctor== true ? doctorModal : null;
                model.Address = addressModal;

                _adminServices.AddUser(model);

                return new JsonResult { Data = new { Status = "Success", msg = "User registered." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}