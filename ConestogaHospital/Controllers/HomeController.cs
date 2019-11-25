using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConestogaHospital.Models;
using ConestogaHospital.Services;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading;


namespace ConestogaHospital.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService = new HomeService();
        private readonly IAdminService _adminService = new AdminService();
        private readonly IUserService _userService = new UserService();
        private readonly IDoctorService _doctorService = new DoctorService();
        private readonly IPatientService _patientService = new PatientService();
        //
        // GET: /Home/
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult HomeScreen()
        {
            if (Convert.ToString(Session["Userid"]) == "")
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        public JsonResult LoginCheck(string Uname, string Password)
        {
            var result = _homeService.Login(Uname, Password);
            if (result == 0)
            {
                return new JsonResult { Data = new { Status = "AuthenticationFailed", msg = "Invalid Username or Password" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                Session["UserId"] = result;
                var user = _adminService.GetUserById(result);
                Session["Name"] = user.FName + " " + user.LName;
                Session["ProfilePic"] = ConfigurationManager.AppSettings["UserImage"].Trim(new Char[] { ' ', '~' }) + user.Image;
                var role = _adminService.GetRoleNameById(user.RoleId);
                Session["Role"] = role;

                return new JsonResult { Data = new { Status = "Success", data = role }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public JsonResult GetUserDetailsById()
        {
            var user = _adminService.GetUserById(Convert.ToInt32(Session["UserId"].ToString()));
            var role = _adminService.GetRoleNameById(user.RoleId);
            var speciality = "";
            var image = ConfigurationManager.AppSettings["UserImage"].Trim(new Char[] { ' ', '~' }) + user.Image;
            if (role == "Doctor")
            {
                speciality = _userService.GetSpecialityById(_userService.GetDoctorByUserId(user.UserId).SpecialityId);
            }
            var data = new { user = user, role = role, speciality = speciality, image = image };

            if (user == null)
            {
                return new JsonResult { Data = new { Status = "NoUser", msg = "No User Found" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = new { Status = "Success", data = data }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        public JsonResult GetDoctorListForDropDown()
        {
            var list = _homeService.GetDoctorListFOrDropDown();
            if (list != null && list.Count != 0)
            {
                return new JsonResult { Data = new { Status = "Success", data = list }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = new { Status = "Fail", msg = "No Doctors" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetAppointmentSlots(string date, string doctorId)
        //public JsonResult GetAppointmentSlots()
        {
            DateTime startTime = Convert.ToDateTime("09:00 AM");
            DateTime endTime;
            var appontmentList = _doctorService.getAppointmentListByDoctorAndDate(Convert.ToDateTime(date), Convert.ToInt32(doctorId));
            List<AppointmentViewModal> slots = new List<AppointmentViewModal>();
            do
            {
                endTime = startTime.AddMinutes(30);
                AppointmentViewModal appointmentViewModal = new AppointmentViewModal();
                appointmentViewModal.StartTime = startTime.ToString("hh:mm tt");
                appointmentViewModal.EndTime = endTime.ToString("hh:mm tt");
                if (appontmentList != null && appontmentList.Count != 0)
                {
                    foreach (var appointment in appontmentList)
                    {
                        if (startTime == appointment.StartTime)
                        {
                            appointmentViewModal.IsAvailable = false;
                            appointmentViewModal.BackgroundColor = "#d9534f";
                            break;
                        }
                        else
                        {
                            appointmentViewModal.IsAvailable = true;
                            appointmentViewModal.BackgroundColor = "#5cb85c";
                        }
                    }
                }
                else
                {
                    appointmentViewModal.IsAvailable = true;
                    appointmentViewModal.BackgroundColor = "#5cb85c";
                }
                slots.Add(appointmentViewModal);

                startTime = endTime;

            } while (endTime != Convert.ToDateTime("05:00 PM"));
            return new JsonResult { Data = new { Status = "Success", data = slots }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SaveAppointment(Appointment model)
        {
            if (model.AppointmentDate <= DateTime.Now)
            {
                return new JsonResult { Data = new { Status = "Fail", msg = "Appointment date should be greater than today." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                model.PatientNo = model.PatientNo.Split('(')[0];
                model.StartTime = Convert.ToDateTime(model.SelectedSlot.StartTime);
                model.EndTime = Convert.ToDateTime(model.SelectedSlot.EndTime);
                string id = _doctorService.AddAppointment(model);
                Thread email = new Thread(delegate () {
                    sendEmail(_doctorService.getAppointmentById(Convert.ToInt32(id)));
                });
                email.IsBackground = true;
                email.Start();
                return new JsonResult { Data = new { Status = "Success", msg = "Your appointment has been booked." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public void sendEmail(AppointmentViewModal modal)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("khajana246@gmail.com", "Conestoga Hospital");
                mm.To.Add(_patientService.GetPatientByPatientNo(modal.PatientNo).EmailId);
                mm.Subject = "Appointment Confirmation";
                mm.Body += "<br />Hello  " + modal.PatientName + " (" + modal.PatientNo + "),<br /><br />";
                mm.Body += "Your appointment with doctor " + modal.DoctorName + " (" + modal.Speciality + ") has been booked for " + modal.AppointmentDate.ToString("dd/MM/yyyy") + " from " + modal.AptStartTime.ToString("hh:mm tt") + " to " + modal.AptEndTime.ToString("hh:mm tt")+".";                

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("khajana246@gmail.com", "khanakhajana@123");


                smtp.Send(mm);
            }
            catch (Exception ex)
            {
                string Mail_msg = "Mail can't be sent because of server problem: ";
                // Mail_msg += ex.Message;               
                //Response.Write(Mail_msg);
                // Label11.Text = Mail_msg;
            }
        }
    }
}