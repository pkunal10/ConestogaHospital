using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConestogaHospital.Models;

namespace ConestogaHospital.Models
{
    public class RoomsViewModel
    {
        public int DetailsId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int RoomId { get; set; }
        public string RoomNo { get; set; }
        public int FloorNo { get; set; }
        public DateTime AdmitDate { get; set; }
        public string AdmitDateDisplay { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpeciality { get; set; }
        public bool IsAvailable { get; set; }
    }
}