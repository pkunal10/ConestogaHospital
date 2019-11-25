using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConestogaHospital.Models;
using ConestogaHospital.Services;
using System.Web.Mvc;

namespace ConestogaHospital.Services
{
    public interface IDoctorService
    {
        List<Appointment> getAppointmentListByDoctorAndDate(DateTime date, int doctorId);
        string AddAppointment(Appointment model);
        List<SelectListItem> GetDoctorListBySpeciality(int specialityId);
        List<AppointmentViewModal> getAppointmentScheduleByOfDoctor(int doctorId);
        AppointmentViewModal getAppointmentById(int appointmentId);
    }
}