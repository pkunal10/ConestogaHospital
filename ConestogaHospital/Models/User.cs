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
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public int RoleId { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public int AddressId { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public Address Address { get; set; }
        [NotMapped]
        public Doctor Doctor { get; set; }
    }
}