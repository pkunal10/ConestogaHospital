using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConestogaHospital.Models
{
    public class TokenViewModel
    {
        public int PatientId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PatientNo { get; set; }
        public DateTime DOB { get; set; }
        public int AddressId { get; set; }
        public string MobileNo { get; set; }
        public int TokenId { get; set; }
        public string TokenNo { get; set; }    
        public int DoctorId { get; set; }
        public DateTime GeneratedTime { get; set; }
        public string GeneratedTimeForDisplay { get; set; }
        public string TokenStatus { get; set; }
        public string RoomNo { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }

    }
}