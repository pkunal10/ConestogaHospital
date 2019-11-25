using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConestogaHospital.Models;

namespace ConestogaHospital.Models
{
    public class PatientViewModel
    {
        public Patient Patient { get; set; }
        public List<Alergy> Alergies { get; set; }
        public Address Address { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }        
        public DateTime AdmitDate { get; set; }        
        public string DisplayAdmitDate { get; set; }        
        public string RoomNo { get; set; }
        public int AdmitDetailId { get; set; }

    }
}