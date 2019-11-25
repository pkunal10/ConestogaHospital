using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConestogaHospital.Models;
using ConestogaHospital.Services;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace ConestogaHospital.Services
{
    public class DoctorService : IDoctorService
    {
        dbContextHMS context = new dbContextHMS();
        public List<Appointment> getAppointmentListByDoctorAndDate(DateTime date, int doctorId)
        {
            return context.Appointments.Where(x => x.AppointmentDate == date && x.DoctorId == doctorId).ToList();
        }
        public List<AppointmentViewModal> getAppointmentScheduleByOfDoctor(int doctorId)
        {
            var list = (from apt in context.Appointments
                        join pat in context.Patients on apt.PatientNo equals pat.PatientNo
                        where apt.DoctorId == doctorId && apt.AppointmentDate == DbFunctions.TruncateTime(DateTime.Now)
                        select new AppointmentViewModal()
                        {
                            AppointmentId = apt.AppointmentId,
                            PatientNo = pat.PatientNo,
                            PatientName = pat.FName + " " + pat.LName,
                            AptStartTime = apt.StartTime,
                            AptEndTime = apt.EndTime
                        }).ToList();
            return list;
        }
        public AppointmentViewModal getAppointmentById(int appointmentId)
        {
            AppointmentViewModal modal = new AppointmentViewModal();
            modal = (from apt in context.Appointments
                       join pat in context.Patients on apt.PatientNo equals pat.PatientNo
                       join doc in context.Doctors on apt.DoctorId equals doc.DoctorId
                       join usr in context.Users on doc.UserId equals usr.UserId
                       join spec in context.Specialites on doc.SpecialityId equals spec.SpecialityId
                       where apt.AppointmentId == appointmentId
                       select new AppointmentViewModal()
                       {
                           AppointmentId = apt.AppointmentId,
                           PatientNo = pat.PatientNo,
                           PatientName = pat.FName + " " + pat.LName,
                           AptStartTime = apt.StartTime,
                           AptEndTime = apt.EndTime,
                           DoctorName = usr.FName + " " + usr.LName,
                           Speciality = spec.SpecialityName,
                           AppointmentDate=apt.AppointmentDate

                       }).FirstOrDefault();
            return modal;
        }
        public string AddAppointment(Appointment model)
        {
            context.Appointments.Add(model);
            context.SaveChanges();
            return "" + model.AppointmentId;
        }

        public List<SelectListItem> GetDoctorListBySpeciality(int specialityId)
        {
            var list = from doc in context.Doctors
                       join user in context.Users on doc.UserId equals user.UserId
                       where doc.SpecialityId == specialityId
                       select new SelectListItem()
                       {
                           Value = doc.DoctorId.ToString(),
                           Text = user.FName + " " + user.LName
                       };

            return list.ToList();
        }
    }
}