using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConestogaHospital.Models
{
    public class AppointmentViewModal
    {
        public int AppointmentId { get; set; }
        public string StartTime { get; set; }
        public DateTime AptStartTime { get; set; }
        public DateTime AptEndTime { get; set; }
        public string EndTime { get; set; }
        public string PatientNo { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Speciality { get; set; }
        public bool IsAvailable { get; set; }
        public string BackgroundColor { get; set; }
    }
}