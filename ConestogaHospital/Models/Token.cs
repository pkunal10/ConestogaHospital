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
    public class Token
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TokenId { get; set; }
        public string TokenNo { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime GeneratedTime { get; set; }
        public int StatusId { get; set; }
        public bool IsAnnounced { get; set; }
        [NotMapped]
        public string PatientNo { get; set; }
    }
}