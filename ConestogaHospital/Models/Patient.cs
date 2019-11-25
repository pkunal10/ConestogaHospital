using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConestogaHospital.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PatientNo { get; set; }
        public string EmailId { get; set; }
        public DateTime DOB { get; set; }
        public int AddressId { get; set; }
        public string MobileNo { get; set; }
    }
}