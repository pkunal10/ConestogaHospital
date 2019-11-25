using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace ConestogaHospital.Models
{
    public class dbContextHMS : DbContext
    {
        public dbContextHMS()
            : base("conStr")
        {

        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Speciality> Specialites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TokenStatus> TokenSatuses { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Alergy> Alergies { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Room> Rooms { get; set; }        
        public DbSet<PatientAdmitDetail> PatientAdmitDetails { get; set; }
    }
}