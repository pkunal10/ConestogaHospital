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
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public int SpecialityId { get; set; }
        public string RoomNo { get; set; } 
        [NotMapped]
        public string DoctorName { get; set; }
        [NotMapped]
        public string Speciality { get; set; }
    }
}