using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConestogaHospital.Models;

namespace ConestogaHospital.Models
{
    public class DiagnosisViewModel
    {
        public List<Symptom> Symptoms { get; set; }
        public List<Prescription> Prescriptions { get; set; }        
        public string DiagnosisDate { get; set; }
        public bool IsAdmit { get; set; }
        public Room SelectedRoom { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public Token Token { get;set; }
        public int DiagnosisId { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTime DiagnosisDateDT { get; set; }
        public string Speciality { get; set; }
        public PatientAdmitDetail PatientAdmitDetail { get; set; }
    }
}